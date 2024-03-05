using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    public Text questionText; // Reference to your Text component

    // You can call this method to change the text
    public void ChangeText(string newText)
    {
        if (questionText != null)
        {
            questionText.text = "Sam";
        }
        else
        {
            Debug.LogError("Text component not assigned. Please assign it in the inspector.");
        }
    }
}