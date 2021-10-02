using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    public static Objective instance { get; private set; }

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

    private void OnDestroy()
    {
        if (instance == this) { instance = null; }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GoodBoy")
        {
            GoodBoy.instance.PowerDown();
            Time.timeScale = 0;
            Debug.Log(string.Format("Completed: {0}", Time.timeSinceLevelLoad));
        }
    }
}
