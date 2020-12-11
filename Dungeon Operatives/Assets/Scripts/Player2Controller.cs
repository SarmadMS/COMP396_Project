using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class Player2Controller : MonoBehaviour
{
    public int health = 100;
    public bool isDead;

    //FPS Movement Variables
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    Vector3 velocity;
    bool isGrounded;
    private Rigidbody rbody;

    //FPS Camera Variables
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    float xRotation = 0f;

    //Sprinting Variables
    private int sprintLimit = 5;
    private float runningSpeed = 10f;
    private bool sprinting = false;
    private bool isSprintingReady = true;
    private float sprintStartTime;

    private SplitScreenController splitScreenController;

    [SerializeField] public AudioSource mainCameraAudioSource;

    [System.Serializable]
    public class soundClips
    {
        public AudioClip p1_Watch_where;
        public AudioClip p1_Hey_knock_it_off;
        public AudioClip p1_Are_you_blind;
    }
    public soundClips SoundClips;

    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        splitScreenController = GameObject.Find("SplitScreen").GetComponent<SplitScreenController>();

    }

    // Update is called once per frame
    void Update()
    {
        //FPS Camera Function
        float mouseX = Input.GetAxis("P2 Look X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("P2 Look Y") * mouseSensitivity * Time.deltaTime;

        xRotation += mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        //Movement Function
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("P2 Move X");
        float z = Input.GetAxis("P2 Move Y");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        //Jump Function
        if (Input.GetButtonDown("P2 Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        // Sprinting
        if (isSprintingReady == true && Input.GetButtonUp("P2 Sprint") && z > 0)
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

    public int GetHealth()
    {
        return health;
    }

    public void DamagePlayer(int damage)
    {
        if (health > 0)
        {
            health -= damage;
        }
        else
        {
            Dead();
        }
    }

    public void Dead()
    {
        splitScreenController.RespawnPlayer2();
        health = 0;
        isDead = true;
        Debug.Log("Player is Dead");
        Destroy(GameObject.Find("Dungeon Operative 2"));
    }
}