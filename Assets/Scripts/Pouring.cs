using UnityEngine;

public class Pouring : MonoBehaviour
{
    public Transform bottle; // 酒瓶对象
    public float minAngle = 0f; // 最小倾斜角度
    public float maxAngle = 90f; // 最大倾斜角度
    public float maxHoldTime = 2f; // 按住时间到达最大角度的时长
    public float liquidRiseRate = 0.5f; // 液面上升速率系数

    private float holdTime = 0f; // 按住时间
    private bool isPouring = false; // 是否在倒酒

    public BoxCollider2D boxCollider;
    public float increaseSpeed = 0.1f; // 增加速度
    private float originalHeight;

    [Header("Pouring Settings")]
    public GameObject liquidPrefab; // 酒液预制体
    public Transform spawnPoint;   // 生成位置
    public float spawnInterval = 0.1f; // 生成间隔
    void Update()
    {
        // 检测鼠标按下
        if (Input.GetMouseButtonDown(0))
        {
            isPouring = true;
            holdTime = 0f;
            InvokeRepeating(nameof(SpawnLiquid), 0, spawnInterval);
        }

        // 检测鼠标松开
        if (Input.GetMouseButtonUp(0))
        {
            isPouring = false;
            holdTime = 0f;
            CancelInvoke(nameof(SpawnLiquid));
        }

        // 计算倾斜角度
        if (isPouring)
        {
            holdTime += Time.deltaTime;
            float t = Mathf.Clamp01(holdTime / maxHoldTime);
            float angle = Mathf.Lerp(minAngle, maxAngle, t);
            bottle.rotation = Quaternion.Euler(0f, 0f, angle);

            if (boxCollider != null)
            {
                // 获取当前的大小
                Vector2 currentSize = boxCollider.size;
                // 增加高度
                currentSize.y += increaseSpeed * Time.deltaTime;

                // 更新碰撞体的大小
                boxCollider.size = currentSize;

                // 保证下边缘不动，只调整上边缘
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
