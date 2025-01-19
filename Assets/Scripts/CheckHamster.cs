using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHamster : MonoBehaviour
{
    public GameObject hamster1;
    public GameObject hamster2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.GetComponent<Foam>().pour == true)
        {
            hamster1.SetActive(false);
            hamster2.SetActive(true);
        }
    }
}
