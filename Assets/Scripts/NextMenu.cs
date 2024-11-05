using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextMenu : MonoBehaviour
{
    public void NextGame()
    {
        SceneManager.LoadScene("Medium");
    }
}
