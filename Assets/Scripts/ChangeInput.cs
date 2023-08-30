using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChangeInput : MonoBehaviour
{
    LoginDB loginDB;
    public GameObject login;
    EventSystem system;
    public Selectable firstInput;
    public Button loginButton;


    private void Awake()
    {
        loginDB = GameObject.Find("Login Screen").GetComponent<LoginDB>(); //assigns component to code as it is an empty field without this
    }


    // Start is called before the first frame update
    void Start()
    {
        system = EventSystem.current; //stores the current data being looked at
        firstInput.Select();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift))
        {
            Selectable previous = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp(); // finds the previous selectable component before the current
            if (previous != null)
            {
                previous.Select();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown(); // finds the next selectable component after the current
            if (next != null)
            {
                next.Select();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            loginButton.onClick.Invoke();
            Debug.Log("Button pressed.");
            loginDB.LoginUser();
            //makes function on other script activate
        }
    }
}
