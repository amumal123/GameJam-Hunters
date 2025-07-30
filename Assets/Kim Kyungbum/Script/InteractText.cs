using UnityEngine;
using TMPro;

public class InteractText : MonoBehaviour
{
    public TextMeshProUGUI promptText;
    private ObjDestroy targetDestroyScript;

    void Start()
    {
        targetDestroyScript = GetComponent<ObjDestroy>();
    }
    void Update()
    {
        if (promptText.color.a != 0)
        {
            Debug.Log(promptText.color.a);
            targetDestroyScript.destroyObject();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("켜짐");
            promptText.color = new Color32(255, 255, 255, 255);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("꺼짐");
            promptText.color = new Color32(255, 255, 255, 0);
        }
    }
}
