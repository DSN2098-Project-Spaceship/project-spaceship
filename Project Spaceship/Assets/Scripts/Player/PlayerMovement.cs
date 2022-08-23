using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Serialized variables
    [SerializeField] float groundSpeed = 5;
    [SerializeField] float groundSpeedMax = 7;
    [SerializeField] float acceleration = 1.5f;
    [SerializeField] float airSpeed = 3;
    [SerializeField] float jumpHeight = 6;
    [SerializeField] float coyoteTime = .125f;

    [Header("Mouse")]
    [SerializeField] float sensitivity = 50;

    [Header("References")]
    [SerializeField] Transform playerCam;

    //public Variables
    public bool grounded;
    public bool isEnabled;

    //Private References 
    CharacterController controller;
    Transform origPlayerCam;

    //Private
    float x, z;
    bool jumping;
    Vector3 moveDirection;
    float coyoteTimeRn;
    float xRotation;
    float speedRn;
    Vector2 groundJumpDir = Vector2.zero;
    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        origPlayerCam = playerCam;
    }

    void Update()
    {
        MyInput();
        Look();
    }

    void FixedUpdate()
    {
        if (isEnabled)
            Movement();
    }

    void MyInput()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        jumping = Input.GetKey(KeyCode.Space);
    }

    void Movement()
    {
        speedRn = Mathf.Abs(x) == 1 || Mathf.Abs(z) == 1 ? Mathf.Lerp(speedRn, groundSpeedMax, Time.deltaTime * acceleration) : groundSpeed;
        if (grounded)
        {
            moveDirection = new Vector3(x * groundSpeed, -.75f, z * speedRn);
            moveDirection = transform.TransformDirection(moveDirection);

            groundJumpDir = new Vector2(x, z);
        }
        else
        {
            moveDirection.x = x * (Mathf.Sign(groundJumpDir.x) == Mathf.Sign(x) && groundJumpDir.x != 0 ? speedRn : airSpeed);
            moveDirection.z = z * (Mathf.Sign(groundJumpDir.y) == Mathf.Sign(z) && groundJumpDir.y != 0 ? speedRn : airSpeed);
            moveDirection = transform.TransformDirection(moveDirection);

            coyoteTimeRn += Time.deltaTime;
        }

        if (jumping && coyoteTimeRn <= coyoteTime)
        {
            moveDirection.y = jumpHeight;
        }

        moveDirection.y -= 20 * Time.deltaTime;
        grounded = (controller.Move(moveDirection * Time.deltaTime) & CollisionFlags.Below) != 0;

        if (grounded)
        {
            coyoteTimeRn = 0;
        }
    }

    private float desiredX;
    float camMin;
    float camMax;
    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.fixedDeltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.fixedDeltaTime;

        //Find current look rotation
        Vector3 rot = playerCam.transform.localRotation.eulerAngles;
        desiredX = rot.y + mouseX;

        //Rotate, and also make sure we dont over- or under-rotate.
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -85f, 85f);

        //If leaning, clamp rotation
        if (Input.GetKeyDown(KeyCode.E))
        {
            camMin = desiredX;
            camMax = desiredX + 30;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            camMin = desiredX - 30;
            camMax = desiredX;
        }
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E))
        {
            desiredX = ClampAngle(desiredX, camMin, camMax);
        }
        //Perform the rotations
        playerCam.transform.localRotation = Quaternion.Euler(xRotation, desiredX, playerCam.eulerAngles.z);
        transform.localRotation = Quaternion.Euler(0, desiredX, 0);
    }

    float ClampAngle(float angle, float min, float max)
    {
        float start = (min + max) * 0.5f - 180;
        float floor = Mathf.FloorToInt((angle - start) / 360) * 360;
        min += floor;
        max += floor;
        return Mathf.Clamp(angle, min, max);
    }
}
