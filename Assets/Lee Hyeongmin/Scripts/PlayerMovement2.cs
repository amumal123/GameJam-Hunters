using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerMovement2 : MonoBehaviour
{
    public float playerSpeed = 3f;
    public float playerRotSpeed = 0.25f;
    public Transform playerEyes;

    private CharacterController characterController;
    private PlayerInput playerInput;
    private float pitch = 0f;
    private float yaw = 0f;
    private float gravity = -9.81f;
    private Vector3 velocity;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();        
    }

    private void Update()
    {
        Move(playerInput.movementInput);
        Rotate(playerInput.lookInput);
    }

    private void Move(Vector2 movementInput) // movementInput <- w를 누르면 0,1 a를 누르면 -1,0 s를 누르면 0,-1 d를 누르면 1,0
    {
        //// 이동방향: 현재 회전 기준의 월드방향으로 변환
        //Vector3 move = transform.right * movementInput.x + transform.forward * movementInput.y;
        //move *= playerSpeed;

        //// 점프/중력 보정
        //if (characterController.isGrounded && velocity.y < 0)
        //{
        //    velocity.y = -2f; // 바닥에 붙임
        //}

        //velocity.y += gravity * Time.deltaTime;

        //Vector3 finalVelocity = move + Vector3.up * velocity.y;

        //characterController.Move(finalVelocity * Time.deltaTime);

        // 이동방향: 현재 회전 기준의 월드방향으로 변환
        Vector3 move = playerEyes.right * movementInput.x + playerEyes.forward * movementInput.y;
        move *= playerSpeed;

        // 점프/중력 보정
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // 바닥에 붙임
        }

        velocity.y += gravity * Time.deltaTime;

        Vector3 finalVelocity = move + Vector3.up * velocity.y;

        characterController.Move(finalVelocity * Time.deltaTime);
    }

    private void Rotate(Vector2 lookInput)
    {
        yaw += lookInput.x * playerRotSpeed;

        pitch -= lookInput.y * playerRotSpeed;
        pitch = Mathf.Clamp(pitch, -90f, 90f);

        //transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
        playerEyes.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }
}
