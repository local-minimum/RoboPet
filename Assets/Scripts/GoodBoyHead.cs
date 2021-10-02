using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodBoyHead : MonoBehaviour
{
    [SerializeField]
    Vector3 anchorOffset;

    public Vector3 AnchorOffset
    {
        get { return anchorOffset; }
    }
}
