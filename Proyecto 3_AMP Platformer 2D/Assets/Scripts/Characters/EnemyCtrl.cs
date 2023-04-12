using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyCtrl : MonoBehaviour
{
    //Enemy initial definitions
    private GameObject enemy;
    private Rigidbody2D enemyRB;

    //Position / rotation parameters
    private float localScaleX, localScaleY, localScaleZ;
    public Vector3 initialPosition;

    //Movement
    public float movSpeed =1;
    private int direction = -1; // Starts walking left, towards player. // Indicates movement orientation, depends on range
    public float moveRange=4;
    
    // Start is called before the first frame update
    void Start()
    {
        enemy = this.gameObject;
        enemyRB = enemy.GetComponent<Rigidbody2D>();
        enemyRB.gravityScale = 2f;
        enemyRB.constraints = RigidbodyConstraints2D.FreezeRotation;

        localScaleX = enemy.transform.localScale.x;
        localScaleY = enemy.transform.localScale.y;
        localScaleZ = enemy.transform.localScale.z;

        initialPosition = enemy.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MovementX();
    }

    private void MovementX()
    {
        //Enemy walk direction and orientation
        if (enemy.transform.position.x >initialPosition.x+moveRange)
        {
            direction = -1;
            enemy.transform.localScale = new Vector3(localScaleX, localScaleY, localScaleZ);
        }
        if (enemy.transform.position.x < initialPosition.x - moveRange)
        {
            direction = 1;
            enemy.transform.localScale = new Vector3(-localScaleX, localScaleY, localScaleZ);
        }
        
        //Enemy movement
        enemy.transform.Translate(direction*movSpeed * Time.deltaTime, 0, 0);
    }
}
