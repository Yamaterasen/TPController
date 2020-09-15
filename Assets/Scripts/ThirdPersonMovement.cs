using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;
using System;

public class ThirdPersonMovement : MonoBehaviour
{
    public event Action Idle = delegate { };
    public event Action StartRunning = delegate { };
    public event Action Jumping = delegate { };
    public event Action Landing = delegate { };
    public event Action Sprint = delegate { };
    public event Action Lift = delegate { };

    [SerializeField] CharacterController _controller = null;
    [SerializeField] Transform _cam = null;


    public float speed = 10f;
    Vector3 velocity;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    bool _isMoving = false;
    bool isGrounded;
    bool isSprinting;

    private void Start()
    {
        Idle?.Invoke();
    }

    void Update()
    {
        //Ground Check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= 0.1f)
        {
            CheckIfStartedMoving();
            //get angle according to camera
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            _controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        else
        {
            CheckIfStoppedMoving();
        }

        //Checking if player is grounded and has pressed the jump button
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            Jumped();
        }

        //Play landing animation if not moving
        if (isGrounded == false && velocity.y < -2f)
        {
            Landed();
            if (isSprinting == true)
            {
                Sprinting();
            }
            else
            {
                CheckIfStoppedMoving();
            }
        }

        //Start sprint if grounded and moving
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded && _isMoving)
        {
            Sprinting();
        }

        //End sprint when shift is let go
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = false;
            CheckIfStoppedMoving();
            speed = 10f;
        }

        if(Input.GetMouseButtonDown(0) && isGrounded)
        {
            Lifting();
            CheckIfStoppedMoving();
        }

        //Apply gravity over time
        velocity.y += gravity * Time.deltaTime;
        _controller.Move(velocity * Time.deltaTime);
    }


    private void CheckIfStartedMoving()
    {
        if (_isMoving == false && isGrounded == true)
        {
            //our velocity says wer're moving but we previously were not
            //this means we've started moving!
            StartRunning?.Invoke();
            Debug.Log("Started");
        }
        _isMoving = true;
    }

    private void CheckIfStoppedMoving()
    {
        if(_isMoving == true && isGrounded == true)
        {
            //our velocity says we're not moving, but we were
            //this means we've stopped!
            Idle?.Invoke();
            Debug.Log("Stopped");
        }
        _isMoving = false;
    }

    private void Jumped()
    {
        Jumping?.Invoke();
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    private void Landed()
    {
        Landing?.Invoke();
    }

    private void Sprinting()
    {
        isSprinting = true;
        speed = 15f;
        Sprint?.Invoke();
    }

    private void Lifting()
    {
        Lift?.Invoke();
    }
}
