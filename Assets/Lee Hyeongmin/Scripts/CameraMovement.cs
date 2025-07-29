using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;

    private void LateUpdate()
    {
        transform.position = player.position;
        transform.eulerAngles = player.eulerAngles;
    }
}
