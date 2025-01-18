using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FoamController : MonoBehaviour
{
    public Vector2 detectionCenter = Vector2.zero; // ��ⷶΧ���ĵ�
    public float detectionRadius = 5f;            // ��ⷶΧ�뾶
    public Color gizmoColor = Color.cyan;         // Gizmo ����ɫ
    public TMP_Text text;
    public Pouring pour;
    public static FoamController instance;
    public bool countDown;
    private float timeSinceFull = 0f; // �����ϴ�ֹͣ���Ƶ�ʱ��
    public RandomCustomer rc;

    public void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }

    /// <summary>
    /// ���������Χ����ĭ״̬Ϊ true �ı���
    /// </summary>
    /// <returns>��ĭ״̬Ϊ true �ı���</returns>
    public float GetFoamRatio()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(detectionCenter, detectionRadius);
        int totalFoam = 0;
        int trueFoam = 0;

        foreach (Collider2D col in colliders)
        {
            Foam foamScript = col.GetComponent<Foam>();
            if (foamScript != null) // ȷ�������� Foam �ű�
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
    /// ��ָ����Χ�ڴ�ӡ��ĭ״̬���������ڲ��ԣ�
    /// </summary>
    public void PrintFoamRatio()
    {
        float ratio = GetFoamRatio();
        text.text = $"��ĭ����: {ratio * 100f:F0}%";
    }

    private void Update()
    {
        PrintFoamRatio();
        // ��� isPouring Ϊ false����¼ʱ��
        if (countDown)
        {
            timeSinceFull += Time.deltaTime;
            if (timeSinceFull > 5f)
            {
                rc.CheckRate(GetFoamRatio() * 100f);
                timeSinceFull = 0f; // ��ֹ�ظ�����
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
