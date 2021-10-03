using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoboShopRotator : MonoBehaviour
{
    [SerializeField]
    Button RotateButton;

    Transform body;
    
    [SerializeField]
    AnimationCurve easing;
    [SerializeField]
    float turnTime = 1f;

    private void Start()
    {
        RotateButton.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        BodySelector.OnSelectBodyType += BodySelector_OnSelectBodyType;
        PositionSelector.OnSelectLimb += PositionSelector_OnSelectLimb;
    }

    private void OnDisable()
    {
        BodySelector.OnSelectBodyType -= BodySelector_OnSelectBodyType;
        PositionSelector.OnSelectLimb -= PositionSelector_OnSelectLimb;
    }
    private void PositionSelector_OnSelectLimb(PositionSelector positionSelector)
    {
        RotateButton.gameObject.SetActive(false);
    }

    private void BodySelector_OnSelectBodyType(string bodyType, Transform bodyTransform)
    {
        Enabled();
        body = bodyTransform;
    }

    public void Enabled()
    {
        RotateButton.gameObject.SetActive(true);
    }

    public void Rotate()
    {
        if (!rotating) StartCoroutine(RunRotation());
    }

    bool rotating = false;
    float[] rotoAngles = new float[] { 90, -90 };
    int rotoIdx = 0;
    IEnumerator<WaitForSeconds> RunRotation()
    {
        rotating = true;
        PositionSelector.disabled = true;
        var t0 = Time.timeSinceLevelLoad;
        var progress = 0f;
        var origin = body.rotation;
        rotoIdx = (rotoIdx + 1) % 2;
        var rotation = Quaternion.AngleAxis(rotoAngles[rotoIdx], Vector3.up);
        while (progress < 1f)
        {
            body.rotation = Quaternion.SlerpUnclamped(origin, rotation, (rotoIdx == 0 ? -1 : 1) * easing.Evaluate(progress));
            yield return new WaitForSeconds(0.05f);
            progress = (Time.timeSinceLevelLoad - t0) / turnTime;
        }
        body.rotation = Quaternion.Slerp(origin, rotation, 1f);
        rotating = false;
        PositionSelector.disabled = false;
    }
}
