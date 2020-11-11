using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    private Rigidbody rbody;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    private float walkingSpeed = 5f;
    private int sprintLimit = 5;
    private float runningSpeed = 10f;
    private bool sprinting = false;
    private bool isSprintingReady = true;
    private float sprintStartTime;


    void Start()
    {
        rbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("P1 Move X");
        float z = Input.GetAxis("P1 Move Y");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);



        //Jump
        if(Input.GetButtonDown("P1 Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);



        // Sprinting
        if (isSprintingReady == true && Input.GetButtonUp("P1 Sprint") && z > 0)
        {
            speed += 5f;
            isSprintingReady = false;
            sprinting = true;
            sprintStartTime = Time.realtimeSinceStartup;
        }
        else if (sprinting == true && z <= 0 && (Time.realtimeSinceStartup - sprintStartTime <= sprintLimit))
        {
            speed -= 5f;
            sprinting = false;
            Invoke("SprintingCooldown", Time.realtimeSinceStartup - sprintStartTime);
        }
        else if (sprinting == true && (Time.realtimeSinceStartup - sprintStartTime >= sprintLimit))
        {
            speed -= 5f;
            sprinting = false;
            Invoke("SprintingCooldown", sprintLimit);
        }

        if (sprinting == false)
        {
            rbody.velocity = new Vector3(x * speed, rbody.velocity.y, z * speed);
        }
        else if (sprinting == true)
        {
            rbody.velocity = new Vector3(x * runningSpeed, rbody.velocity.y, z * speed);
        }
    }
    
    void SprintingCooldown()
    {
        isSprintingReady = true;
    }
}
