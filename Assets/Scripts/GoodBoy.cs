using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodBoy : MonoBehaviour
{
    Transform _trackingPosition;

    [SerializeField]
    private Rigidbody body;

    [SerializeField]
    string[] legs;
    [SerializeField]
    Transform[] legAnchors;

    [SerializeField]
    string head;
    [SerializeField]
    Transform headAnchor;

    [SerializeField]
    KeyCode[] activationKeys;

    [SerializeField]
    KeyCode reverseKey;


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
        for (int i=0; i<legs.Length; i++)
        {
            Vector3 anchor = legAnchors[i].position;
            var leg = Instantiate(
                Resources.Load<LegController>(string.Format("Legs/{0}", legs[i])),
                body.transform.position,
                Quaternion.identity
            );
            leg.gameObject.SetActive(false);
            var xOffSign = i % 2 == 0 ? -1 : 1;
            var offset = new Vector3(leg.anchorOffset.x * xOffSign, leg.anchorOffset.y, leg.anchorOffset.z);
            leg.transform.position = anchor + offset;
            leg.transform.SetParent(transform, true);
            leg.SetKeys(activationKeys[i], reverseKey);
            leg.gameObject.SetActive(true);
            leg.legPosition = (LegPosition)i;
        }

        var head = Instantiate(
            Resources.Load<GoodBoyHead>(string.Format("Heads/{0}", this.head))
        );
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
