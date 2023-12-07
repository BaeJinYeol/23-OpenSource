using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    private ThirdPersonController player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("���� �÷��̾� �ӵ� : " + player.SprintSpeed);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("�÷��̾ Ż��!!");
        }
    }

    private void Awake()
    {
        player = FindObjectOfType<ThirdPersonController>();

        if (player != null)
        {
            Debug.Log("Player is not null");
        }
        else
        {
            Debug.Log("Player is null");
        }
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
