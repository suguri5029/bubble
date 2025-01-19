using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamsterCheckCheek : MonoBehaviour
{
    public GameObject hamster1;
    public GameObject hamsterAnim;
    public GameObject hameter2;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.GetComponent<Foam>().pour == true)
        {
            hamster1.SetActive(false);
            hamsterAnim.SetActive(true);
        }
    }
    public void ChangeColl()
    {
        hamsterAnim.SetActive(false);
        hameter2.SetActive(true);
    }
}
