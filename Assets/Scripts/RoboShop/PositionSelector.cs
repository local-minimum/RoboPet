using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionSelector : MonoBehaviour
{
    const string EMISSION_COLOR = "_EmissionColor";

    bool mouseOver = false;

    Color defaultColor;

    [SerializeField]
    Color highlightColor;

    MeshRenderer mr;

    private void Start()
    {
        mr = GetComponent<MeshRenderer>();
        defaultColor = mr.material.GetColor(EMISSION_COLOR);
    }
    private void OnMouseEnter()
    {
        mouseOver = true;
        mr.material.SetColor(EMISSION_COLOR, highlightColor);
        Debug.Log(name);
    }

    private void OnMouseExit()
    {
        mouseOver = false;
        mr.material.SetColor(EMISSION_COLOR, defaultColor);
    }


}
