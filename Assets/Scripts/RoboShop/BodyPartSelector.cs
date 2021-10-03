using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartSelector : MonoBehaviour
{
    [SerializeField]
    PositionSelector positionSelectorPrefab;
    [SerializeField]
    TMPro.TextMeshProUGUI description;
    [SerializeField]
    GameObject LegsInventory;
    [SerializeField]
    GameObject HeadsInventory;
    [SerializeField]
    RoboShopRotator rotator;

    bool active = false;

    GoodBoyHeadAnchor headAnchor;
    GoodBoyLegAnchor[] legAnchors;

    private void Awake()
    {
        LegsInventory.SetActive(false);
        
    }

    public void SetDescription(string description)
    {
        this.description.text = description;
    }
    
    public void SetLimb(string limb)
    {
        var anchor = anchors[activeSelector];
        anchors.Remove(activeSelector);
        Destroy(activeSelector.gameObject);
        activeSelector = null;
        PlayerPrefs.SetString(anchor.SettingKey, limb);
        PositionSelector.disabled = false;
        if (anchor.BodyPart == BodyPart.Leg)
        {
            LegsInventory.SetActive(false);
        }
        rotator.Enabled();
    }

    private void OnEnable()
    {        
        BodySelector.OnSelectBodyType += BodySelector_OnSelectBodyType;
        PositionSelector.OnSelectLimb += PositionSelector_OnSelectLimb;
    }

    private void OnDisable()
    {
        BodySelector.OnSelectBodyType -= BodySelector_OnSelectBodyType;
        PositionSelector.OnSelectLimb -= PositionSelector_OnSelectLimb;
    }

    Dictionary<PositionSelector, GoodBoyAnchor> anchors = new Dictionary<PositionSelector, GoodBoyAnchor>();
    PositionSelector activeSelector;
    private void PositionSelector_OnSelectLimb(PositionSelector positionSelector)
    {
        activeSelector = positionSelector;
        var anchor = anchors[positionSelector];
        if (anchor.BodyPart == BodyPart.Leg)
        {
            LegsInventory.SetActive(true);
        }
    }

    private void BodySelector_OnSelectBodyType(string bodyType, Transform bodyTransform)
    {
        description.text = "Attach limbs";
        headAnchor = bodyTransform.GetComponentInChildren<GoodBoyHeadAnchor>();
        anchors.Add(Spawn(headAnchor.transform), headAnchor);
        legAnchors = bodyTransform.GetComponentsInChildren<GoodBoyLegAnchor>();
        for (int i = 0, l = legAnchors.Length; i<l; i++)
        {
            anchors.Add(Spawn(legAnchors[i].transform), legAnchors[i]);
        }
        var rb = bodyTransform.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    public PositionSelector Spawn(Transform anchor)
    {
        var positionSelector = Instantiate(positionSelectorPrefab);
        positionSelector.gameObject.SetActive(false);
        positionSelector.transform.position = anchor.position;
        positionSelector.transform.SetParent(anchor.parent.parent, true);
        positionSelector.Configure(anchor.parent.GetComponent<Rigidbody>(), anchor.position);
        positionSelector.gameObject.SetActive(true);
        return positionSelector;
    }
}
