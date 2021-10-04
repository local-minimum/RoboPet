using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField]
    GameObject DeployButton;

    bool active = false;

    GoodBoyHeadAnchor headAnchor;
    GoodBoyLegAnchor[] legAnchors;

    private void Awake()
    {
        GoodBoyInput.HasPower = false;
        LegsInventory.SetActive(false);
        HeadsInventory.SetActive(false);
        DeployButton.SetActive(false);
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
            var leg = ((GoodBoyLegAnchor)anchor).SpawnLeg();
            leg.GetComponent<Rigidbody>().useGravity = false;
            var joint = leg.GetComponent<Joint>();
            joint.connectedMassScale = 0;

        } else if (anchor.BodyPart == BodyPart.Head)
        {
            HeadsInventory.SetActive(false);
            var head = ((GoodBoyHeadAnchor)anchor).SpawnHead();
            head.GetComponent<Rigidbody>().useGravity = false;
            var joint = head.GetComponent<Joint>();
            joint.connectedMassScale = 0;
        }
        if (anchors.Count > 0)
        {
            rotator.Enabled();
        } else
        {
            DeployButton.SetActive(true);
        }
    }

    public void DeployRobot()
    {
        SceneManager.LoadScene("Level 001");
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
        } else if (anchor.BodyPart == BodyPart.Head)
        {
            HeadsInventory.SetActive(true);
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
