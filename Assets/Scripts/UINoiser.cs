using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINoiser : MonoBehaviour
{
    Texture2D tex;
    Image img;
    Sprite sprite;

    [SerializeField]
    float defaultNoiseProbability = 0.5f;

    [SerializeField]
    float defaultAlpha = 0.5f;
    [SerializeField]
    float swappingAlpha = 0.7f;

    void Start()
    {
        tex = new Texture2D(Screen.width, Screen.height);
        sprite = Sprite.Create(tex, new Rect(0, 0, Screen.width, Screen.height), Vector2.one * 0.5f);
        sprite.name = "Rendered Noise Img";
        img = GetComponent<Image>();
        img.sprite = sprite;
        StartCoroutine(Noiser());
    }

    void SaltAndPepper(float transparencyProbability)
    {
        for (int y = 0; y < tex.height; y++)
        {
            for (int x = 0; x < tex.width; x++)
            {
                if (Random.value < transparencyProbability)
                {
                    tex.SetPixel(x, y, Palette.tansparent);
                } else
                {
                    tex.SetPixel(x, y, Random.value < 0.5f ? Palette.color1 : Palette.color5);
                }
                

            }
        }
        tex.Apply();
    }
    bool swapping;
    [SerializeField]
    float swapDuration = 0.5f;

    IEnumerator<WaitForSeconds> CameraSwap()
    {
        var color = Color.white;
        color.a = swappingAlpha;
        swapping = true;
        img.color = color;
        yield return new WaitForSeconds(swapDuration);
        swapping = false;
        color.a = defaultAlpha;
        img.color = color;
    }
    

    IEnumerator<WaitForSeconds> Noiser()
    {
        while (true)
        {
            SaltAndPepper(swapping ? 0 : defaultNoiseProbability);
            yield return new WaitForSeconds(0.05f);
        }
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
