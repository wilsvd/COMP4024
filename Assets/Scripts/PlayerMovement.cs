using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class PlayerMovement : MonoBehaviour
    {

        internal Vector2 speed = new Vector2(15f, 20f);

    // Update is called once per frame
    void Update()
        {
            float inputX = Input.GetAxisRaw("Horizontal");
            float inputY = Input.GetAxisRaw("Vertical");

            MovePlayer(inputX, inputY, Time.deltaTime);
        }


        public void MovePlayer(float inputX, float inputY, float deltaTime)
        {
        
            Vector3 movement = new Vector3(speed.x * inputX, speed.y * inputY, 0);

            movement *= deltaTime;

            transform.Translate(movement);
        }

    }
