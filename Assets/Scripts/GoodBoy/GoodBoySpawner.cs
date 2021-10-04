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

    public CheckPoint CheckPoint { get; set; }

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
        goodBoy = InstantiateBody(transform.position, GetLookRotation(transform.position));                
        goodBoy.gameObject.name = "GoodBoy";
        spawnOffset = transform.position - GetGround(transform.position);
        GoodBoyInput.HasPower = true;
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
            StartCoroutine(Realign());
        } else if (GoodBoyInput.Respawn && goodBoy != null)
        {
            StartCoroutine(Respawn());
        }
    }
    [SerializeField]
    List<CheckPoint> checkPoints = new List<CheckPoint>();

    Vector3 LookTarget
    {
        get
        {
            var chpts = checkPoints.Count;
            var idx = chpts == 0 ? -1 : checkPoints.IndexOf(CheckPoint);
            if (CheckPoint == null && chpts == 0 || idx == chpts - 1)
            {
                return Objective.instance.transform.position;
            }
            return checkPoints[idx + 1].LookTarget;
        }
    }

    Vector3 SpawnerLocation
    {
        get
        {
            if (CheckPoint == null) return transform.position;
            return CheckPoint.transform.position;
        }
    }

    Quaternion GetLookRotation(Vector3 spawnPosition)
    {
        var lookForward = LookTarget - spawnPosition;
        lookForward.y = 0;
        return Quaternion.LookRotation(lookForward.normalized, Vector3.up);
    }

    IEnumerator<WaitForSeconds> Realign()
    {
        GoodBoyInput.HasPower = false;
        var spawnPosition = GetGround(goodBoy.Center) + spawnOffset;
        var rotation = GetLookRotation(spawnPosition);
        Destroy(goodBoy.gameObject);

        yield return new WaitForSeconds(0.5f);

        goodBoy = InstantiateBody(spawnPosition, rotation);
        goodBoy.gameObject.name = "GoodBoy";
        GoodBoyInput.HasPower = true;
    }

    IEnumerator<WaitForSeconds> Respawn()
    {
        GoodBoyInput.HasPower = false;
        Destroy(goodBoy.gameObject);
        var spawnPosition = SpawnerLocation;        
        yield return new WaitForSeconds(0.5f);

        goodBoy = InstantiateBody(spawnPosition, GetLookRotation(spawnPosition));
        goodBoy.gameObject.name = "GoodBoy";
        GoodBoyInput.HasPower = true;
    }

}
