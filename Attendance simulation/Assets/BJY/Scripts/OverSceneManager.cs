using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OverSceneManager : MonoBehaviour
{
    [SerializeField]
    private Text clear_text;
    [SerializeField]
    private Text fail_text;

    private bool success;
    // Start is called before the first frame update
    void Start()
    {
        success = GameManager.Instance.GetResult();
        if (success)
        {
            clear_text.gameObject.SetActive(true);
        }
        else
        {
            fail_text.gameObject.SetActive(true);
        }
    }

    public void ClickedRestart()
    {
        switch (GameManager.Instance.GetLevel())
        {
            case 1:
                ChoiceLevel1();
                break;
            case 2:
                ChoiceLevel2();
                break;
            case 3:
                ChoiceLevel3();
                break;
            default:
                break;
        }

    }

    public void ClickMainBtn()
    {
        SceneManager.LoadScene(0);
    }

    public void ChoiceLevel1()
    {
        GameManager.Instance.StartLevel1();
    }
    public void ChoiceLevel2()
    {
        GameManager.Instance.StartLevel2();
    }
    public void ChoiceLevel3()
    {
        GameManager.Instance.StartLevel3();
    }
}
