using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManagerScript : MonoBehaviour
{
    public void GameStart()
    {
        SceneManager.LoadScene(1);  // Load first level
    }
}
