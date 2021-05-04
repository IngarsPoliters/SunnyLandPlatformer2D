using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField]private int playerLives = 3;
    [SerializeField]private int playerCherries = 0;
    [SerializeField] private TextMeshProUGUI count;
    [SerializeField] private TextMeshProUGUI playerLivesText;


    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);

        int numberOfGameSessionObjects = FindObjectsOfType<GameSession>().Length;
        if (numberOfGameSessionObjects > 1)
        {
            // Destroy this one 
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        playerLivesText.text = playerLives.ToString();
        count.text = playerCherries.ToString();
    }


    public void AddToCherries(int addPoints)
    {
        playerCherries += addPoints;
        // update score on screen
        count.text = playerCherries.ToString();
    }

    public void ProcessPlayerDeath()
    {
        // Check if player lives are greater than 1, take one life & respawn at this level
        if(playerLives > 1)
        {
            TakeLife();
        }
        // esle, respawn at start level
        else
        {
            ResetGameSession();
        }
    }


    private void TakeLife()
    {
        playerLives--;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        playerLivesText.text = playerLives.ToString();
    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene("Level 1");
        Destroy(gameObject);
    }

    }
