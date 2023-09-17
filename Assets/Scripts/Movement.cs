using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCharacterMover : MonoBehaviour
{


    [SerializeField]
    private float movementSpeed;


    // Update is called once per frame
    void Update()
    {
        HandleMovementInput();

    }
    void HandleMovementInput()
    {

    float horizontal = Input.GetAxis("Horizontal");
    float vertical = Input.GetAxis("Vertical");
    Vector3 movement = new Vector3(horizontal, 0, vertical);
    transform.Translate(movement* movementSpeed* Time.deltaTime, Space.World);

    }

}