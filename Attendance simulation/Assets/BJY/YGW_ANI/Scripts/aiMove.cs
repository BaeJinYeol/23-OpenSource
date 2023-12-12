using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using StarterAssets;

// 자동으로 이동하게 하는 스크립트
public class aiMove : MonoBehaviour
{
    NavMeshAgent nav;
    Transform target;

    public Animator ani;
    public Vector3[] arrWaypoint;     // 이동할 위치를 저장하는 배열

    private Vector3 dest;               // 타겟 목적지
    private Coroutine moveStop;

    private ThirdPersonController player_speed;    // 플레이어 이동 속도 감소

    private bool hasAttacked = false;   // AI의 공격 여부

    public int n_Waypoint = 5;
    public float moveRange = 50f;

    private void Awake()
    {
        player_speed = FindObjectOfType<ThirdPersonController>();

        arrWaypoint = new Vector3[n_Waypoint];
        GeneratePoints();
    }

    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        nav.speed = 3.8f;

        ani = GetComponent<Animator>();
        ani.SetBool("walk", true);

        Invoke("AiMove", 1);
    }

    private void Update()
    {
        CheckForPlayerDetection();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            target = other.gameObject.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        target = null;
        ani.SetBool("isExist", false);
        hasAttacked = false;
        Invoke("AiMove", 1);
    }

    private void AiMove()       // 자동 이동(위치 지정 필요)
    {
        int random = Random.Range(0, arrWaypoint.Length);

        for (int i = 0; i < arrWaypoint.Length; i++)
        {
            if (i == random)
            {
                dest = arrWaypoint[i];
                if (moveStop == null)
                {
                    moveStop = StartCoroutine(crAiMove());
                }

                nav.SetDestination(dest);       // 지정된 장소로 이동
                break;
            }
        }
    }

    IEnumerator crAiMove()
    {
        while (true)
        {
            CheckForPlayerDetection();      // 플레이어 감지
            var dis = Vector3.Distance(this.transform.position, dest);

            if (dis <= 0.5f)
            {
                if (moveStop != null)
                {
                    StopCoroutine(moveStop);
                    moveStop = null;
                    Invoke("AiMove", 0.5f);
                    break;
                }
            }
            yield return null;
        }
    }

    private void CheckForPlayerDetection()
    {
        if (target != null)
        {
            // NavMeshAgent를 사용하여 플레이어를 따라가도록 수정
            nav.SetDestination(target.position);

            // 플레이어가 AI의 공격 범위에 들어왔을 때, 수행되는 구문
            var target_dis = Vector3.Distance(this.transform.position, target.position);
            if (target_dis < 0.3f && !hasAttacked)
            {
                ani.SetBool("isExist", true);
                hasAttacked = true;
                if (player_speed.SprintSpeed > 2f)
                {
                    player_speed.SprintSpeed -= 3.5f;
                    Invoke("RecoverSpeed", 2.5f);           // 플레이어 이동 속도 회복
                }
            }
        }
    }

    private void GeneratePoints()
    {
        for (int i = 0; i < n_Waypoint; ++i)
        {
            Vector3 randPos = new Vector3(
                transform.position.x + Random.Range(-moveRange, moveRange),
                transform.position.y,
                transform.position.z + Random.Range(-moveRange, moveRange)
            );

            arrWaypoint[i] = randPos;
        }
    }

    private void RecoverSpeed()
    {
        ani.SetBool("isExist", false);
        player_speed.SprintSpeed += 3.5f;
    }
}
