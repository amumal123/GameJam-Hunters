using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 6f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (controller == null)
        {
            Debug.LogError("❌ CharacterController를 찾지 못했어요!");
        }
        else
        {
            Debug.Log("✅ CharacterController 연결 완료");
        }
    }
    void Update()
    {
        // 키보드 입력 받기
        float x = Input.GetAxis("Horizontal"); // A, D
        float z = Input.GetAxis("Vertical");   // W, S

        // 현재 바라보는 방향 기준으로 이동 벡터 계산
        Vector3 move = transform.right * x + transform.forward * z;

        // 실제로 이동
        controller.Move(move * speed * Time.deltaTime);
    }
}
