using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuCtrl : MonoBehaviour
{
    public GameObject panelGame, panelIntro, panelPause, panelGameOver, mainCameraScene0;
    public TextMeshProUGUI points, endText;
    public int countScene =0;
    public Animator transitionAnimator;

    private bool gameOver, gameFinished;
    private float timer;

    public PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        gameFinished = false;

        panelGame.SetActive(false);
        panelIntro.SetActive(true);
        panelPause.SetActive(false);
        panelGameOver.SetActive(false);

        Time.timeScale = 0f;

        //PowerUps start game set up
        PlayerPrefs.SetInt("_canShoot", 0); //_canShoot = 1 means player picked up power up and can shoot.

        //playerHealth = FindObjectOfType<PlayerHealth>();  // Removed because the start of this script only runs once. Replaced it on playerHealth, to run it every scene.
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        points.text = ("Time: ")+(Mathf.Round(timer*10f)/10f).ToString();
        if (playerHealth != null) // Only runs this code if playerHEalth "exists", which happens until Scene 1 is loaded, not on the intro panel.
        {
            if (playerHealth.PlayerDeath() == true || playerHealth.GameFinished() == true && countScene !=0)
            { GameOver(); Time.timeScale = 0f; }

            if (playerHealth.LevelFinished() == true)
            {
                //load next level, only 2 levels. We can make it variable for N levels. 
                countScene++;
                SceneManager.LoadScene(countScene, LoadSceneMode.Additive); // Do it variable. 
                SceneManager.UnloadSceneAsync(countScene - 1);
            }
        }
    }

    public void StartGame()
    {
        mainCameraScene0.SetActive(false); 
        panelGame.SetActive(true);
        Time.timeScale = 1f;

        panelIntro.SetActive(false);
        //transitionAnimator.SetTrigger("StartLevel");

        countScene++; 
        SceneManager.LoadScene(countScene, LoadSceneMode.Additive);
    }

    public void Pause()
    {
        panelPause.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Continue()
    {
        Time.timeScale = 1f;
        panelPause.SetActive(false);
    }

    public void Restart()
    {
        Time.timeScale = 0f; 
        panelIntro.SetActive(true); 
        panelPause.SetActive(false);
        panelGameOver.SetActive(false);

        mainCameraScene0.SetActive(true); 
        SceneManager.UnloadSceneAsync(countScene);
        countScene = 0;
        
        //SceneManager.UnloadScene(3); // Boss inactive for now
        //SceneManager.LoadScene(1,LoadSceneMode.Additive);
    }

    public void GameOver()
    {

        //print(countScene);
        panelGameOver.SetActive(true);
        Time.timeScale = 0f;

        //mainCameraScene0.SetActive(true); 
        //SceneManager.UnloadSceneAsync(countScene);
        //countScene = 0;

        if (playerHealth.PlayerDeath())
        {
            endText.text = "Game Over";
        }
        if(playerHealth.GameFinished())
        {
            endText.text = "Congratulations!";
        }
    }
}
