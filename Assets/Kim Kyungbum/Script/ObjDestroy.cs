using UnityEditor;
using UnityEngine;

public class ObjDestroy : MonoBehaviour
{
    [SerializeField]
    private GameObject targetObj;
    public void destroyObject()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Destroy(targetObj);
            Debug.Log("삭제됨!!");
        }
    }
}
