using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcController : MonoBehaviour
{
    [SerializeField] private string prompt;
    public string InteractionPrompt => prompt;

    public bool touching = false;
    public float speed = 10;
    public wallTouch WallTouch;

    // Update is called once per frame
    void Update()
    {
        npcMovement();
    }

    public void npcMovement()
    {
        if (touching == false)
        {
            if (WallTouch.timerOn == false)
            {
                transform.Translate(Vector3.back * Time.deltaTime * speed);
            }
        }
    }

}
