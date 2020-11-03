using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float walkingSpeed = 5f;
    [SerializeField] private float runningSpeed = 10f;
    [SerializeField] private int sprintLimit = 5;
    [SerializeField] private float jumpForce = 30f;
    [SerializeField] private float cameraRotation = 10f;
    [SerializeField] private float bodyRotation = 10f;

    private float leftJoystickX;
    private float leftJoystickY;
    private float rightJoystickX;
    private float rightJoystickY;
    private bool sprinting = false;
    private bool isSprintingReady = true;
    private float sprintStartTime;
    private float bodyAngle;
    private float headAngle;
    private float lookX = 0f;
    private float lookY = 0f;


    private Rigidbody rbody;
    private Transform playerHead;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        playerHead = transform.Find("Camera").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        leftJoystickX = Input.GetAxis("P1 Move X");
        leftJoystickY = Input.GetAxis("P1 Move Y");
        rightJoystickX = Input.GetAxis("P1 Look X");
        rightJoystickY = Input.GetAxis("P1 Look Y");

        // Look
        //Debug.Log(Input.GetAxis("P1 Look X") + " " + Input.GetAxis("P1 Look Y"));     
        if (rightJoystickX > lookX)
        {
            lookX = rightJoystickX;
            bodyAngle += transform.rotation.y + (lookX * bodyRotation);
        }
        else if (rightJoystickX < lookX || rightJoystickX == 0)
        {
            lookX = 0;
        }

        if (rightJoystickX < lookX)
        {
            lookX = rightJoystickX;
            bodyAngle += transform.rotation.y + (lookX * bodyRotation);
        }
        else if (rightJoystickX > lookX || rightJoystickX == 0)
        {
            lookX = 0;
        }        
        transform.rotation = Quaternion.Euler(transform.rotation.x, bodyAngle, transform.rotation.z);

        if (rightJoystickY > lookY)
        {
            lookY = rightJoystickY;
            headAngle += transform.rotation.x + (lookY * cameraRotation);
        }
        else if (rightJoystickY < lookY || rightJoystickY == 0)
        {
            lookY = 0;
        }

        if (rightJoystickY < lookY)
        {
            lookY = rightJoystickY;
            headAngle += transform.rotation.x + (lookY * cameraRotation);
        }
        else if (rightJoystickY > lookY || rightJoystickY == 0)
        {
            lookY = 0;
        }
        playerHead.transform.rotation = Quaternion.Euler(Mathf.Clamp(headAngle, -45, 45), bodyAngle, transform.rotation.z);
        
               

        // Jump
        if (Input.GetButtonDown("P1 Jump"))
        {
            Debug.Log("jump");
            rbody.AddForce(0, jumpForce, 0, ForceMode.Impulse);
        }

        // Sprinting
        if (isSprintingReady == true && Input.GetButtonUp("P1 Sprint") && leftJoystickY > 0)
        {
            isSprintingReady = false;
            sprinting = true;
            sprintStartTime = Time.realtimeSinceStartup;
        }
        else if (sprinting == true && leftJoystickY <= 0 && (Time.realtimeSinceStartup - sprintStartTime <= sprintLimit))
        {
            sprinting = false;
            Invoke("SprintingCooldown", Time.realtimeSinceStartup - sprintStartTime);
        }
        else if (sprinting == true && (Time.realtimeSinceStartup - sprintStartTime >= sprintLimit))
        {
            sprinting = false;
            Invoke("SprintingCooldown", sprintLimit);
        }

        if (sprinting == false)
        {
            rbody.velocity = new Vector3(leftJoystickX * walkingSpeed, rbody.velocity.y, leftJoystickY * walkingSpeed);
        }
        else if (sprinting == true)
        {
            rbody.velocity = new Vector3(leftJoystickX * runningSpeed, rbody.velocity.y, leftJoystickY * runningSpeed);
        }
    }

    void SprintingCooldown()
    {
        isSprintingReady = true;
    }
}
