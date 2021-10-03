﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodBoy : MonoBehaviour
{

    Transform _trackingPosition;

    [SerializeField]
    GoodBoyLegAnchor[] legAnchors;
    [SerializeField]
    GoodBoyHeadAnchor headAnchor;

    public Transform trackingPosition
    {
        get
        {
            return _trackingPosition;
        }
    }

    public static GoodBoy instance { get; private set; }

    public static float distanceToObjective
    {
        get
        {
            var offset = Objective.instance.transform.position - instance.trackingPosition.position;
            return offset.magnitude;
        }
    }


    public static float GetSqDistanceTo(Transform other)
    {
        return (other.position - instance.trackingPosition.position).sqrMagnitude;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
        GoodBoyInput.HasPower = true;
        for (int i=0, l=legAnchors.Length; i<l; i++)
        {
            legAnchors[i].SpawnLeg();
        }

        var head = headAnchor.SpawnHead();        
        _trackingPosition = head.transform;                    
    }

    private void OnDestroy()
    {
        if (instance == this) instance = null;
    }


    public void PowerDown()
    {
        GoodBoyInput.HasPower = true;
    }
}
