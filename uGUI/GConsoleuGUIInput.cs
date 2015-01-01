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
		}

		void onEndEdit (string line)
		{
			uGUI.OnInput ();
		}

		void Update ()
		{
				if (input.text != oldvalue) {
						uGUI.OnChange ();
				}
				oldvalue = input.text;
		}
}
