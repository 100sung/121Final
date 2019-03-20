using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneT : MonoBehaviour
{
    public void intro()
    {
        SceneManager.LoadScene("intro");
    }
    public void level1()
    {
        SceneManager.LoadScene("Level1");
    }
    public void buttonQ()
    {
        Application.Quit();
    }


}
