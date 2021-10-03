using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartSelector : MonoBehaviour
{
    [SerializeField]
    PositionSelector positionSelectorPrefab;

    bool active = false;

    GoodBoyHeadAnchor headAnchor;
    GoodBoyLegAnchor[] legAnchors;

    private void OnEnable()
    {
        BodySelector.OnSelectBodyType += BodySelector_OnSelectBodyType;
    }

    private void OnDisable()
    {
        BodySelector.OnSelectBodyType -= BodySelector_OnSelectBodyType;
    }

    private void BodySelector_OnSelectBodyType(string bodyType, Transform bodyTransform)
    {
        headAnchor = bodyTransform.GetComponentInChildren<GoodBoyHeadAnchor>();
        legAnchors = bodyTransform.GetComponentsInChildren<GoodBoyLegAnchor>();
    }
}
