using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private enum State
    {
        Roaming,
        ChasingPlayer
    }

    private State state;
    private EnemyPathfinding enemyPathfinding;
    private SpriteRenderer spriteRenderer;

    private Transform playerTransform;
    private float moveSpeed = 2f;
    public float detectionRange = 5.0f;

    private void Awake()
    {
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        state = State.Roaming;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    //------- 여기까진 불러오고 초기화 과정

    private void Start()
    {
        StartCoroutine(RoamingTime());
    }

    private IEnumerator RoamingTime()
    {
        while (state == State.Roaming)
        {
            Vector2 roamPosition = GetRoamingPosition();
            enemyPathfinding.MoveTo(roamPosition);

            if (roamPosition.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }

            yield return new WaitForSeconds(3f);
        }
    }

    private Vector2 GetRoamingPosition()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    private void Update()
    {
        if (Vector2.Distance(playerTransform.position, transform.position) <= detectionRange)
        {
            state = State.ChasingPlayer;
            MoveTowardsPlayer();
        }

        else if (Vector2.Distance(playerTransform.position, transform.position) >= detectionRange)
        {
            state = State.Roaming;
        }
    }

    void MoveTowardsPlayer()
    {
        // 적 캐릭터가 플레이어를 향해 이동하도록 합니다.
        Vector3 moveDirection = (playerTransform.position - transform.position).normalized;
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        if (playerTransform.position.x > transform.position.x)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }
}

