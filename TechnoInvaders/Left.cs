using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xmin = -2.45f;
    public float xmax = 2.45f;
}

public class Left : MonoBehaviour
{
    public GameObject Player;
    Rigidbody2D playerRb;
    public float speed = 5f;
    float width;
    public Boundary boundary;
    public float maxSpeed = 2.5f;
    public float moveVar = 1f;
    public float forceVar = 500f;
    Ctrl control;

    void Start()
    {
        control = Player.GetComponent<Ctrl>();
        width = Screen.width;
        playerRb = Player.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (Input.GetTouch(i).position.x > width / 2)
            {
                if (control.alive)
                {
                    Movement(moveVar);
                }
            }
            if (Input.GetTouch(i).position.x < Screen.width / 2)
            {
                if (control.alive)
                {
                    Movement(-moveVar);
                }
            }
        }



        
        playerRb.position = new Vector2(Mathf.Clamp(playerRb.position.x, -2.2f, 2.2f), playerRb.position.y);




    }

    void Movement(float horizontalInput)
    {

        if (playerRb.velocity.magnitude > maxSpeed)
        {
            playerRb.velocity = playerRb.velocity.normalized * maxSpeed;
        }

        playerRb.AddForce(new Vector2(horizontalInput * (forceVar * Time.deltaTime), 0));

    }
    void FixedUpdate()
    {
        // #if UNITY_EDITOR
        if (control.alive)
        {
            Movement(Input.GetAxis("Horizontal"));
        }
        // #endif
    }
}


