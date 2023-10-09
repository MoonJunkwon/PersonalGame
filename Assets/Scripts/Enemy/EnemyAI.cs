using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private enum State
    {
        Roaming
    }
    private State state;
    private EnemyPathfinding enemyPathfinding;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        state = State.Roaming;
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
}
