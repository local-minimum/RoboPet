using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public void Play() 
    {
        IntermissionNarrator.Stage = 0;
        SceneManager.LoadScene("Intermission");
    }
}
