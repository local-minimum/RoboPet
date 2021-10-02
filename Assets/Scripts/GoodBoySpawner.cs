using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodBoySpawner : MonoBehaviour
{
    static string DEFAULT_BODY_TYPE = "Blocky";
    public static string BODY_TYPE_SETTING = "Body.Type";
    
    private GoodBoy InstantiateBody()
    {
        return Instantiate(
            Resources.Load<GoodBoy>(
                string.Format(
                    "Bodies/{0}",
                    PlayerPrefs.GetString(BODY_TYPE_SETTING, DEFAULT_BODY_TYPE)
                )            
            ),
            transform.position,
            Quaternion.identity
        );
    }

    public static GoodBoySpawner instance { get; private set; }

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
        var goodboy = InstantiateBody();                
        goodboy.gameObject.name = "GoodBoy";
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
}
