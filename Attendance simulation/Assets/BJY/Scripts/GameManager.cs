using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private GameObject main_camera;
    private GameObject follow_camera;
    private GameObject player;
    private GameObject speedline_ui;
    private int goalClass;
    private bool success_game = false;

    public int level;
    public bool isBackpack = false;
    public bool isHelmet = false;
    public bool isWatch = false;
    public bool isRide = false;

    private void Start()
    {
        // 다음 씬으로 전환할 때 이벤트를 등록
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬이 로드되면 호출될 콜백 메소드
        if (scene.name == "Room")
        {
            DontDestroyPlayer();
        }
        else if (scene.name == "YGW")
        {
            SpawnYGW();
        }
        else if (scene.name == "School")
        {
            SpawnSchool();
        }
    }

    private void DontDestroyPlayer()
    {
        main_camera = GameObject.Find("MainCamera");
        follow_camera = GameObject.Find("PlayerFollowCamera");
        player = GameObject.Find("Player");
        speedline_ui = GameObject.Find("SpeedLine_UI");

        DontDestroyOnLoad(main_camera);
        DontDestroyOnLoad(follow_camera);
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(speedline_ui);
    }
    public void TakeBackPack()
    {
        GameObject.Find("mixamorig1:Neck").transform.Find("Backpack").gameObject.SetActive(true);
        isBackpack = true;
    }
    public void TakeHelmet()
    {
        GameObject.Find("mixamorig1:HeadTop_End").transform.Find("Helmet").gameObject.SetActive(true);
        isHelmet = true;
    }
    public void UnTakeHelmet()
    {
        GameObject.Find("Helmet").gameObject.SetActive(false);
        isHelmet = false;
    }
    public void TakeWatch()
    {
        GameObject.Find("mixamorig1:LeftHand").transform.Find("Watch").gameObject.SetActive(true);
        isWatch = true;
    }

    public void SetGoalClass(ClassRoom cr)
    {
        goalClass = cr.GetHashCode();
    }
    public string GetGoalClass()
    {
        return goalClass.ToString();
    }

    public void SpawnYGW()
    {
        Transform spawnPoint;
        switch (level)
        {
            case 1:
                spawnPoint = GameObject.Find("SpawnPoint1").transform;
                player.transform.position = spawnPoint.position;
                GameObject.Find("NPCs").transform.Find("NPC1").gameObject.SetActive(true);
                break;
            case 2:
                spawnPoint = GameObject.Find("SpawnPoint2").transform;
                player.transform.position = spawnPoint.position;
                GameObject.Find("NPCs").transform.Find("NPC1").gameObject.SetActive(true);
                GameObject.Find("NPCs").transform.Find("NPC2").gameObject.SetActive(true);
                break;
            case 3:
                spawnPoint = GameObject.Find("SpawnPoint3").transform;
                player.transform.position = spawnPoint.position;
                GameObject.Find("NPCs").transform.Find("NPC1").gameObject.SetActive(true);
                GameObject.Find("NPCs").transform.Find("NPC2").gameObject.SetActive(true);
                GameObject.Find("NPCs").transform.Find("NPC3").gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }
    public void SpawnSchool()
    {
        Transform spawnPoint = GameObject.Find("SpawnPoint").transform;
        player.transform.position = spawnPoint.position;
    }
    public void GoalIn()
    {
        success_game = true;
        GameOver();
    }

    public void GameOver()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        isBackpack = false;
        isHelmet = false;
        isWatch = false;

        TimeManager.Instance.EndTime();

        Destroy(player);
        Destroy(follow_camera);
        Destroy(main_camera);
        Destroy(GameObject.Find("[HUD Navigation Canvas]"));
        Destroy(GameObject.Find("[HUD Navigation System]"));

        SceneManager.LoadScene(4);
    }
    public bool GetResult()
    {
        return success_game;
    }
    public int GetLevel()
    {
        return level;
    }
    public void StartLevel1()
    {
        TimeManager.Instance.StartTime(120f);
        SceneManager.LoadScene(3);
        level = 1;

        success_game = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void StartLevel2()
    {
        TimeManager.Instance.StartTime(150f);
        SceneManager.LoadScene(3);
        level = 2;

        success_game = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void StartLevel3()
    {
        TimeManager.Instance.StartTime(200f);
        SceneManager.LoadScene(3);
        level = 3;

        success_game = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}