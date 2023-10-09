using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    public static PlayerControll Instance;

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private TrailRenderer myTrail;


    private PlayerControlls playerControlls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator myAni;
    private SpriteRenderer mySprite;

    private bool isDashing = false;

    private void Awake()
    {
        Instance = this;
        playerControlls = new PlayerControlls();
        rb = GetComponent<Rigidbody2D>();
        myAni = GetComponent<Animator>();
        mySprite = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        playerControlls.Combat.Dash.performed += _ => Dash();
    }
    private void Update()
    {
        playerInput();
    }
    private void FixedUpdate()
    {
        Move();
        MousePosition();
    }

    private void OnEnable()
    {
        playerControlls.Enable();
    }

    private void playerInput()
    {
        movement = playerControlls.Player.PlayerInput.ReadValue<Vector2>();

        //movement는 InputSystem
        myAni.SetFloat("moveX", movement.x); 
        myAni.SetFloat("moveY", movement.y); 
    }
    private void Move()
    {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }
    private void MousePosition()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x < playerScreenPoint.x)
        {
            mySprite.flipX = true;
        }
        else mySprite.flipX = false;
    }

    private void Dash()
    {
        if (!isDashing)
        {
        isDashing = true;
        moveSpeed *= dashSpeed;
        myTrail.emitting = true;
        StartCoroutine(EndDashRoutine());
        }
    }

    private IEnumerator EndDashRoutine()
    {
        float dashTime = .2f;
        float dashCD = .25f;
        yield return new WaitForSeconds(dashTime);
        moveSpeed /= dashSpeed;
        myTrail.emitting = false;
        yield return new WaitForSeconds(dashCD);
        isDashing = false;
    }
}
