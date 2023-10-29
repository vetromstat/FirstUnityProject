using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{

    public void ExitPressed()
    {
        Application.Quit();
        Debug.Log("Exited");
    }

    public void StartPressed()
    {   
        SceneManager.LoadScene("SampleScene");
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("SampleScene"));
    }

    public void SettingsPressed()
    {
        Debug.Log("Settings pressed");
    }

}
