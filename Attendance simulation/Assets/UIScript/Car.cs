using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public GameObject[] carPrefabs;
    public float moveSpeed = 4f;
    public Vector3 direction = Vector3.forward;

    private void Awake()
    {
        if (GetComponent<AudioSource>() != null)
        {
            GetComponent<AudioSource>().volume = SoundManager.Instance.bgVolume;
            GetComponent<AudioSource>().spatialBlend = 1f;
        }
        Invoke("DestroyObject", 32f);
    }
    private void FixedUpdate()
    {
        //int randomIndex = Random.Range(0, carPrefabs.Length);

        //GameObject car = Instantiate(carPrefabs[randomIndex], transform.position, Quaternion.identity);
        
        //car.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, moveSpeed);
        //this.GetComponent<Rigidbody>().velocity = transform.forward * moveSpeed;
        //rb.MovePosition(rb.position + moveSpeed * direction * Time.deltaTime);
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);

        //Destroy(this, 32f);
    }
    void DestroyObject()
    {
        // 생성된 게임 오브젝트 파괴
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.Instance.GameOver();
        }        
    }
}
