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
    public int startStock;
    int stock;
    TMPro.TextMeshProUGUI stockText;

    private void Awake()
    {
        stock = startStock;
        bodyPartSelector = GetComponentInParent<BodyPartSelector>();        
        btn = GetComponent<Button>();
        btn.interactable = stock > 0;
        stockText = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        if (stockText != null) stockText.text = stock.ToString();
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
        stock -= 1;
        if (stockText != null)
        {
            stockText.text = stock.ToString();
        }
        btn.interactable = stock > 0;
        bodyPartSelector.SetDescription("");
        bodyPartSelector.SetLimb(limb);
    }
}
