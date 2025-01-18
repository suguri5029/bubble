using UnityEngine;

public class Pouring : MonoBehaviour
{
    public Transform bottle; // ��ƿ����
    public float minAngle = 0f; // ��С��б�Ƕ�
    public float maxAngle = 90f; // �����б�Ƕ�
    public float maxHoldTime = 2f; // ��סʱ�䵽�����Ƕȵ�ʱ��
    public float liquidRiseRate = 0.5f; // Һ����������ϵ��

    private float holdTime = 0f; // ��סʱ��
    private bool isPouring = false; // �Ƿ��ڵ���

    public BoxCollider2D boxCollider;
    public float increaseSpeed = 0.1f; // �����ٶ�
    private float originalHeight;

    [Header("Pouring Settings")]
    public GameObject liquidPrefab; // ��ҺԤ����
    public Transform spawnPoint;   // ����λ��
    public float spawnInterval = 0.1f; // ���ɼ��
    void Update()
    {
        // �����갴��
        if (Input.GetMouseButtonDown(0))
        {
            isPouring = true;
            holdTime = 0f;
            InvokeRepeating(nameof(SpawnLiquid), 0, spawnInterval);
        }

        // �������ɿ�
        if (Input.GetMouseButtonUp(0))
        {
            isPouring = false;
            holdTime = 0f;
            CancelInvoke(nameof(SpawnLiquid));
        }

        // ������б�Ƕ�
        if (isPouring)
        {
            holdTime += Time.deltaTime;
            float t = Mathf.Clamp01(holdTime / maxHoldTime);
            float angle = Mathf.Lerp(minAngle, maxAngle, t);
            bottle.rotation = Quaternion.Euler(0f, 0f, angle);

            if (boxCollider != null)
            {
                // ��ȡ��ǰ�Ĵ�С
                Vector2 currentSize = boxCollider.size;
                // ���Ӹ߶�
                currentSize.y += increaseSpeed * Time.deltaTime;

                // ������ײ��Ĵ�С
                boxCollider.size = currentSize;

                // ��֤�±�Ե������ֻ�����ϱ�Ե
                Vector2 currentOffset = boxCollider.offset;
                boxCollider.offset = new Vector2(currentOffset.x, currentOffset.y + (increaseSpeed * Time.deltaTime) / 2);
            }
        }
    }
    void SpawnLiquid()
    {
        Instantiate(liquidPrefab, spawnPoint.position, Quaternion.identity);
    }
}
