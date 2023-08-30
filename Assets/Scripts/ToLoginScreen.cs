using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToLoginScreen : MonoBehaviour
{
    public void toLoginScreen()
    {
        SceneManager.LoadScene("LoginScreen");
    }
}
