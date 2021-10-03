using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void LimbSelectEvent(PositionSelector positionSelector);

public class PositionSelector : MonoBehaviour
{
    public static bool disabled;

    public static event LimbSelectEvent OnSelectLimb;

    const string EMISSION_COLOR = "_EmissionColor";

    bool mouseOver = false;

    Color defaultColor;

    [SerializeField]
    Color highlightColor;

    [SerializeField]
    Color disabledColor;

    MeshRenderer mr;
    FixedJoint joint;


    public void Configure(Rigidbody anchorBody, Vector3 anchorWorldPosition)
    {
        joint.connectedBody = anchorBody;
        joint.connectedAnchor = anchorBody.transform.InverseTransformPoint(anchorWorldPosition);                 
    }

    private void Awake()
    {
        joint = GetComponent<FixedJoint>();        
        joint.autoConfigureConnectedAnchor = false;
        joint.enableCollision = false;
        mr = GetComponent<MeshRenderer>();
        defaultColor = mr.material.GetColor(EMISSION_COLOR);
    }

    private void OnMouseEnter()
    {
        mouseOver = true;
        if (!disabled) { 
            mr.material.SetColor(EMISSION_COLOR, highlightColor);
        }
    }

    private void OnMouseExit()
    {
        mouseOver = false;
        if (!disabled)
        {
            mr.material.SetColor(EMISSION_COLOR, defaultColor);
        }        
    }

    private void Update()
    {
        if (disabled)
        {
            mr.material.SetColor(EMISSION_COLOR, disabledColor);            
        } else if (Input.GetMouseButtonDown(0) && mouseOver)
        {
            disabled = true;
            OnSelectLimb?.Invoke(this);
        }
    }
}
