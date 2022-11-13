using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Item itemNeeded;
    [SerializeField] bool hasTooltip = true;
    [SerializeField] string tooltipStr = "Looks like its locked";
    Animator anim;
    string keyboardTT;
    string controllerTT;


    void Awake()
    {
        anim = GetComponentInParent<Animator>();
        keyboardTT = GetComponent<Tooltip>().textToShowKeyboard;
        controllerTT = GetComponent<Tooltip>().textToShowController;
    }

    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            RaycastHit hit;
            if (InteractRaycast.Instance.RayHit(out hit))
            {
                if (hit.transform.gameObject == gameObject)
                {
                    if (Inventory.Instance.inventory[Inventory.Instance.selectedItem] == itemNeeded || itemNeeded == null)
                    {
                        OpenDoor();
                    }
                }
            }
        }
        if(itemNeeded != null && hasTooltip)
        {
            if (Inventory.Instance.inventory[Inventory.Instance.selectedItem] != itemNeeded)
            {
                GetComponent<Tooltip>().textToShowKeyboard = tooltipStr;
                GetComponent<Tooltip>().textToShowController = tooltipStr;
            }
            else
            {
                GetComponent<Tooltip>().textToShowKeyboard = keyboardTT;
                GetComponent<Tooltip>().textToShowController = controllerTT;
            }
        }
    }

    void OpenDoor()
    {
        anim.SetTrigger("OpenDoor");
        itemNeeded = null;
        UIManager.Instance.ShowEndText();
    }
}
