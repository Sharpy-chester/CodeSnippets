using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    [SerializeField] GameObject arrowPrefab;
    [SerializeField] GameObject bow;
    internal float angle;

    void Awake()
    {
        
    }

    void Update()
    {
        LookAtMouse();
    }

    void LookAtMouse()
    {
        Vector2 mouse = Input.mousePosition;
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(bow.transform.position);
        Vector2 offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);
        //Debug.Log(screenPoint.x);
        //Debug.Log(screenPoint.y);
        angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        bow.transform.rotation = Quaternion.Euler(0, 0, angle);

        
    }
}