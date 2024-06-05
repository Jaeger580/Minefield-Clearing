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
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (UIToggler.uiIsOn) return;
        if (GameOverScreen.instance.gameIsOver) return;
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

    public void Mark(InputAction.CallbackContext context) 
    {
        if (UIToggler.uiIsOn) return;
        if (context.started) 
        {
            RaycastHit hit;
            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, 6f))
            {
                Debug.Log(hit.transform.name);
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Tile")) 
                {
                    PhysicalTile tile = hit.transform.GetComponent<PhysicalTile>();
                    
                    tile.UpdateFlagPlacement(!tile.hasFlag);
                } 
            }
        }
        
    }
}
