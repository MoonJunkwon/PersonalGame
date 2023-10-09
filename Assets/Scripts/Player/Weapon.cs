using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    PlayerControll playerControll;
    ActiveWeapon activeWeapon;

    PlayerControlls playerControlls;
    Animator myAni;

    private bool isAttacking = false;

    CapsuleCollider2D weaponCollider;

    private void Awake()
    {
        playerControll = GetComponentInParent<PlayerControll>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();

        weaponCollider = GetComponent<CapsuleCollider2D>();

        playerControlls = new PlayerControlls();
        myAni = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        playerControlls.Enable();
    }

    void Start()
    {
        weaponCollider.isTrigger = false;
        playerControlls.Combat.Attack.started += _ => Attack();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            Attack();
        }

        MouseFollowWithOffset2();
    }

    public void Attack()
    {
        myAni.SetTrigger("Attack");
        weaponCollider.isTrigger = true;
        isAttacking = true;
    }


    private void MouseFollowWithOffset2()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(playerControll.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x)
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, -180, angle);
        }
        else
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void OnAttackAnimationEnd()
    {
        weaponCollider.isTrigger = false;
        isAttacking = false;
    }
}

