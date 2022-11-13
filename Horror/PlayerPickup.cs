using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    Transform playerCameraTransform;
    
    [SerializeField] float dropDist = 2f;
    [SerializeField] float interactDist = 3f;
    Tooltip tooltip;
    RaycastHit hit;

    void Awake()
    {
        playerCameraTransform = Camera.main.transform;
    }

    void Update()
    {
        InteractRaycast.Instance.RayHit(out hit);
        Tooltips();
        Interaction();
    }

    void Tooltips()
    {
        if(hit.collider != null)
        { 
            if(hit.transform.GetComponent<Tooltip>())
            {
                tooltip = hit.transform.GetComponent<Tooltip>();
                tooltip.ShowText();
            }
            else if (tooltip != null)
            {
                tooltip.HideText();
                tooltip = null;
            }
        }
        else if (tooltip != null)
        {
            tooltip.HideText();
            tooltip = null;
        }
    }

    void Interaction()
    {
        if (Input.GetButtonDown("Interact"))
        {
            bool toggledDoc = false;
            if (UIManager.Instance.showingDocument)
            {
                UIManager.Instance.HideDocument();
                toggledDoc = true;
            }
            if (hit.collider != null)
            {
                if (hit.transform.GetComponent<HoldableItem>())
                {
                    PickupItem(hit.transform);
                }
                else if (hit.transform.GetComponent<Document>() && !toggledDoc)
                {
                    UIManager.Instance.ViewDocument(hit.transform.GetComponent<Document>());
                }
            }
        }
        if (Input.GetButtonDown("Drop") && Inventory.Instance.inventory[Inventory.Instance.selectedItem] != null)
        {
            DropHeldItem();
        }
    }

    public void DropHeldItem()
    {
        if(Inventory.Instance.inventory[Inventory.Instance.selectedItem] != null)
        {
            GameObject item = Instantiate(Inventory.Instance.inventory[Inventory.Instance.selectedItem].itemPrefab, transform.position, Quaternion.identity);
            item.transform.parent = transform;
            //i could do it without making it a parent, but my brains mush at the moment
            item.transform.localPosition = Vector3.forward * dropDist;
            item.transform.parent = null;
            Inventory.Instance.RemoveFromInventory(Inventory.Instance.selectedItem);
            UIManager.Instance.UpdateInv(Inventory.Instance.selectedItem);
        }
    }

    void PickupItem(Transform item)
    {
        Inventory.Instance.AddToInventory(item.GetComponent<HoldableItem>());
        UIManager.Instance.UpdateInv(Inventory.Instance.selectedItem);
        if(tooltip != null)
        {
            tooltip.HideText();
            tooltip = null;
        }
        Destroy(item.gameObject);
    }

    
}
