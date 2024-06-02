using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Vector2 moveInput;

    [SerializeField]
    private float moveSpeed = 1.0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (UIToggler.uiIsOn) return;
        if (GameOverScreen.instance.gameIsOver) return;
        HandleMovement(moveInput);
    }

    private void HandleMovement(Vector2 Input)
    {
        Vector3 moveVector = new Vector3(Input.x, 0, Input.y);

        // not sure why this works when moveVector *= rb.rotation is illegal
        moveVector = rb.rotation * moveVector;

        rb.MovePosition(rb.position + moveVector * Time.fixedDeltaTime * moveSpeed);
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}
