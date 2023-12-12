using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void ClickBtnStart()
    {
        SceneManager.LoadScene(3);
    }
    public void ClickLevel1()
    {
        GameManager.Instance.StartLevel1();
    }
    public void ClickLevel2()
    {
        GameManager.Instance.StartLevel2();
    }
    public void ClickLevel3()
    {
        GameManager.Instance.StartLevel3();
    }

}
