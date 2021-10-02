using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodBoy : MonoBehaviour
{
    static string DEFAULT_LEG_TYPE = "BlockMotor";
    public static string LegTypeSetting(LegPosition legPosition)
    {
        return string.Format("Leg.Type.{0}", legPosition.ToString());
    }

    public static LegController InstantiateLeg(LegPosition legPosition)
    {
        return Instantiate(Resources.Load<LegController>(
            string.Format(
                "Legs/{0}",
                PlayerPrefs.GetString(LegTypeSetting(legPosition), DEFAULT_LEG_TYPE)
            )
        ));
    }

    static string DEFAULT_HEAD_TYPE = "Sphere";
    static string HEAD_TYPE_SETTING = "Head.Type";
    public static GoodBoyHead InstantiateHead()
    {
        return Instantiate(Resources.Load<GoodBoyHead>(
            string.Format(
                "Heads/{0}",
                PlayerPrefs.GetString(HEAD_TYPE_SETTING, DEFAULT_HEAD_TYPE)
            )
        ));
    }

    Transform _trackingPosition;

    [SerializeField]
    private Rigidbody body;

    [SerializeField]
    Transform[] legAnchors;
    [SerializeField]
    Transform headAnchor;

    public Vector3 GetLegAnchor(LegPosition position)
    {
        return legAnchors[(int)position].position;
    }

    public Rigidbody AnchorBody
    {
        get { return body; }        
    }

    public Transform trackingPosition
    {
        get
        {
            return _trackingPosition;
        }
    }

    public static GoodBoy instance { get; private set; }

    public static float distanceToObjective
    {
        get
        {
            var offset = Objective.instance.transform.position - instance.trackingPosition.position;
            return offset.magnitude;
        }
    }


    public static float GetSqDistanceTo(Transform other)
    {
        return (other.position - instance.trackingPosition.position).sqrMagnitude;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
        HasPower = true;
        for (int i=0; i<4; i++)
        {
            Vector3 anchor = legAnchors[i].position;
            var legPosition = (LegPosition)i;
            var leg = InstantiateLeg(legPosition);
            leg.gameObject.SetActive(false);
            var xOffSign = i % 2 == 0 ? -1 : 1;
            var offset = new Vector3(leg.anchorOffset.x * xOffSign, leg.anchorOffset.y, leg.anchorOffset.z);
            leg.transform.position = anchor + offset;
            leg.transform.SetParent(transform, true);            
            leg.gameObject.SetActive(true);
            leg.legPosition = legPosition;
        }

        var head = InstantiateHead();
        head.gameObject.SetActive(false);
        head.transform.position = headAnchor.transform.position + head.transform.TransformPoint(head.AnchorOffset);
        head.transform.SetParent(transform, true);
        head.GetComponent<FixedJoint>().connectedBody = body;
        _trackingPosition = head.transform;
        head.gameObject.SetActive(true);
            
    }

    private void OnDestroy()
    {
        if (instance == this) instance = null;
    }

    public bool HasPower { get; private set; }

    public void PowerDown()
    {
        HasPower = false;
    }
}
