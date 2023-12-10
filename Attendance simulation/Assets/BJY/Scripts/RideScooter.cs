using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RideScooter : MonoBehaviour
{
    private GameObject player;
    private Animator animator;
    private bool isRide;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        animator = player.GetComponent<Animator>();
        isRide = false;
    }

    void Update()
    {
        if (isRide)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                GetOutScooter();
            }
        }
    }
    public void GetOutScooter()
    {
        player.GetComponent<StarterAssets.ThirdPersonController>().MoveSpeed = 2;
        animator.SetBool("Scooter", false);
        transform.parent = null;
        GameManager.Instance.isRide = isRide = false;
        GameManager.Instance.UnTakeHelmet();
        Destroy(gameObject);
    }
    public void RideTheScooter()
    {
        GameManager.Instance.isRide = isRide = true;
        animator.SetBool("Scooter", true);
        transform.position = player.transform.position + new Vector3(0f, 0.81f, 0.05f);
        Vector3 currentRotation = player.transform.rotation.eulerAngles;
        float newYRotation = currentRotation.y + 90f;
        Vector3 newRotation = new Vector3(currentRotation.x, newYRotation, currentRotation.z);
        transform.rotation = Quaternion.Euler(newRotation); 
        transform.parent = player.transform;
        player.GetComponent<StarterAssets.ThirdPersonController>().MoveSpeed = 12f;

        Invoke("GetOutScooter", 10f);
    }
}
