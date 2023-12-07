using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using StarterAssets;

// ��ġ�� �����ؼ� �̵��ϴ� ��ũ��Ʈ
public class EnemyAI : MonoBehaviour
{
    NavMeshAgent nav;
    [SerializeField] Transform target;

    public Animator ani;
    public Transform[] arrWaypoint;     // �̵��� ��ġ�� �����ϴ� �迭

    private Vector3 dest;               // Ÿ�� ������
    private Coroutine moveStop;

    private ThirdPersonController player_speed;    // �÷��̾� �̵� �ӵ� ����

    private bool hasAttacked = false;   // AI�� ���� ����

    private void Awake()
    {
        player_speed = FindObjectOfType<ThirdPersonController>();
    }

    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        nav.speed = 2.5f;

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
                dest = arrWaypoint[i].position;
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
            if (target_dis < 0.16f && !hasAttacked)
            {
                ani.SetBool("isExist", true);
                hasAttacked = true;
                if (player_speed.SprintSpeed > 2f)
                {
                    player_speed.SprintSpeed -= 3.5f;
                    Invoke("RecoverSpeed", 2.5f);       // �÷��̾� �̵� �ӵ� ȸ��
                }
            }
        }
    }
    private void RecoverSpeed()
    {
        ani.SetBool("isExist", false);
        player_speed.SprintSpeed += 3.5f;
    }
}
