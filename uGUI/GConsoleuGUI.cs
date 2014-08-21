using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class GConsoleuGUI : MonoBehaviour
{

		public Text consoleOutput;
		public InputField input;
		public GConsoleuGUISuggestion[] suggestions;
		public bool clearOnSubmit = false;
		public bool reselectOnSubmit = true;
		public int minCharBeforeSuggestions;

		void Start ()
		{
				//Register the "OnOutput" method as a listener for console output.
				GConsole.OnOutput += OnOutput;
				input.GetComponent<GConsoleuGUIInput> ().uGUI = this;
				//Fire the OnChange method whenever input changes (so we can then update the suggestions).
				//EventDelegate.Add(input.onChange, OnChange);
		}

		void OnOutput (string line)
		{
				Debug.Log ("OnOutput(" + line + ")");
				consoleOutput.text += '\n' + line;
		}

		public void OnInput ()
		{
				Debug.Log ("OnInput(" + input.value + ")");
				string cmd = input.value;

				if (string.IsNullOrEmpty (cmd))
						return;


				//Send command to the console
				GConsole.Eval (cmd);

				if (clearOnSubmit) {
						input.value = string.Empty;
						input.transform.FindChild("gc_Input_text").GetComponent<Text> ().text = string.Empty;
				}
				if (reselectOnSubmit) //Has to be done in next frame for NGUI reasons (quirk in NGUI)..
						StartCoroutine (SelectInputNextFrame ());
		}

		IEnumerator SelectInputNextFrame ()
		{
				yield return null;
				//input.;

		}

		IEnumerator ClearInputNextFrame ()
		{
				yield return null;
				input.value = "";
				input.transform.FindChild("gc_Input_text").GetComponent<Text> ().text = string.Empty;

		}


		/* void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }
    }*/


		void OnEnable ()
		{
				StartCoroutine (ClearInputNextFrame ()); //Necessary because otherwise the input field will contain the letter used to open the UI.
		}

		public void OnChange ()
		{
				LoadSuggestions ();
		}

		private void LoadSuggestions ()
		{
				List<String> sugstrings;

				//Not enough characters typed yet, no suggestions to be shown!
				if (minCharBeforeSuggestions != 0 && input.value.Length < minCharBeforeSuggestions) {
						sugstrings = new List<String> ();
				} else {
						//Ask GConsole for suggestions, true because we want to have the description too.
						sugstrings = GConsole.GetSuggestions (input.value, true);
				}

				//Display suggestions (and hide unused suggestion boxes by passing null).
				for (int i = 0; i < suggestions.Length; i++) {
						if (i < sugstrings.Count)
								suggestions [i].ShowSuggestion (sugstrings [i]);
						else
								suggestions [i].ShowSuggestion (null);
				}
        
		}
    
		public void OnSuggestionClicked (string line)
		{
				//Ugly solution of setting input to the button (suggestion) that was just clicked.
				/*input.value =
          NGUIText.StripSymbols(
          UIButton.current.GetComponent<GConsoleNGUISuggestion>().label.text.Split(' ')[0]
          + " ");
		*/

				//Reselect the input the next frame (quirk in NGUI, can't do it the current).
				StartCoroutine (SelectInputNextFrame ());
		}
}
