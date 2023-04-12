using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class BulletCtrl : MonoBehaviour
{

    private float bulletSpeed = 7f, bulletDirection;
    private GameObject player;

    private CircleCollider2D bulletCollider;
    private Rigidbody2D bulletRB;

    // Start is called before the first frame update
    void Start()
    {
        bulletCollider= GetComponent<CircleCollider2D>();
        bulletCollider.enabled= true;
        bulletCollider.radius = 0.05f;

        bulletRB= GetComponent<Rigidbody2D>();
        bulletRB.gravityScale = 0f;

        player = GameObject.FindGameObjectWithTag("Player");
        bulletDirection = (player.transform.localScale.x / Mathf.Abs(player.transform.localScale.x));

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(bulletSpeed * Time.deltaTime *bulletDirection, 0));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("collided");
        Destroy(this.gameObject);
        if (collision.gameObject.tag=="Enemy")
        {
            Destroy(collision.gameObject);
        }
    }
}
