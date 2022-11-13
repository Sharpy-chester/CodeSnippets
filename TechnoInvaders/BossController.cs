using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{

    Vector3 finalPos = new Vector3(0, 1, 0);
    public float moveSpeed = 1f;
    public float hp;
    public float maxhp = 30;
    public Image hpGO;
    public GameObject[] guns;
    float cooldown = .5f;
    float maxCooldown = .5f;
    public GameObject bullet;
    int gunNum;
    MainController mainController;
    GameObject gameManager;
    ShakeManager shake;

    void Awake()
    {
        hp = maxhp;
        gunNum = guns.Length;
        cooldown = maxCooldown;
        gameManager = GameObject.Find("GameManager");
        mainController = gameManager.GetComponent<MainController>();
        shake = GameObject.Find("CameraShakeManager").GetComponent<ShakeManager>();
    }

    void Update()
    {
        if (this.transform.position.y > finalPos.y)
        {
            this.transform.Translate(0, (-moveSpeed * Time.deltaTime), 0);
        }
        Mathf.Clamp(this.transform.position.y, 1, 8);

        float hpBar = hp / maxhp;
        hpGO.rectTransform.localScale = new Vector3(hpBar, 1, 1);

        if (hp <= 0)
        {
            shake.CamShake();
            mainController.bossKill = true;
            Destroy(this.gameObject);
        }
        Shooting();
    }

    void Shooting()
    {
        cooldown -= Time.deltaTime;
        if (cooldown <= 0)
        {
            int rand = Random.Range(0, gunNum);
            Vector3 instaPos = guns[rand].transform.position;
            Instantiate(bullet, instaPos, this.gameObject.transform.rotation);
            cooldown = maxCooldown;
        }



    }
}
