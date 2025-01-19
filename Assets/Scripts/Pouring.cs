using CarterGames.Assets.AudioManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pouring : MonoBehaviour
{
    public static bool allowedToPour = true;

    [SerializeField] TutorialHints hints;
    bool alreadyShowHint = false;

    public Image bearCan; // ��ƿsprite
    // public RectTransform bottleRectTransform; // ��ƿsprite
    public RectTransform idleRectTransform;
    public RectTransform startPouringRectTransform;

    public Transform bottle; // ��ƿ����
    public float minAngle = 60f; // ��С��б�Ƕ�
    public float maxAngle = 130f; // �����б�Ƕ�
    public float maxHoldTime = 4f; // ��סʱ�䵽�����Ƕȵ�ʱ��
    public float liquidRiseRate = 0.5f; // Һ��������ʼ����ϵ��

    private float holdTime = 0f; // ��סʱ��
    public bool isPouring = false; // �Ƿ��ڵ���

    public BoxCollider2D boxCollider;
    private float originalHeight;

    [Header("Pouring Settings")]
    public GameObject liquidPrefab; // ��ҺԤ����
    public Transform spawnPoint;   // ����λ��
    public float spawnInterval = 0.5f; // ���ɼ��
    public float baseSpawnInterval = 0.5f;
    public float maxSpawnInterval = 0.05f;
    private Coroutine pouringCoroutine;

    void Start()
    {
        allowedToPour = true;
    }

    void Update()
    {
        if (!allowedToPour) return;

        // ���ո���
        // if (Input.GetMouseButtonDown(0))
        if (Input.GetKeyDown(KeyCode.Space))
        {
            setBearCanRectTransform(startPouringRectTransform, bearCan.rectTransform);
            if (!alreadyShowHint) {
                hints.displayNextHint();
                alreadyShowHint = true;
            }

            isPouring = true;
            holdTime = 0f;
            GetComponent<InspectorAudioClipPlayer>().Play();
            pouringCoroutine = StartCoroutine(SpawnLiquidCoroutine());
        }

        // ���ո��ɿ�
        if (Input.GetKeyUp(KeyCode.Space)) {
            isPouring = false;
            holdTime = 0f;
            spawnInterval = baseSpawnInterval;
            GetComponent<InspectorAudioClipPlayer>().Stop();

            if (pouringCoroutine != null)
            {
                StopCoroutine(pouringCoroutine);
                pouringCoroutine = null;
            }

            setBearCanRectTransform(idleRectTransform, bearCan.rectTransform);
        }
        // ������б�Ƕ�
        if (isPouring)
        {
            holdTime += Time.deltaTime;
            float t = Mathf.Clamp01(holdTime / maxHoldTime);
            float angle = Mathf.Lerp(minAngle, maxAngle, t);
            bottle.rotation = Quaternion.Euler(0f, 0f, angle);

            bearCan.rectTransform.rotation = Quaternion.Euler(0f, 0f, angle);
            spawnInterval -= 0.1f * Time.deltaTime;
            if (spawnInterval <= maxSpawnInterval)
                spawnInterval = maxSpawnInterval;

            // ʹ��ָ��˥����������Һ�������ٶ�
            float pourSpeed = liquidRiseRate / (1f + holdTime * 5f);

            if (boxCollider != null)
            {
                // ��ȡ��ǰ�Ĵ�С
                Vector2 currentSize = boxCollider.size;
                // ���Ӹ߶�
                currentSize.y += pourSpeed * Time.deltaTime;

                // ������ײ��Ĵ�С
                boxCollider.size = currentSize;

                // ��֤�±�Ե������ֻ�����ϱ�Ե
                Vector2 currentOffset = boxCollider.offset;
                boxCollider.offset = new Vector2(currentOffset.x, currentOffset.y + (pourSpeed * Time.deltaTime) / 2);
            }
        }
    }

    IEnumerator SpawnLiquidCoroutine()
    {
        while (isPouring)
        {
            Instantiate(liquidPrefab, spawnPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void RefreshScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void setBearCanRectTransform(RectTransform src, RectTransform dest)
    {
        dest.position = src.position;
        dest.pivot = src.pivot;
        dest.rotation = src.rotation;
    }
}

