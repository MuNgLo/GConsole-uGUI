using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Scripts/Gconsole-uGUI/GConsoleuGUIInput")]
public class GConsoleuGUIInput : MonoBehaviour
{
		[HideInInspector]
		public GConsoleuGUI
				uGUI;
		private string oldvalue;
		private InputField input;

		void Start ()
		{
				input = GetComponent<InputField> ();
				input.onSubmit.AddListener (OnSubmit);
		}

		void OnSubmit (string line)
		{
				uGUI.OnInput ();
		}

		void Update ()
		{
				if (input.value != oldvalue) {
						uGUI.OnChange ();
				}
				oldvalue = input.value;
		}
}
