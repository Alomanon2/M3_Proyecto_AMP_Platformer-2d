using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]

public class StarCtrl : MonoBehaviour
{
    private GameObject star;
    private Vector3 initialPosition;
    private int direction = 1; // Starts moving up. Indicates movement orientation, depends on range
    private float moveRange = 0.05f, movSpeed = 0.12f;

    // Start is called before the first frame update
    void Start()
    {
        star = this.gameObject;
        initialPosition = star.transform.position;
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
        if (star.transform.position.y > initialPosition.y + moveRange)
        {
            direction = -1;
        }
        if (star.transform.position.y < initialPosition.y - moveRange)
        {
            direction = 1;
        }

        //Enemy movement
        star.transform.Translate(0, direction * movSpeed * Time.deltaTime, 0);
    }
}
