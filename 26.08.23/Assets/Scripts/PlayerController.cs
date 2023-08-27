using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    CharacterController controller;

    public float movementSpeed = walkSpeed;              
    public float rotationSpeed = 10f;

    private Rigidbody rb;

    Animator anim;                                  
    private Transform cam;

    private const float walkSpeed = 1.5f;
    private const float runSpeed = 4.0f;

    //bool isGrounded = true;

    private void Start()
    {
        anim = GetComponent<Animator>();                
        controller = GetComponent<CharacterController>();
        cam = Camera.main.transform; 
    }
    private void Update()
    {

        float hzInput = Input.GetAxisRaw("Horizontal"); 
        float vInput = Input.GetAxisRaw("Vertical");

        if (hzInput != 0 || vInput != 0) 
        {
            
            anim.SetFloat("Walk", 1f);

            
            Vector3 camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
            Vector3 moveDirection = (vInput * camForward + hzInput * cam.right).normalized;

            
          
            controller.Move(moveDirection * movementSpeed * Time.deltaTime);

            
            if (moveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

        }
        else
        {
            anim.SetFloat("Walk", 0);
        }

        if (Input.GetKey(KeyCode.LeftShift)) {
            // Set current speed to run if shift is down
            anim.SetBool("Sprint", true);
            movementSpeed = runSpeed;
        }
        else
        {
            // Otherwise set current speed to walking speed
            anim.SetBool("Sprint", false);
            movementSpeed = walkSpeed;
        }

        if (Input.GetKey(KeyCode.C))
        {
            anim.SetTrigger("Crouch");
        }

        //if (Input.GetKey(KeyCode.C))
        //{
        //    anim.SetInteger("Crouch",1);
        //}
        //else
        //{
        //    anim.SetInteger("Crouch",0);
        //}


        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("Attack", true);
        }
        if (Input.GetMouseButtonUp(0))
        {
            anim.SetBool("Attack", false);
        }
    }
}

