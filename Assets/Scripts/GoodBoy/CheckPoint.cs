using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField]
    Transform lookTarget;

    public Vector3 LookTarget
    {
        get
        {
            return lookTarget.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GoodBoy")
        {
            GoodBoySpawner.instance.CheckPoint = this;
        }
    }

}
