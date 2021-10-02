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
}
