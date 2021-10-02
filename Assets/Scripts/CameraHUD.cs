using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHUD : MonoBehaviour
{
    [SerializeField]
    TMPro.TextMeshProUGUI cameraInfo;

    [SerializeField]
    TMPro.TextMeshProUGUI runTimeInfo;

    private void OnEnable()
    {
        CameraDirector.OnNewCamera += CameraDirector_OnNewCamera;
    }

    private void OnDisable()
    {
        CameraDirector.OnNewCamera -= CameraDirector_OnNewCamera;
    }

    private void CameraDirector_OnNewCamera(SurvalianceCamera camera)
    {
        var info = camera.info;
        cameraInfo.text = string.Format("{0}\n{1}", info[0], info[1]);
    }

    private void Update()
    {
        var t = Time.timeSinceLevelLoad;
        int minutes = (int) (t / 60f);
        int seconds = (int)(t - minutes * 60);
        int millies = (int)((t - minutes * 60 - seconds) * 1000);
        runTimeInfo.text = string.Format("{0:00}:{1:00}\n{2:F1}m", minutes, seconds, GoodBoy.distanceToObjective);
    }
}
