using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Scripts/Gconsole-uGUI/GConsoleuGUIInput")]
public class GConsoleuGUIInput : MonoBehaviour
{
		[HideInInspector]
		public GConsoleuGUI uGUI;
		private string oldvalue;
		private InputField input;

		void Start ()
		{
				input = GetComponent<InputField> ();
				input.onEndEdit.AddListener (onEndEdit);
				input.onValueChange.AddListener (onChangeEdit);
		}

		void onEndEdit (string line)
		{
			if (Input.GetButtonDown ("Submit")) {	// make sure we only realy as a submition if submit key was used same frame (Ugly but workaround since uGUI triggers submit when inputfield is deselected)
				uGUI.OnInput ();
			}
		}
		
		void onChangeEdit(string line)
		{
			if (input.text != oldvalue) {
				uGUI.OnChange ();
			}
			oldvalue = input.text;
		}
}
