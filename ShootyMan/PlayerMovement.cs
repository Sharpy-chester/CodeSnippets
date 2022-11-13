using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float playerSpeed;
    float movementMultiplier = 10f;
    float xDir;
    float yDir;
    Vector3 moveDirection;
    Rigidbody2D rb;
    [SerializeField] Joystick joystickMove;
    [SerializeField] Joystick joystickLook;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }


    void Update()
    {

        /*xDir = Input.GetAxis("Horizontal");
        yDir = Input.GetAxis("Vertical");*/
        xDir = joystickMove.Horizontal;
        yDir = joystickMove.Vertical;
        moveDirection = new Vector2(xDir, yDir);
        Rotation();
    }

    private void FixedUpdate()
    {
        if(xDir != 0 && yDir != 0)
        {
            Movement();
        }
        
    }

    void Movement()
    {
        rb.AddForce(moveDirection * playerSpeed, ForceMode2D.Force);
    }

    void Rotation()
    {
        if (joystickLook.Horizontal != 0 || joystickLook.Vertical != 0)
        {
            transform.right = new Vector2(joystickLook.Horizontal, joystickLook.Vertical);
        }
        else if (xDir != 0 && yDir != 0)
        {
            transform.right = moveDirection;
        }
    }
}
