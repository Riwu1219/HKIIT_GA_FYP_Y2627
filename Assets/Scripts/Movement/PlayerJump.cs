using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private InputActionReference jumpButton;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private CharacterController cc;
    [SerializeField] private LayerMask groundLayers;

    private float gravity = Physics.gravity.y; // get current gravity
    private Vector3 movement;

    private void Update()
    {
        bool _isGrounded = isGrounded();

        if (jumpButton.action.WasPressedThisFrame() && _isGrounded)
        {
            Jump();
        }

        movement.y += gravity * Time.deltaTime; // apply gravity to vertical movement
    }

    private void Jump()
    {
        movement.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);

        cc.Move(movement * Time.deltaTime); // apply movement to character controller
    }

    //cast a sphere at the player's position to check if grounded and check
    private bool isGrounded()
    {
        return Physics.CheckSphere(transform.position, 0.2f, groundLayers);
    }
}
    
