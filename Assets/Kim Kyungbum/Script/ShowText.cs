using UnityEngine;
using TMPro;

public class ShowText : MonoBehaviour
{
    public TextMeshProUGUI promptText;
    void Start()
    {
        promptText.text = "Hello World!";
        Color text = promptText.color;
        text.a = 0f; // alpha 0 = 완전 투명
        promptText.color = text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
