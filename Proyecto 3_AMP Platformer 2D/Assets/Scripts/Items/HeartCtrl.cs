using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

[RequireComponent(typeof(BoxCollider2D))]

public class HeartCtrl : MonoBehaviour
{
    private GameObject heart;
    private Vector3 initialPosition;
    private int direction = 1; // Starts moving up. Indicates movement orientation, depends on range
    private float moveRange = 0.05f, movSpeed = 0.12f;

    // Start is called before the first frame update
    void Start()
    {
        heart = this.gameObject;
        initialPosition = heart.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        //print(heart.transform.position);
        MovementY();
    }
    private void MovementY()
    {
        //Enemy walk direction and orientation
        if (heart.transform.position.y > initialPosition.y + moveRange)
        {
            direction = -1;
        }
        if (heart.transform.position.y < initialPosition.y - moveRange)
        {
            direction = 1;
        }

        //Enemy movement
        heart.transform.Translate(0,direction * movSpeed * Time.deltaTime, 0);
    }
}
