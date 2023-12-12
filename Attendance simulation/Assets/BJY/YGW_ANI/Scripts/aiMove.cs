using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using StarterAssets;

// �ڵ����� �̵��ϰ� �ϴ� ��ũ��Ʈ
public class aiMove : MonoBehaviour
{
    NavMeshAgent nav;
    Transform target;

    public Animator ani;
    public Vector3[] arrWaypoint;     // �̵��� ��ġ�� �����ϴ� �迭

    private Vector3 dest;               // Ÿ�� ������
    private Coroutine moveStop;

    private ThirdPersonController player_speed;    // �÷��̾� �̵� �ӵ� ����

    private bool hasAttacked = false;   // AI�� ���� ����

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

    private void AiMove()       // �ڵ� �̵�(��ġ ���� �ʿ�)
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

                nav.SetDestination(dest);       // ������ ��ҷ� �̵�
                break;
            }
        }
    }

    IEnumerator crAiMove()
    {
        while (true)
        {
            CheckForPlayerDetection();      // �÷��̾� ����
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
            // NavMeshAgent�� ����Ͽ� �÷��̾ ���󰡵��� ����
            nav.SetDestination(target.position);

            // �÷��̾ AI�� ���� ������ ������ ��, ����Ǵ� ����
            var target_dis = Vector3.Distance(this.transform.position, target.position);
            if (target_dis < 0.3f && !hasAttacked)
            {
                ani.SetBool("isExist", true);
                hasAttacked = true;
                if (player_speed.SprintSpeed > 2f)
                {
                    player_speed.SprintSpeed -= 3.5f;
                    Invoke("RecoverSpeed", 2.5f);           // �÷��̾� �̵� �ӵ� ȸ��
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
