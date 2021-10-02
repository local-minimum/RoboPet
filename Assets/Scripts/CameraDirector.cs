using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void NewCameraEvent(SurvalianceCamera camera, bool firstCamera);

public class CameraDirector : MonoBehaviour
{
    public static event NewCameraEvent OnNewCamera;
    public static CameraDirector instance { get; private set; }

    SurvalianceCamera currentCamera;
    float switchTime;

    [SerializeField]
    float minCameraTime = 4f;

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

    private void Start()
    {
        switchTime = -minCameraTime * 2;
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    private void Update()
    {
        var nextCamera = SurvalianceCamera.HighestPriorityCamera;
        if (nextCamera != currentCamera && Time.timeSinceLevelLoad - switchTime > minCameraTime)
        {
            var firstCamera = currentCamera == null;
            currentCamera?.gameObject.SetActive(false);
            currentCamera = nextCamera;
            currentCamera.gameObject.SetActive(true);
            switchTime = Time.timeSinceLevelLoad;
            OnNewCamera?.Invoke(currentCamera, firstCamera);
        }
    }
}
