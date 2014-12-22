using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using System;

[AddComponentMenu("Scripts/Gconsole-uGUI/GConsoleuGUI")]
public class GConsoleuGUI : MonoBehaviour
{
		public Text consoleOutput;
		public InputField input;
		public EventSystem eSystem;
		public GConsoleuGUISuggestion[] suggestions;
		public bool clearOnSubmit = false;
		public bool reselectOnSubmit = true;
		public int minCharBeforeSuggestions;

		void Start ()
		{
				GConsole.OnOutput += OnOutput;	//Register the "OnOutput" method as a listener for console output.
				input.GetComponent<GConsoleuGUIInput> ().uGUI = this;
		}

		void OnOutput (string line)
		{
				consoleOutput.text += '\n' + line;
		}

		public void OnInput ()
		{
		string cmd = input.text ;
				if (string.IsNullOrEmpty (cmd))
						return;
				//Send command to the console
				GConsole.Eval (cmd);
				if (clearOnSubmit) {
			input.text  = string.Empty;
						input.transform.FindChild ("gc_Input_text").GetComponent<Text> ().text = string.Empty;
						Debug.Log("Clearing!");
				}
				if (reselectOnSubmit) {// TODO focus is now kept on input but need to trigger edit state to
						eSystem.SetSelectedGameObject (input.gameObject, null);
				}
		}

		public void OnChange ()
		{
				LoadSuggestions ();
		}

		private void LoadSuggestions ()
		{
				List<GConsoleItem> sugitems;

				//Not enough characters typed yet, no suggestions to be shown!
		if (minCharBeforeSuggestions != 0 && input.text.Length < minCharBeforeSuggestions) {
						sugitems = new List<GConsoleItem> ();
				} else {
						//Ask GConsole for suggestions, true because we want to have the description too.
						sugitems = GConsole.GetSuggestionItems(input.text);
				}

				//Display suggestions (and hide unused suggestion boxes by passing null).
				for (int i = 0; i < suggestions.Length; i++) {
						if (i < sugitems.Count)
								suggestions [i].ShowSuggestion (sugitems [i]);
						else
								suggestions [i].ShowSuggestion (null);
				}
		}
    
		public void OnSuggestionClicked (string line)
		{
				int index = Convert.ToInt32 (line) - 1;
				//Ugly solution of setting input to the button (suggestion) that was just clicked.
		input.text = suggestions [index].label.text.Split (' ') [0];
		}
}
