using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Escape : MonoBehaviour
{

    EventSystem system;
    public Button escapeButton;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            escapeButton.onClick.Invoke();
            Debug.Log("Button pressed.");
        }
    }
}
