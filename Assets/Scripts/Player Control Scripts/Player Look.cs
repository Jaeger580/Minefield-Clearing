using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [SerializeField]
    private float sensitivity = 5f;

    [SerializeField]
    private GameObject mainCamera;

    private Vector2 mouseInput;
    private Rigidbody rb;
    private float vertRotation;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        HandleLook(mouseInput);
    }

    private void HandleLook(Vector2 Input) 
    {
        float horizRotation = Input.x * sensitivity;
        transform.Rotate(0, horizRotation, 0);
        //rb.MoveRotation(Quaternion.Euler(0, horizRotation, 0));
        
        vertRotation -= Input.y * sensitivity;
        vertRotation = Mathf.Clamp(vertRotation, -90, 90);
        
        mainCamera.transform.localRotation = Quaternion.Euler(vertRotation, 0, 0);
    }

    public void Look(InputAction.CallbackContext context) 
    {
        mouseInput = context.ReadValue<Vector2>();
    }
}
