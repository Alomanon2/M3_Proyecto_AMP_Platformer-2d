using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{

    PlayerHealth playerHealth;
    Animator animator;
    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if(playerHealth!=null)
        {
            if (playerHealth.LevelFinished())
            {
                LoadNextLevel();
            }

        }
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        animator.SetTrigger("StartLevel");
        yield return new WaitForSeconds(1);
    }

}
