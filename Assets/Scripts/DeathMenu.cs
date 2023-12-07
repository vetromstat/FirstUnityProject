using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Restart : MonoBehaviour
{

    public void ResetTheGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //GameObject.FindWithTag("Player").SetActive(true);
        gameObject.SetActive(false);
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("MainMenu"));
        
    }
}
