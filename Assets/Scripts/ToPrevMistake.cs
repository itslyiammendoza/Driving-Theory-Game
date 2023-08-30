using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToPrevMistake : MonoBehaviour
{
    public void toPrevMistakes()
    {
        SceneManager.LoadScene("PrevMistakes");
    }
}
