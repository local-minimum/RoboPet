using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINoiser : MonoBehaviour
{
    Texture2D tex;
    Image img;
    Sprite sprite;

    void Start()
    {
        tex = new Texture2D(Screen.width, Screen.height);
        sprite = Sprite.Create(tex, new Rect(0, 0, Screen.width, Screen.height), Vector2.one * 0.5f);
        sprite.name = "Rendered Noise Img";
        img = GetComponent<Image>();
        img.sprite = sprite;
    }

    void SaltAndPepper()
    {
        for (int y = 0; y < tex.height; y++)
        {
            for (int x = 0; x < tex.width; x++)
            {
                tex.SetPixel(x, y, Random.value < 0.5f ? Palette.color1 : Palette.color5);
            }
        }
        tex.Apply();
    }
    bool swapping;
    [SerializeField]
    float swapDuration = 0.5f;
    IEnumerator<WaitForSeconds> CameraSwap()
    {
        swapping = true;
        float t0 = Time.timeSinceLevelLoad;
        img.enabled = true;
        while (Time.timeSinceLevelLoad - t0 < swapDuration)
        {
            SaltAndPepper();
            yield return new WaitForSeconds(0.05f);
        }
        img.enabled = false;        
        swapping = false;
    }

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
        StartCoroutine(CameraSwap());
    }
}
