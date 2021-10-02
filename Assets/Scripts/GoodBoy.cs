using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodBoy : MonoBehaviour
{
    [SerializeField]
    private Transform _trackingPosition;

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
        }
    }

    private void OnDestroy()
    {
        if (instance == this) instance = null;
    }
}
