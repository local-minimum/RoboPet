using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BodyOption
{
    public string BodyType;
    public string Description;
    public Transform ViewPosition;
    public Transform Body;
}

public delegate void SelectBodyTypeEvent(string bodyType, Transform bodyTransform);

public class BodySelector : MonoBehaviour
{
    public static BodySelector instance { get; private set; }
    
    public static event SelectBodyTypeEvent OnSelectBodyType;

    [SerializeField]
    TMPro.TextMeshProUGUI description;
    [SerializeField]
    Button leftButton;
    [SerializeField]
    Button rightButton;
    [SerializeField]
    Button selectButton;
    
    public BodyOption[] options;
    public int currentIndex;
    [SerializeField] Camera cam;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(this);
        }
    }

    private void OnDestroy()
    {
        if (instance == this) { instance = null; }
    }

    public void SelectNext()
    {
        StartCoroutine(FocusOn(currentIndex + 1));
    }

    public void SelectPrevious()
    {
        StartCoroutine(FocusOn(currentIndex - 1));
    }

    public void Select()
    {
        var bodyType = options[currentIndex].BodyType;
        PlayerPrefs.SetString(GoodBoySpawner.BODY_TYPE_SETTING, bodyType);
        enabled = false;
        OnSelectBodyType?.Invoke(bodyType, options[currentIndex].Body);
    }

    private void OnEnable()
    {
        StartCoroutine(FocusOn(currentIndex));
    }

    private void OnDisable()
    {
        leftButton.gameObject.SetActive(false);
        rightButton.gameObject.SetActive(false);
        selectButton.gameObject.SetActive(false);
    }

    [SerializeField]
    float transitionTime = 1f;
    [SerializeField]
    AnimationCurve transitionEasing;

    IEnumerator<WaitForSeconds> FocusOn(int index)
    {
        currentIndex = index;
        description.text = "";
        leftButton.gameObject.SetActive(false);
        rightButton.gameObject.SetActive(false);
        selectButton.gameObject.SetActive(false);                
        var option = options[index];
        float t0 = Time.timeSinceLevelLoad;
        float progress = 0f;
        var origin = cam.transform.position;
        var startRotation = cam.transform.rotation;
        var targetRotation = option.ViewPosition.rotation;
        while (progress < 1f && index == currentIndex && enabled)
        {
            cam.transform.position = Vector3.Lerp(origin, option.ViewPosition.position, transitionEasing.Evaluate(progress));
            cam.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, transitionEasing.Evaluate(progress));
            yield return new WaitForSeconds(0.02f);
            progress = (Time.timeSinceLevelLoad - t0) / transitionTime;
        }
        if (index == currentIndex && enabled)
        {
            cam.transform.position = option.ViewPosition.position;
            description.text = option.Description;
            leftButton.interactable = index > 0;
            rightButton.interactable = index < options.Length - 1;
            leftButton.gameObject.SetActive(true);
            rightButton.gameObject.SetActive(true);
            selectButton.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        var newOption = Input.GetAxis("Horizontal");
        if (newOption > 0 && currentIndex < options.Length - 1)
        {
            StartCoroutine(FocusOn(currentIndex + 1));
        } else if (newOption < 0 && currentIndex > 0)
        {
            StartCoroutine(FocusOn(currentIndex - 1));
        }
    }
}
