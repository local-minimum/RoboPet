using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodBoySpawner : MonoBehaviour
{
    static string DEFAULT_BODY_TYPE = "Blocky";
    public static string BODY_TYPE_SETTING = "Body.Type";

    GoodBoy goodBoy;

    private GoodBoy InstantiateBody(Vector3 position, Quaternion rotation)
    {
        return Instantiate(
            Resources.Load<GoodBoy>(
                string.Format(
                    "Bodies/{0}",
                    PlayerPrefs.GetString(BODY_TYPE_SETTING, DEFAULT_BODY_TYPE)
                )            
            ),
            position,
            rotation
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

    Vector3 spawnOffset;

    private void Start()
    {
        goodBoy = InstantiateBody(transform.position, transform.rotation);                
        goodBoy.gameObject.name = "GoodBoy";
        spawnOffset = transform.position - GetGround(transform.position);
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    
    Vector3 GetGround(Vector3 point)
    {
        LayerMask mask = LayerMask.NameToLayer("Ground");
        RaycastHit hitInfo;
        if (Physics.Raycast(new Ray(point, Vector3.down), out hitInfo, 10, mask))
        {
            return hitInfo.point;

        }
        return new Ray(point, Vector3.down).GetPoint(1);
    }

    private void Update()
    {
        if (GoodBoyInput.Realign && goodBoy != null)
        {
            StartCoroutine(Respawn());
        }
    }

    IEnumerator<WaitForSeconds> Respawn()
    {
        GoodBoyInput.HasPower = false;
        var center = goodBoy.Center + spawnOffset;
        Destroy(goodBoy.gameObject);
        var lookForward = Objective.instance.transform.position - center;
        lookForward.y = 0;
        var rotation = Quaternion.LookRotation(lookForward.normalized, Vector3.up);

        yield return new WaitForSeconds(0.5f);

        goodBoy = InstantiateBody(center, rotation);
        goodBoy.gameObject.name = "GoodBoy";
        GoodBoyInput.HasPower = true;
    }

}
