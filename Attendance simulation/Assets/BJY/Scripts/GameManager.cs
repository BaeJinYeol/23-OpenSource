using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private GameObject main_camera;
    private GameObject follow_camera;
    private GameObject player;

    public bool isBackpack = false;

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
        else if (scene.name == "MainMenuScene")
        {
            GameOver();
        }
    }

    private void DontDestroyPlayer()
    {
        main_camera = GameObject.Find("MainCamera");
        follow_camera = GameObject.Find("PlayerFollowCamera");
        player = GameObject.Find("Player");

        DontDestroyOnLoad(main_camera);
        DontDestroyOnLoad(follow_camera);
        DontDestroyOnLoad(player);
    }

    public void TakeBackPack()
    {
        GameObject.Find("mixamorig1:Neck").transform.Find("Backpack").gameObject.SetActive(true);
        isBackpack = true;
    }

    public void SpawnYGW()
    {
        Transform spawnPoint = GameObject.Find("SpawnPoints").transform;
        player.transform.position = spawnPoint.position;
    }
    public void SpawnSchool()
    {
        Transform spawnPoint = GameObject.Find("SpawnPoint").transform;
        player.transform.position = spawnPoint.position;
    }
    public void GameOver()
    {
        Destroy(player);
        Destroy(follow_camera);
        Destroy(main_camera);
        Destroy(GameObject.Find("[HUD Navigation Canvas]"));
        Destroy(GameObject.Find("[HUD Navigation System]"));
    }
}