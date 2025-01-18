using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pouring : MonoBehaviour
{
    public Transform bottle; // 酒瓶对象
    public float minAngle = 0f; // 最小倾斜角度
    public float maxAngle = 90f; // 最大倾斜角度
    public float maxHoldTime = 2f; // 按住时间到达最大角度的时长
    public float liquidRiseRate = 0.5f; // 液面上升初始速率系数

    private float holdTime = 0f; // 按住时间
    public bool isPouring = false; // 是否在倒酒

    public BoxCollider2D boxCollider;
    private float originalHeight;

    [Header("Pouring Settings")]
    public GameObject liquidPrefab; // 酒液预制体
    public Transform spawnPoint;   // 生成位置
    public float spawnInterval = 0.5f; // 生成间隔
    public float baseSpawnInterval = 0.5f;
    public float maxSpawnInterval = 0.05f;
    private Coroutine pouringCoroutine;
  

    void Update()
    {
        // 检测鼠标按下
        if (Input.GetMouseButtonDown(0))
        {
            isPouring = true;
            holdTime = 0f;
            pouringCoroutine = StartCoroutine(SpawnLiquidCoroutine());
        }

        // 检测鼠标松开
        if (Input.GetMouseButtonUp(0))
        {
            isPouring = false;
            holdTime = 0f;
            spawnInterval = baseSpawnInterval;
            if (pouringCoroutine != null)
            {
                StopCoroutine(pouringCoroutine);
                pouringCoroutine = null;
            }
        }
        // 计算倾斜角度
        if (isPouring)
        {
            holdTime += Time.deltaTime;
            float t = Mathf.Clamp01(holdTime / maxHoldTime);
            float angle = Mathf.Lerp(minAngle, maxAngle, t);
            bottle.rotation = Quaternion.Euler(0f, 0f, angle);
            spawnInterval -= 0.1f * Time.deltaTime;
            if (spawnInterval <= maxSpawnInterval)
                spawnInterval = maxSpawnInterval;

            // 使用指数衰减函数控制液面增长速度
            float pourSpeed = liquidRiseRate / (1f + holdTime * 5f);

            if (boxCollider != null)
            {
                // 获取当前的大小
                Vector2 currentSize = boxCollider.size;
                // 增加高度
                currentSize.y += pourSpeed * Time.deltaTime;

                // 更新碰撞体的大小
                boxCollider.size = currentSize;

                // 保证下边缘不动，只调整上边缘
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
}

