using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvalianceCameraActivation : MonoBehaviour
{
    SurvalianceCamera survalianceCamera;
    Collider trigger;

    private void Start()
    {
        survalianceCamera = transform.parent.GetComponentInChildren<SurvalianceCamera>();
        trigger = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GoodBoy")
        {            
            survalianceCamera.RequestAcitvate(trigger);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "GoodBoy")
        {
            survalianceCamera.RequestDeactivate(trigger);
        }
    }
}
