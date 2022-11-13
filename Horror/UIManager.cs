using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Inventory")]
    [SerializeField] Color unselectedItemSpaceColour = Color.gray;
    [SerializeField] Color selectedItemSpaceColour = Color.white;
    [SerializeField] Image[] itemSpaces = new Image[7];
    [SerializeField] GameObject documentViewer;
    public bool showingDocument = false;
    public Text tooltipText;
    [SerializeField] Text endOfTechDemoText;
    private static UIManager _instance;
    public static UIManager Instance { get { return _instance; } }

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
        
    }

    private void Start()
    {
        UpdateInv(0);
    }

    public void UpdateInv(int selectedItemSpace)
    {
        for(int i = 0; i < itemSpaces.Length; i++)
        {
            itemSpaces[i].color = unselectedItemSpaceColour;
            itemSpaces[i].GetComponentInChildren<Text>().text = "";
            if (Inventory.Instance.inventory[i] != null)
            {
                itemSpaces[i].GetComponentInChildren<Text>().text = Inventory.Instance.inventory[i].itemName;
            }
        }
        itemSpaces[selectedItemSpace].color = selectedItemSpaceColour;
    }

    public void ViewDocument(Document document)
    {
        documentViewer.SetActive(true);
        documentViewer.GetComponent<Image>().sprite = document.documentSprite;
        documentViewer.GetComponentInChildren<Typewriter>().StartEffect();
        showingDocument = true;
        FindObjectOfType<PlayerLook>().canLook = false;
        FindObjectOfType<PlayerMovement>().canMove = false;
    }

    public void HideDocument()
    {
        documentViewer.SetActive(false);
        showingDocument = false;
        FindObjectOfType<PlayerLook>().canLook = true;
        FindObjectOfType<PlayerMovement>().canMove = true;
    }

    public void ShowEndText()
    {
        endOfTechDemoText.enabled = true;
    }
}