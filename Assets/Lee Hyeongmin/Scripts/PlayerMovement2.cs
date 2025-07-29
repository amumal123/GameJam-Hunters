using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerMovement2 : MonoBehaviour
{
    public float playerSpeed = 3f;
    public float playerRotSpeed = 0.25f;

    private CharacterController characterController;
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

        // 키보드 입력 받기
        float x = Input.GetAxis("Horizontal"); // A, D
        float z = Input.GetAxis("Vertical");   // W, S

        // 현재 바라보는 방향 기준으로 이동 벡터 계산
        Vector3 move = transform.right * x + transform.forward * z;

        // 실제로 이동
        //controller.Move(move * speed * Time.deltaTime);
    }

    private void Move(Vector2 movementInput) // movementInput <- w를 누르면 0,1 a를 누르면 -1,0 s를 누르면 0,-1 d를 누르면 1,0
    {
        //Vector3 moveDirection = (transform.right * movementInput.x + transform.forward * movementInput.y) * playerSpeed;
        //// transform.right: 로컬 좌표계에서의 플레이어 x축
        //// transform.forward: 로컬 좌표계에서의 플레이어 z축
        //// playerSpeed: 이동속도

        ////moveDirection.y = playerRigidbody.linearVelocity.y; // y축으로는 이동하지 않을 것이기에 고정용

        //playerRigidbody.linearVelocity = moveDirection; // .linearVelocity: 값을 넣어주면 그에따라 움직임

        Vector3 movementDirection = (Vector3.forward * movementInput.y) + (Vector3.right * movementInput.x);
        transform.Translate(movementDirection.normalized * Time.deltaTime * playerSpeed, Space.Self);
    }

    private void Rotate(Vector2 lookInput)
    {
        yaw += lookInput.x * playerRotSpeed;

        pitch -= lookInput.y * playerRotSpeed;
        pitch = Mathf.Clamp(pitch, -90f, 90f);

        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }
}
