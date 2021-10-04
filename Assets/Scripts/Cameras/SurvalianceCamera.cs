using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum CameraMode { Static, Tracking, Sweaping };

public class SurvalianceCamera : MonoBehaviour
{
    static SurvalianceCamera fallbackCamera;
    static List<SurvalianceCamera> cameras = new List<SurvalianceCamera>();

    public static SurvalianceCamera HighestPriorityCamera
    {
        get
        {
            var best = cameras
                .Where(cameras => cameras.Priority > 0)
                .OrderBy(camera => camera.Priority)
                .FirstOrDefault();
            return best != null ? best : fallbackCamera;
        }
    }

    public CameraMode cameraMode = CameraMode.Static;

    private Quaternion startRotation;
    [SerializeField]
    private bool claimMainCamera;
    [SerializeField]
    private Transform sweapingEdgeStart;
    [SerializeField]
    private Transform sweapingEdgeEnd;
    [SerializeField]
    private float sweapDuration = 4f;
    [SerializeField]
    private string cameraName;

    public string[] info
    {
        get
        {
            return new string[2]
            {
                cameraName,
                cameraMode.ToString(),
            };
        }
    }

    private void Start()
    {
        startRotation = transform.rotation;
        if (claimMainCamera)
        {
            fallbackCamera = this;            
        } else
        {
            gameObject.SetActive(false);
        }
        cameras.Add(this);
        gameObject.SetActive(false);
    }

    private void Update()
    {       
       switch (cameraMode)
        {
            case CameraMode.Static:
                transform.rotation = startRotation;
                return;
            case CameraMode.Tracking:
                if (GoodBoy.instance != null)
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
    }

    public void RequestDeactivate(Collider trigger)
    {
        activatedTriggers.Remove(trigger);
    }

    public float Priority
    {
        get
        {
            return activatedTriggers.Count > 0 ? GoodBoy.GetSqDistanceTo(transform) : 0;
        }
    }

    private void OnDestroy()
    {
        if (fallbackCamera == this) { fallbackCamera = null; }
        cameras.Remove(this);
    }
}
