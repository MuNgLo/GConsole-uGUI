using UnityEngine;
using UnityEngine.UI;
using System.Collections;


[AddComponentMenu("Scripts/GConsoleNGUISuggestion")]
public class GConsoleuGUISuggestion : MonoBehaviour
{
    public Text label;

    public void ShowSuggestion(string s)
    {
        if (string.IsNullOrEmpty(s))
            gameObject.SetActive(false);
        else
        {
            gameObject.SetActive(true);
            label.text = s;
        }
    }
}
