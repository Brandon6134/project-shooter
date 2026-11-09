using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    private Vector2 moveInput;
    public float moveSpeed;
    public float groundDamping;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump = true;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask ground;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        //ground check for player, shoot raycast from current position and see if it hits "ground"
        //the length of the raycast is half your player's height, since raycast is from middle point of player, then plus a bit more (~0.2f)
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight*0.5f + 0.2f, ground);

        SpeedControl();

        //handle damping / drag
        if (grounded)
            rb.linearDamping = groundDamping;
        else
            rb.linearDamping = 0f;
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void MovePlayer()
    {
        //calculate movement direction
        moveDirection = orientation.forward * moveInput.y + orientation.right * moveInput.x;

        //on ground
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        
        //in air
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3(rb.linearVelocity.x,rb.linearVelocity.z);

        //limit velocity if needed
        if (flatVelocity.magnitude > moveSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed; //calculate max limited velocity
            rb.linearVelocity = new Vector3(limitedVelocity.x,rb.linearVelocity.y,limitedVelocity.z); //apply limited speed to rb
        }
    }
    
    private void OnJump()
    {
        //when to jump
        if (grounded && readyToJump)
        {
            readyToJump = false;

            //reset y velocity, so always jump same height
            rb.linearVelocity = new Vector3(rb.linearVelocity.x,0f,rb.linearVelocity.z);

            //apply force (impulse cause only apply the force in one instance)
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            
            Invoke(nameof(ResetJump),jumpCooldown); //call reset jump to set readyToJump = true, after xx seconds of jump cooldown
        }
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}
