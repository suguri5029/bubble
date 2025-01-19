using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamsterAnim : MonoBehaviour
{
    public HamsterCheckCheek hcc;

    public void OnAnimStop()
    {
        hcc.ChangeColl();
    }
}
