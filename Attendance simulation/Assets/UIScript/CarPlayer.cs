using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPlayer : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            // 초기 위치 임시 지정
            transform.position = new Vector3(105.5f, 2f, -335f);
        }
    }
}
