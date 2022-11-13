using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{

    public float hp = 5;
    float maxhp = 5;
    MainController mainController;
    public Image healthBar;
    public GameObject shot;
    public float wait = 0f;
    public float maxWait;
    public GameObject killParticleSystem;
    ShakeManager shake;

    void Awake()
    {
        GameObject gameManager = GameObject.Find("GameManager");
        mainController = gameManager.GetComponent<MainController>();
        shake = GameObject.Find("CameraShakeManager").GetComponent<ShakeManager>();

        maxhp = mainController.hp;
        hp = mainController.hp;

        maxWait = Random.Range(1, 5);
    }

    void Update()
    {
        wait += Time.deltaTime;
        float hpBar = hp / maxhp;
        healthBar.rectTransform.localScale = new Vector3(hpBar, 1, 1);

        if (hp <= 0)
        {
            GameObject explosion = Instantiate(killParticleSystem, this.transform.position, this.transform.rotation);
            explosion.transform.parent = null;
            shake.CamShake();
            mainController.score += 10f;
            Destroy(this.gameObject);
        }

        if (wait > maxWait)
        {
            if (mainController.best1 == this.gameObject)
            {
                Shooting();
            }
            else if (mainController.best2 == this.gameObject)
            {
                Shooting();
            }
            else if (mainController.best3 == this.gameObject)
            {
                Shooting();
            }
            else if (mainController.best4 == this.gameObject)
            {
                Shooting();
            }
            wait = 0f;
            maxWait = Random.Range(1, 5);
        }

    }

    void Shooting()
    {
        Instantiate(shot, this.transform.position, this.transform.rotation);
    }
}
