using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Intermission
{
    public string name;
    public string[] prompts;
    public string nextScene;
}

public class IntermissionNarrator : MonoBehaviour
{
    public const string INTERMISSION_SETTING = "Intermission.Stage";

    public static int Stage
    {
        get
        {
            return PlayerPrefs.GetInt(INTERMISSION_SETTING, 0);
        }
        
        set
        {
            PlayerPrefs.SetInt(INTERMISSION_SETTING, value);
        }
    }

    [SerializeField]
    int forcedStage = -1;

    [SerializeField]
    Intermission[] intermissions;

    [SerializeField]
    TMPro.TextMeshProUGUI textField;
    
    int index = -1;

    Intermission intermission;

    private void Start()
    {
        int stage = forcedStage >= 0 ? forcedStage : Stage;
        if (stage < intermissions.Length)
        {
            intermission = intermissions[stage];
            Next();
        } else
        {            
            SceneManager.LoadScene("Menu");
        }
    }
    
    public void Next()
    {
        index += 1;
        if (index < intermission.prompts.Length)
        {
            textField.text = intermission.prompts[index].Replace('|', '\n');
        } else
        {
            Stage += 1;
            SceneManager.LoadScene(intermission.nextScene);
        }
    }
}
