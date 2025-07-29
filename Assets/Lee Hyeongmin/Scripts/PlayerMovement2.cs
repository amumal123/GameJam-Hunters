using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    public float playerSpeed = 3f;
    public float playerRotSpeed = 0.25f;

    private Rigidbody playerRigidbody;
    private PlayerInput playerInput;
    private float pitch = 0f;
    private float yaw = 0f;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        Move(playerInput.movementInput);
        Rotate(playerInput.lookInput);
    }

    private void Move(Vector2 movementInput)
    {
        Vector3 moveDirection = (transform.right * movementInput.x + transform.forward * movementInput.y) * playerSpeed;

        moveDirection.y = playerRigidbody.linearVelocity.y;

        playerRigidbody.linearVelocity = moveDirection;
    }

    private void Rotate(Vector2 lookInput)
    {
        yaw += lookInput.x * playerRotSpeed;

        pitch -= lookInput.y * playerRotSpeed;
        pitch = Mathf.Clamp(pitch, -90f, 90f);

        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }
}
