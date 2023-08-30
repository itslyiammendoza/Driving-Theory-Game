using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToLevel : MonoBehaviour
{
    public void toLevelMenu()
    {
        SceneManager.LoadScene("Levels");
    }
}
