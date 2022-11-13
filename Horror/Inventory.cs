using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Item[] inventory = new Item[7];
    internal int selectedItem = 0;
    private static Inventory _instance;
    public static Inventory Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        selectedItem = 0;
    }

    private void Update()
    {
        ItemSelectInput();
    }

    public void AddToInventory(HoldableItem item)
    {
        for(int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                inventory[i] = item.item;
                break;
            }
        }
    }

    public void RemoveFromInventory(HoldableItem item)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == item.item)
            {
                inventory[i] = null;
                break;
            }
        }
    }

    public void RemoveFromInventory(int index)
    {
        inventory[index] = null;
    }

    void ItemSelectInput()
    {
        int oldSelectedItem = selectedItem;
        //keyboard inputs (i hate it but i dont know a better way of doing this :/)
        if (Input.GetKeyDown(KeyCode.Alpha1))
            selectedItem = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            selectedItem = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3))
            selectedItem = 2;
        if (Input.GetKeyDown(KeyCode.Alpha4))
            selectedItem = 3;
        if (Input.GetKeyDown(KeyCode.Alpha5))
            selectedItem = 4;
        if (Input.GetKeyDown(KeyCode.Alpha6))
            selectedItem = 5;
        if (Input.GetKeyDown(KeyCode.Alpha7))
            selectedItem = 6;
        //controller inputs
        if (Input.GetButtonDown("NextItem"))
        {
            ++selectedItem;
            if (selectedItem > inventory.Length - 1)
                selectedItem = 0;
        }
        if (Input.GetButtonDown("PreviousItem"))
        {
            --selectedItem;
            if (selectedItem < 0)
                selectedItem = inventory.Length - 1;
        }

        if (selectedItem != oldSelectedItem)
        {
            UIManager.Instance.UpdateInv(selectedItem);
        }
    }
}
