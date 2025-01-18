using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foam : MonoBehaviour
{
    public CircleCollider2D coll; // 圆形碰撞体
    public SpriteRenderer sp;     // 精灵渲染器
    public bool foam;             // 当前泡沫状态
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
            sp.color = Color.yellow; // 碰撞时显示黄色
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
            sp.color = Color.white; // 离开碰撞时恢复为白色
        }
    }

}
