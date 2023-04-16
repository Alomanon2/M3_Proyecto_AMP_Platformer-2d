using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class LimitCtrlBoss2 : MonoBehaviour
{
    private BoxCollider2D thisBoxCollider;
    private bool blocked = false;

    BossCtrl bossCtrl;

    // Start is called before the first frame update
    void Start()
    {
        thisBoxCollider = GetComponent<BoxCollider2D>();
        thisBoxCollider.isTrigger = false;
        bossCtrl = FindObjectOfType<BossCtrl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bossCtrl.BossDeath())
        {
            thisBoxCollider.isTrigger = true;
            //Camera.main.orthographicSize = 4;
        }
    }
}
