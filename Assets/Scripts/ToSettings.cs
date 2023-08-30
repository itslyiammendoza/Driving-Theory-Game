using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToSettings : MonoBehaviour
{
    public void toSettings()
    {
        SceneManager.LoadScene("Settings");
    }
}
