using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class LimitCtrlBoss : MonoBehaviour
{
    //public Camera mainCamera;
    private BoxCollider2D thisBoxCollider;
    private bool blocked = false;

    public bool bossStart;
    BossCtrl bossCtrl;

        // Start is called before the first frame update
    void Start()
    {
        bossStart = false;

        thisBoxCollider= GetComponent<BoxCollider2D>();
        thisBoxCollider.isTrigger= true;
        bossCtrl= FindObjectOfType<BossCtrl>();
    }

    // Update is called once per frame
    void Update()
    {
        if(bossCtrl.BossDeath())
        { 
            thisBoxCollider.isTrigger= true;
            Camera.main.orthographicSize = 4;
        }    
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(!bossCtrl.BossDeath() && collision.gameObject.tag == "Player")
        {
        thisBoxCollider.isTrigger = false;
            
            Camera.main.orthographicSize = 8;
            bossStart= true;

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        { 
            Destroy(collision.gameObject); 
        }
    }
}
