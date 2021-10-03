using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryOption : MonoBehaviour
{
    BodyPartSelector bodyPartSelector;
    Button btn;
    public string decription;
    public string limb;
    

    private void Awake()
    {
        bodyPartSelector = GetComponentInParent<BodyPartSelector>();
        btn = GetComponent<Button>();        
    }

    public void ShowDescription() {
        bodyPartSelector.SetDescription(decription);
    }

    public void HideDescription()
    {
        bodyPartSelector.SetDescription("");
    }

    public void Click()
    {
        btn.interactable = false;
        bodyPartSelector.SetDescription("");
        bodyPartSelector.SetLimb(limb);
    }
}
