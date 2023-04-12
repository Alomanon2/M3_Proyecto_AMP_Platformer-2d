using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class BossCtrl : MonoBehaviour



    {
    //Enemy initial definitions
    private GameObject boss;
    private Rigidbody2D enemyRB;

    //Position / rotation parameters
    private float localScaleX, localScaleY, localScaleZ;
    public Vector3 initialPosition;

    //Movement
    public float movSpeed = 1;
    private int direction = -1; // Starts walking left, towards player. // Indicates movement orientation, depends on range
    public int moveRange = 4;

    //Boss Health parameters
    public int health;
    private bool bossDeath;
    public HealthBar healthBar;
    public GameObject bossHealthBar;
    public LimitCtrlBoss limitCtrlBoss;

        // Start is called before the first frame update
        void Start()
        {
            health = 50;
            bossDeath = false;
            boss = this.gameObject;
            enemyRB = boss.GetComponent<Rigidbody2D>();
            enemyRB.gravityScale = 2f;
            enemyRB.constraints = RigidbodyConstraints2D.FreezeRotation;

            localScaleX = boss.transform.localScale.x;
            localScaleY = boss.transform.localScale.y;
            localScaleZ = boss.transform.localScale.z;

            initialPosition = boss.transform.position;

            healthBar.SetMaxHealth(health);
            bossHealthBar.SetActive(false);
            limitCtrlBoss = FindObjectOfType<LimitCtrlBoss>();
}

        // Update is called once per frame
        void Update()
        {

        if (limitCtrlBoss.bossStart)
        {
            MovementX();
            bossHealthBar.SetActive(true);
        }

    }

        private void MovementX()
        {
            //Enemy walk direction and orientation
            if (boss.transform.position.x > initialPosition.x + moveRange)
            {
                direction = -1;
                boss.transform.localScale = new Vector3(localScaleX, localScaleY, localScaleZ);
            }
            if (boss.transform.position.x < initialPosition.x - moveRange)
            {
                direction = 1;
                boss.transform.localScale = new Vector3(-localScaleX, localScaleY, localScaleZ);
            }

            //Enemy movement
            boss.transform.Translate(direction * movSpeed * Time.deltaTime, 0, 0);
        }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Bullet")
        {
            direction = -direction;
            boss.transform.localScale = new Vector3(-boss.transform.localScale.x, localScaleY, localScaleZ);
        }
        if(collision.gameObject.tag =="Bullet")
        {
            BossHealth();
            healthBar.SetHealth(health);
        }
    }
 
    private void BossHealth()
    {
        health--;
        if(health<=0)
        {
            bossDeath= true;
            bossHealthBar.SetActive(false);
            Destroy(boss.gameObject); 
        }
    }
    public bool BossDeath()
    {
        return bossDeath;
    }
}