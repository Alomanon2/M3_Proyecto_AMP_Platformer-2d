using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    //Health parameters
    public int health, heartContainers;
    public Sprite fullHeart, emptyHeart;
    public Image[] maxHearts;

    //Player parameters
    private GameObject player;
    Rigidbody2D playerRB;
    private float damageForce = 10f;

    //Required for game manager Menu Ctrl (scene change, menu update)
    public bool death, levelFinished, gameFinished;
    MenuCtrl menuCtrl;
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetInt("_health",maxHearts.Length); // I used this becasue player prefs made it so that every time i start the game, i start with less 5 hearts (After 1st gameplay losing some)
        
        death = false;
        levelFinished = false;
        gameFinished= false;

        menuCtrl= FindObjectOfType<MenuCtrl>();
        if(menuCtrl.countScene == 1) { PlayerPrefs.SetInt("_health", maxHearts.Length); }
        health = PlayerPrefs.GetInt("_health");
        
        heartContainers = maxHearts.Length;
        player = GameObject.FindGameObjectWithTag("Player"); 
        playerRB = player.GetComponent<Rigidbody2D>();

        FindObjectOfType<MenuCtrl>().playerHealth = this;

    }

    // Update is called once per frame
    void Update()
    {
        HeartContainers();
        PlayerDeath();
        PlayerPrefs.SetInt("_health", health);
        //if(Input.GetButton("Fire1")==true) { playerRB.AddForce(Vector2.right * damageForce*10, ForceMode2D.Impulse); }
    }

    private void HeartContainers()
    {
        for (int i = 0; i < maxHearts.Length; i++)
        {
            if (i < health)
            {
                maxHearts[i].sprite = fullHeart;
            }
            else
            {
                maxHearts[i].sprite = emptyHeart;
            }
            if (i < maxHearts.Length)
            {
                maxHearts[i].enabled = true;
            }
            else
            {
                maxHearts[i].enabled = false;
            }

        }
    }  //Rendering empty hearts when damaged.

    private void OnCollisionEnter2D(Collision2D collision) //Falling or enemy collision impact on health
    {
        if(collision.gameObject.tag == "Destroyer")
        {
            health --;
        }
        if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "BOSS")
        {
            health --;
            playerRB.AddForce(Vector2.up * damageForce, ForceMode2D.Impulse); 
            if (player.transform.localScale.x > 0)
            {
                playerRB.AddForce(Vector2.left*damageForce*100,ForceMode2D.Impulse);
            }
            if (player.transform.localScale.x < 0)
            {
                playerRB.AddForce(Vector2.right*damageForce*100, ForceMode2D.Impulse);
            }
            
            //playerRB.AddForce(new Vector2(-player.transform.localScale.x/Mathf.Abs(player.transform.localScale.x)*Mathf.Abs(damageForce)*100,Mathf.Abs(damageForce)), //Adds a force opposite to the players orientation.x and upwards.
            //    ForceMode2D.Impulse);
            //print("Impulse X:"+player.transform.localScale.x / Mathf.Abs(player.transform.localScale.x) * Mathf.Abs(damageForce));
            //print("Impulse Y:"+Mathf.Abs(damageForce)); //Como le doy impulso opuesto al signo de local scale del enemigo (pikes, impulse down, beetle, impulse up-right/left
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Heart")
        {
            if (health < maxHearts.Length)
            {
                health++;
            }
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.tag == "EndLevel")
        {
            print("End level");
            levelFinished= true;

        } 
        if (collision.gameObject.tag == "EndGame")
        {
            print("Endgame");
            gameFinished = true;
        }

    } //End level or game trigger


    //Begin flags to die, switch levels or end game. These are called on MenuCtrl Script. 
    public bool PlayerDeath()
    {
        if (health <= 0)
        {
            Time.timeScale = 0f; 
            death = true;
            
        }
        return death;
    }

    public bool LevelFinished()
    {
        return levelFinished;
    }

    public bool GameFinished()
    {
        return gameFinished;
    }
}
