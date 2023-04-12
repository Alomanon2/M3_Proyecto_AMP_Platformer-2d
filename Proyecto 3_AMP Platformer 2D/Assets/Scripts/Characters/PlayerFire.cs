using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject player,bullet;
    public int canShoot;
    // Start is called before the first frame update
    void Start()
    {
        canShoot = PlayerPrefs.GetInt("_canShoot");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetInt("_canShoot", canShoot); //_canShoot = 1 means player picked up power up and can shoot now. 
        if(canShoot==1 && Input.GetKeyDown(KeyCode.M))
        {
            Shoot();
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PowerUpShoot")
        {
            canShoot = 1;
            Destroy(collision.gameObject);
        }
    }
    private void Shoot()
    {
        Instantiate(bullet, player.transform.position, Quaternion.identity);
    }    
}
