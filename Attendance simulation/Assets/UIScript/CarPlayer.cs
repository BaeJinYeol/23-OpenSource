using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPlayer : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            // �ʱ� ��ġ �ӽ� ����
            transform.position = new Vector3(105.5f, 2f, -335f);
        }
    }
}
