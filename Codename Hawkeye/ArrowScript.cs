using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowScript : MonoBehaviour
{
    public List<GameObject> Arrow_Prefab;
    public float Arrow_Speed;
    public float Arrow_LifeTime;

    [SerializeField] ArrowType selectedArrowType;

    GameObject newArrow;

    [Header("ArrowUI")]
    [SerializeField] GameObject ArrowUI;
    [SerializeField] Sprite NormalArrowUI;
    [SerializeField] Sprite FireArrowUI;
    [SerializeField] Sprite IceArrowUI;
    public float fireRate;
    [SerializeField] float cooldown = 0;

    enum ArrowType
    {
        normal,
        ice,
        fire
    }

    void Awake()
    {
        selectedArrowType = ArrowType.normal;
    }

    void Update()
    {
        cooldown += Time.deltaTime;
        if (Input.GetButtonDown("Fire2"))
        {
            selectedArrowType++;
            if (selectedArrowType == (ArrowType)(System.Enum.GetValues(typeof(ArrowType)).Length))
            {
                selectedArrowType = 0;
            }

            if (ArrowUI != null)
            {
                //print("test123");
                switch (selectedArrowType)
                {
                    case ArrowType.normal:
                        ArrowUI.GetComponent<Image>().sprite = NormalArrowUI;
                        break;
                    case ArrowType.ice:
                        ArrowUI.GetComponent<Image>().sprite = IceArrowUI;
                        break;
                    case ArrowType.fire:
                        ArrowUI.GetComponent<Image>().sprite = FireArrowUI;
                        break;
                    default:
                        break;
                }
            }
        }

        if (Input.GetButtonDown("Fire1") && fireRate < cooldown) 
        {
            switch (selectedArrowType)
            {
                case ArrowType.normal:
                    newArrow = Instantiate(Arrow_Prefab[0], transform);
                    break;
                case ArrowType.ice:
                    newArrow = Instantiate(Arrow_Prefab[1], transform);
                    break;
                case ArrowType.fire:
                    newArrow = Instantiate(Arrow_Prefab[2], transform);
                    break;
                default:
                    newArrow = Instantiate(Arrow_Prefab[0], transform);
                    break;
            }

            newArrow.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(-Arrow_Speed, 0));
            newArrow.transform.parent = null;
            newArrow.transform.localScale = new Vector3(-1, 1, 1);
            Destroy(newArrow, Arrow_LifeTime);
            cooldown = 0;
        }
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.collider.CompareTag("Enemy"))
    //    {
    //        Destroy(Game);
    //    }        
    //}


}
