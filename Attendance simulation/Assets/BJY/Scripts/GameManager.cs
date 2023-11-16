using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject main_camera;
    [SerializeField] private GameObject follow_camera;

    private void Start()
    {
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(main_camera);
        DontDestroyOnLoad(follow_camera);
    }
}