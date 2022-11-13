using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ctrl : MonoBehaviour
{

    public float speed = 1f;
    public float timer;
    public float shotDelay = 1f;
    public GameObject shotPrefab;
    public GameObject controller;
    MainController mainController;
    public Image hpImage;
    public float hp = 100f;
    public float maxHp = 100f;
    float hpBar;
    public bool alive = true;
    public float wait = 0f;
    public float maxWait = 2f;
    SpriteRenderer spriteRenderer;
    public Sprite explosion;
    public Sprite player;
    public bool left = false;
    public bool right = false;


    void Start()
    {
        mainController = controller.GetComponent<MainController>();
        hp = maxHp;
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();


    }
    void Update()
    {
        if (alive)
        {
            Movement();
            Shooting();
            Health();
        }
        else
        {
            wait += Time.deltaTime;
            if (wait >= maxWait)
            {

                wait = 0;
                hp = maxHp;
                spriteRenderer.sprite = player;
                mainController.lives--;
                int x = 0;
                for (int i = 0; i < (mainController.hearts.Count - 1); i++)
                {
                    x++;
                }
                Destroy(mainController.hearts[x]);
                alive = true;

            }
            else
            {

                spriteRenderer.sprite = explosion;
            }
        }



    }


    public void Left()
    {
        if (this.transform.position.x > -2.45f)
        {
            left = true;
        }

    }
    public void Right()
    {
        if (this.transform.position.x < 2.45f)
        {
            right = true;
        }
    }

    public void Unpressed()
    {
        speed = 0;
        left = false;
        right = false;
    }
    void Movement()
    {
        // float isMoving = Input.GetAxis("Horizontal");
        // if (this.transform.position.x < -2.45 && isMoving < 0)
        // {
        //     isMoving = 0;
        // }
        // else if (this.transform.position.x > 2.45 && isMoving > 0)
        // {
        //     isMoving = 0;
        // }
        // this.transform.position += Vector3.right * isMoving * (speed * Time.deltaTime);



    }

    void Shooting()
    {
        timer += Time.deltaTime;
        if (timer > shotDelay)
        {
            Instantiate(shotPrefab, this.transform.position, this.transform.rotation);
            timer = 0f;
        }
    }

    void Health()
    {

        hpBar = hp / maxHp;
        hpImage.rectTransform.localScale = new Vector3(hpBar, 1, 1);
        if (hp <= 0)
        {
            alive = false;
        }


    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Shot 1(Clone)")
        {
            hp -= 5f;
            Destroy(collision.gameObject);

        }
    }

}
