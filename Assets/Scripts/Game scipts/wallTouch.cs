using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallTouch : MonoBehaviour, npcIInteractable
{
    [SerializeField] private string prompt;
    public string InteractionPrompt => prompt;

    public npcController NpcController;

    private float turnSpeed = 40;
    private float secondsLeft = 1;
    public bool timerOn = false;
    public bool choice = false;
    public int radNum = 1;

    // Update is called once per frame
    void Update()
    {
        
        if (timerOn)
        {
            
            if (secondsLeft > 0)
            {
                if (choice == false)
                {
                    radNum = Random.Range(1, 3);
                    choice = true;
                }
                Debug.Log(radNum);
                NpcController.transform.Translate(Vector3.zero);
                if (radNum == 1)
                {
                    NpcController.transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed);
                }
                else
                {
                    NpcController.transform.Rotate(Vector3.down * Time.deltaTime * turnSpeed);
                }
                secondsLeft -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Timer done");
                secondsLeft = 0;
                timerOn = false;
                choice = false;

            }
        }
    }

    public bool Interact(npcInteractor interactor)
    {
        Debug.Log("touching");
        secondsLeft = 2;
        timerOn = true;
        NpcController.touching = true;
        return true;
    }
}
