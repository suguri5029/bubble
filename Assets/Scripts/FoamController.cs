using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FoamController : MonoBehaviour
{
    public static FoamController instance;
    public RandomCustomer rc;
    public Pouring pour;
    public Color gizmoColor = Color.cyan;         // Gizmo 的颜色
    public Vector2 detectionCenter = Vector2.zero; // 检测范围中心点
    public TMP_Text text;
    public TMP_Text spillText;
    public float detectionRadius = 5f;            // 检测范围半径
    private float timeSinceFull = 0f;
    public bool countDown;     
    public int spill;//溢出数量 
    

    public void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }

    /// <summary>
    /// 计算给定范围内泡沫状态为 true 的比例
    /// </summary>
    /// <returns>泡沫状态为 true 的比例</returns>
    public float GetFoamRatio()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(detectionCenter, detectionRadius);
        int totalFoam = 0;
        int trueFoam = 0;

        foreach (Collider2D col in colliders)
        {
            Foam foamScript = col.GetComponent<Foam>();
            if (foamScript != null) // 确保对象有 Foam 脚本
            {
                totalFoam++;
                if (foamScript.foam)
                {
                    trueFoam++;
                }
            }
        }

        if (totalFoam == 0) return 0f;
        return (float)trueFoam / totalFoam;
    }

    /// <summary>
    /// 在指定范围内打印泡沫状态比例（用于测试）
    /// </summary>
    public void PrintFoamRatio()
    {
        float ratio = GetFoamRatio();
        text.text = $"泡沫比例: {ratio * 100f:F0}%";
    }

    private void Update()
    {
        PrintFoamRatio();
        spillText.text = spill.ToString();
        if (spill > 15)
        {
            rc.feedbackText.text = "溢出太多了";
            return;
        }
        if (countDown)
        {
            timeSinceFull += Time.deltaTime;
            if (timeSinceFull > 5f)
            {
                rc.CheckRate(GetFoamRatio() * 100f);
                timeSinceFull = 0f; // 防止重复触发
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(detectionCenter, detectionRadius);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(gizmoColor.r, gizmoColor.g, gizmoColor.b, 0.4f);
        Gizmos.DrawSphere(detectionCenter, detectionRadius);
    }
}
