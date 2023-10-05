using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;

    private PlayerControlls playerControlls;
    private Vector2 movement;

    private Rigidbody2D rb;

    private void Awake()
    {
        playerControlls = new PlayerControlls();
        rb = GetComponent<Rigidbody2D>();
        
    }

    private void Update()
    {
        playerInput();
    }
    private void FixedUpdate()
    {
        Move();
    }

    private void OnEnable()
    {
        playerControlls.Enable();
    }

    private void playerInput()
    {
        movement = playerControlls.Player.PlayerInput.ReadValue<Vector2>();
    }
    private void Move()
    {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }
}
