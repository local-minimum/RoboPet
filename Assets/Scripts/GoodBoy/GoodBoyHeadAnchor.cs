using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodBoyHeadAnchor : MonoBehaviour
{
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

    public GoodBoyHead SpawnHead(Rigidbody body)
    {
        var head = InstantiateHead();
        head.gameObject.SetActive(false);
        head.transform.position = transform.position + head.transform.TransformPoint(head.AnchorOffset);
        head.transform.SetParent(transform.parent.parent, true);
        head.GetComponent<FixedJoint>().connectedBody = body;        
        head.gameObject.SetActive(true);
        return head;
    }
}
