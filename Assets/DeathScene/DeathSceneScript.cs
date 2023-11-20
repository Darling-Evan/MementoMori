using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathSceneScript : MonoBehaviour
{
    public void Respawn()
    {
        SceneManager.LoadScene("MainMap");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
