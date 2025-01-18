using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foam : MonoBehaviour
{
    public CircleCollider2D coll;
    public SpriteRenderer sp;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Foam")
        {
            coll.isTrigger = false;
            sp.color = Color.yellow;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Foam")
        {
            sp.color = Color.white;
        }
    }
}
