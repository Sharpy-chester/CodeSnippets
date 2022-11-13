using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    [SerializeField] Text interactTxt;
    public string textToShowKeyboard = "Press E to pick up";
    public string textToShowController = "Press X to pick up";

    private void Start()
    {
        interactTxt = UIManager.Instance.tooltipText;
    }

    public void ShowText()
    {
        interactTxt.gameObject.SetActive(true);
        print(Input.GetJoystickNames().Length);
        if (Input.GetJoystickNames().Length > 0)
            interactTxt.text = textToShowController;
        else
            interactTxt.text = textToShowKeyboard;
        
            
    }

    public void HideText()
    {
        interactTxt.gameObject.SetActive(false);
    }
}
