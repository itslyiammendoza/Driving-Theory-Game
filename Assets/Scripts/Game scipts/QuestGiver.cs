using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuestGiver : MonoBehaviour, IInteractable
{

    [SerializeField] private string prompt;

    [Header("Timer Variables")]
    static public float startTime;
    static public bool timerStarted = false;
    static public bool gameStarted = false;

    public TMPro.TMP_Text questPopUp;
    private bool questText = false;
    private readonly float timeOnScreen = 5.0f;


    void Start()
    {

    }

    public string InteractionPrompt => prompt;
    //returns prompt when interaactionPromt is received
    public bool Interact(Interactor interactor) //what happens with object after interacted with
    {
        if (timerStarted == false)
        {
            startTime = Time.time;
            timerStarted = true;
            gameStarted = true;
            //timer starts
        }

        if (PlayerQuests.takenQuests.Count < 3)
        {
            questPopUp.enabled = true;
            questPopUp.text = "Quest Accepted!";
            questText = true;
            PlayerQuests.takenQuests.Enqueue(Random.Range(1, 21));
            Debug.Log("Quest Accepted.");
            //accepts quest
        }

        return true;
    }

    void Update()
    {
        if (questPopUp.enabled && (Time.time - startTime) >= timeOnScreen)
        {
            questPopUp.enabled = false;
            questText = false;
            //turns off quest accepted text after time has passed
        }
    }

}