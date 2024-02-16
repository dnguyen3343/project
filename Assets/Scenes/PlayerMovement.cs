using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    //All the variables
    public float moveSpeed = 3.0f;
    public double staminaGauge = 10.0f;
    public Rigidbody2D thing;
    Boolean tired = false;
    public int tiredCounter = 0;
    public Slider slider;
    Vector2 movement;
   

    // Update is called once per frame
    void Update()
    {
        //gets the keyboard inputs
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        //detremines percantage of the slider
        slider.value = (float)staminaGauge/10;
        //sprinting code
        if (Input.GetKey(KeyCode.Space)&&staminaGauge>=0)
        {
            moveSpeed = 6.0f;
            staminaGauge = staminaGauge - 0.01;
            //tired code
            if (Input.GetKeyDown(KeyCode.Space) && staminaGauge < 1)
            {
                tiredCounter++;
            }
            if (tiredCounter >= 3)
            {
                tired = true;
            }
        }
        else
        {
            //restoring stamina code
            moveSpeed = 3.0f;
            if (staminaGauge < 10 && !Input.GetKey(KeyCode.Space))
            {
                staminaGauge = staminaGauge + 0.0005;
            }
            //removing tired code + capping stamina
            else if (staminaGauge > 10)
            {
                staminaGauge = 10;
                tiredCounter = 0;
                tired = false;
            }
                
        }


    }

    void FixedUpdate()
    {
        //all direction same distance
        movement.Normalize();

        //speed od player code
        if (!tired)
        {
            thing.MovePosition(thing.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
        else 
        {
            thing.MovePosition(thing.position + movement * moveSpeed * Time.fixedDeltaTime/2);
        }
    }
}
