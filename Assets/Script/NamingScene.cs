using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NamingScene : MonoBehaviour
{
    public void ToNextScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
