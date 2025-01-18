using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foam : MonoBehaviour
{
    public CircleCollider2D coll; // Բ����ײ��
    public SpriteRenderer sp;     // ������Ⱦ��
    public bool foam;             // ��ǰ��ĭ״̬
    public bool pour;
    private void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Foam")
        {
            foam = false;
            coll.isTrigger = false;
            sp.color = Color.yellow; // ��ײʱ��ʾ��ɫ
            pour = true;
        }
        if (collision.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Foam")
        {
            foam = true;
            sp.color = Color.white; // �뿪��ײʱ�ָ�Ϊ��ɫ
        }
    }

}
