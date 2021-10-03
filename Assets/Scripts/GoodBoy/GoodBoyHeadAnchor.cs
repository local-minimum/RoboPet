using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodBoyHeadAnchor : GoodBoyAnchor
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

    public override string SettingKey => HEAD_TYPE_SETTING;
    public override BodyPart BodyPart => BodyPart.Head;

    public GoodBoyHead SpawnHead()
    {
        var head = InstantiateHead();
        head.gameObject.SetActive(false);
        head.transform.position = transform.position + head.transform.TransformPoint(head.AnchorOffset);
        head.transform.SetParent(transform.parent.parent, true);
        head.GetComponent<FixedJoint>().connectedBody = transform.parent.GetComponent<Rigidbody>();        
        head.gameObject.SetActive(true);
        return head;
    }
}
