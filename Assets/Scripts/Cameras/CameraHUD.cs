using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraHUD : MonoBehaviour
{
    [SerializeField]
    TMPro.TextMeshProUGUI cameraInfo;

    [SerializeField]
    TMPro.TextMeshProUGUI runTimeInfo;

    [SerializeField]
    float distanceHack = 1.3f;

    private void OnEnable()
    {
        CameraDirector.OnNewCamera += CameraDirector_OnNewCamera;
        Objective.OnLevelEnd += Objective_OnLevelEnd;
    }


    private void OnDisable()
    {
        CameraDirector.OnNewCamera -= CameraDirector_OnNewCamera;
        Objective.OnLevelEnd -= Objective_OnLevelEnd;
    }
    bool levelDone = false;
    private void Objective_OnLevelEnd()
    {
        StartCoroutine(EndLevel());
    }

    IEnumerator<WaitForSeconds> EndLevel()
    {
        levelDone = true;
        GoodBoyInput.HasPower = false;
        yield return new WaitForSeconds(0.6f);
        SceneManager.LoadScene("Intermission");
    }

    private void CameraDirector_OnNewCamera(SurvalianceCamera camera, bool firstCamera)
    {
        var info = camera.info;
        cameraInfo.text = string.Format("{0}\n{1}", info[0], info[1]);
    }
    
    void SetRuntimeInfo(float distance)
    {
        var t = Time.timeSinceLevelLoad;
        int minutes = (int)(t / 60f);
        int seconds = (int)(t - minutes * 60);
        runTimeInfo.text = string.Format(
            "{0:00}:{1:00}\n{2:F1}m",
            minutes,
            seconds,
            distance
        );

    }

    private void Update()
    {
        if (levelDone) return;
        SetRuntimeInfo(Mathf.Max(GoodBoy.distanceToObjective - distanceHack, 0));
    }
}
