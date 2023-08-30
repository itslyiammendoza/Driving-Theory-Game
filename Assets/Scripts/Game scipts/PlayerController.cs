using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 20;
    public float turnSpeed = 60;
    public float horizontalInput;
    public float verticalInput;

    private bool questAccepted = false;

    void Update()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        //gets w, a, s, d inputs

        transform.Translate(Vector3.back * Time.deltaTime * speed * verticalInput);
        transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * horizontalInput);
        //uses these inputs to interact with player object
        
    }
}