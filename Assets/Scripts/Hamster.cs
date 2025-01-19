using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hamster : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject liquidPrefab; // ¾ÆÒºÔ¤ÖÆÌå
    private Coroutine pouringCoroutine;

    public bool isPouring;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.GetComponent<Foam>().pour == true)
        {
            if (pouringCoroutine == null)
            {
                pouringCoroutine = StartCoroutine(SpawnLiquidCoroutine());
                isPouring = true;
            }                     
        }
    }
    IEnumerator SpawnLiquidCoroutine()
    {
        while (isPouring)
        {
            Instantiate(liquidPrefab, spawnPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }
    }


}
