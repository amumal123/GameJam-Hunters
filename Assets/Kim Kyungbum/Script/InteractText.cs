using UnityEngine;
using TMPro;

public class InteractText : MonoBehaviour
{
    public TextMeshProUGUI promptText;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("µé¾î¿È");
            promptText.text = "Open Door (E)";
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("³ª°¨");
            promptText.text = "";
        }
    }
}
