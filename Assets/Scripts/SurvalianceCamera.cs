using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraMode { Static, Tracking, Sweaping };

public class SurvalianceCamera : MonoBehaviour
{
    public CameraMode cameraMode = CameraMode.Static;

    private Quaternion startRotation;
    public bool claimMainCamera;   
    public Transform sweapingEdgeStart;
    public Transform sweapingEdgeEnd;
    public float sweapDuration = 4f;

    private void Start()
    {
        startRotation = transform.rotation;
        if (claimMainCamera)
        {
            CameraDirector.instance.ActivateCamera(this);
        } else
        {
            gameObject.SetActive(false);
        }
    }
    private void Update()
    {
       switch (cameraMode)
        {
            case CameraMode.Static:
                transform.rotation = startRotation;
                return;
            case CameraMode.Tracking:                
                transform.rotation = Quaternion.LookRotation(GoodBoy.instance.trackingPosition.position - transform.position);
                return;
            case CameraMode.Sweaping:
                var t = Time.timeSinceLevelLoad % (2 * sweapDuration);
                t = Mathf.Abs((t - sweapDuration) / sweapDuration);
                var target = Vector3.Lerp(sweapingEdgeEnd.position, sweapingEdgeStart.position, t);
                transform.rotation = Quaternion.LookRotation(target - transform.position);
                return;
        }
    }

    private HashSet<Collider> activatedTriggers = new HashSet<Collider>();

    public void RequestAcitvate(Collider trigger)
    {
        activatedTriggers.Add(trigger);
        CameraDirector.instance.ActivateCamera(this);
    }

    public void RequestActivate()
    {
        CameraDirector.instance.ActivateCamera(this);
    }

    public void RequestDeactivate(Collider trigger)
    {
        activatedTriggers.Remove(trigger);
        if (activatedTriggers.Count == 0)
        {
            CameraDirector.instance.DeactivateCamera(this);
        }
    }
}
