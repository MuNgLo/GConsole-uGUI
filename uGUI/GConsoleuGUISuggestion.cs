using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[AddComponentMenu("Scripts/Gconsole-uGUI/GConsoleuGUISuggestion")]
public class GConsoleuGUISuggestion : MonoBehaviour
{
	[HideInInspector]
	public GConsoleuGUI uGUI;
	[HideInInspector]
	public Text label;

	void Start() {
		GetComponent<Button> ().onClick.AddListener (OnClick);
	}
	void OnClick() {
		uGUI.OnSuggestionClicked (label.text);
	}
	public void ShowSuggestion (string s)
	{

		if (string.IsNullOrEmpty (s)) {
			gameObject.SetActive (false);
		} else {
			label.text = s;
			gameObject.SetActive (true);
		}
	}
}
