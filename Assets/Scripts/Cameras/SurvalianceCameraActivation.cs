using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvalianceCameraActivation : MonoBehaviour
{
    [SerializeField]
    SurvalianceCamera survalianceCamera;
    Collider trigger;

    private void Start()
    {        
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
