using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDirector : MonoBehaviour
{ 
    public static CameraDirector instance { get; private set; }

    SurvalianceCamera fallbackCamera;
    SurvalianceCamera currentCamera;
    SurvalianceCamera nextCamera;
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

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    private void Update()
    {
        if (nextCamera != currentCamera && Time.timeSinceLevelLoad - switchTime > minCameraTime)
        {
            currentCamera.gameObject.SetActive(false);
            currentCamera = nextCamera;
            currentCamera.gameObject.SetActive(true);
            switchTime = Time.timeSinceLevelLoad;
        }
    }

    public void ActivateCamera(SurvalianceCamera camera)
    {
        if (currentCamera == null)
        {
            fallbackCamera = camera;
            currentCamera = camera;
            nextCamera = camera;
            switchTime = Time.timeSinceLevelLoad;
            camera.gameObject.SetActive(true);
        } else
        {
            nextCamera = camera;
        }
    }

    public void DeactivateCamera(SurvalianceCamera camera)
    {
        if (currentCamera == camera)
        {
            nextCamera = fallbackCamera;
        } else if (nextCamera == camera)
        {
            nextCamera = currentCamera;
        }
    }
}
