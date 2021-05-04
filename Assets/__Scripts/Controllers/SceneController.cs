using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var index = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(index + 1);
        }
    }

    // Start is called before the first frame update
    public void Start_OnClick()
    {
        SceneManager.LoadScene("Level 1"); 
    }

    public void LoadIntroLevel()
    {
        SceneManager.LoadScene("Intro Level");
    }

    public void LoadNextScene()
    {
        var index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index + 1);
        // Get current scene index

        //Load scene with index +1 - must have next scene
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Update is called once per frame
    public void Options_OnClick()
    {
        
    }
}
