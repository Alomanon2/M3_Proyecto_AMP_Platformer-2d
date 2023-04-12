using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverCtrl : MonoBehaviour
{


    private GameObject player;
    public GameObject powerUpShoot, instructionLever, instructionFire;
    private Animator leverAnimator;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        leverAnimator= GetComponent<Animator>();
        powerUpShoot.SetActive(false);
        instructionFire.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PullLever();
        }
    }
    private void PullLever()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            leverAnimator.SetTrigger("LeverTrigger");
            powerUpShoot.SetActive(true);
            Destroy(instructionLever);
            instructionFire.SetActive(true);
        }
    }
}
