using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private Vector2 inputVec2;
    private Vector3 moveDirection;

    [SerializeField]
    private float speed = 3.0f;

    void OnMove(InputValue value)
    {
        inputVec2 = value.Get<Vector2>();
        

    // W (앞) - z축 양수
    if (inputVec2.y > 0)
        Debug.Log("W (앞) 눌림");

    // S (뒤) - z축 음수
    if (inputVec2.y < 0)
        Debug.Log("S (뒤) 눌림");

    // D (오른쪽) - x축 양수
    if (inputVec2.x > 0)
        Debug.Log("D (오른쪽) 눌림");

    // A (왼쪽) - x축 음수
    if (inputVec2.x < 0)
        Debug.Log("A (왼쪽) 눌림");



        if (inputVec2 != null)
        {
            moveDirection = new Vector3(inputVec2.x, 0, inputVec2.y);
        }
    }
    void Update()
    {
        transform.Translate(moveDirection * Time.deltaTime * speed);
    }
}
