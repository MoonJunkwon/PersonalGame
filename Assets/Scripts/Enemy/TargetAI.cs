using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAI : MonoBehaviour
{

    [SerializeField] private Transform target;
    private float move = 3f;
    private bool isPlayerInRange = false;

    private void Update()
    {
        if (isPlayerInRange == true)
        {
            // 플레이어를 향해 이동
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        // 적 캐릭터가 플레이어를 향해 이동하도록 합니다.
        Vector3 moveDirection = (target.position - transform.position).normalized;
        transform.Translate(moveDirection * move * Time.deltaTime);;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (target)
            isPlayerInRange = true;
    }
}
