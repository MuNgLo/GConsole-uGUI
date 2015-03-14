using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using System;

[AddComponentMenu("Scripts/Gconsole-uGUI/GConsoleuGUI")]
public class GConsoleuGUI : MonoBehaviour
{
	public int max_OutputLength = 500; // If we put to much text in the textarea Unity will go nuts
	public Text consoleOutput;
	public InputField input;
	public GConsoleuGUISuggestion[] suggestions;
	public bool clearOnSubmit = false;
	public bool reselectOnSubmit = true;
	public int minCharBeforeSuggestions = 1;

	void Start ()
	{
		GConsole.OnOutput += OnOutput;	//Register the "OnOutput" method as a listener for console output.
		input.GetComponent<GConsoleuGUIInput> ().uGUI = this;
		foreach(GConsoleuGUISuggestion sugg in suggestions) {
			sugg.uGUI = this;
		}
	}

	void OnEnable ()
	{
		input.Select ();
		input.ActivateInputField ();
	}

	void OnOutput (string line)
	{
		if (consoleOutput.text.Length > max_OutputLength) {  // Shorten the textlength so Unity can handle it
			consoleOutput.text = consoleOutput.text.Substring( ( consoleOutput.text.Length - max_OutputLength ), max_OutputLength );
		}
		consoleOutput.text += '\n' + line;	// add the console output to the output textarea TODO make this clamp length
	}

	public void OnInput ()
	{
		string cmd = input.text;
		if (string.IsNullOrEmpty (cmd)) {
			return;
		}
		GConsole.Eval (cmd);	//Send command to the console
		if (clearOnSubmit) {
			input.text = string.Empty;
		}
		if (reselectOnSubmit) {
			input.Select ();
			input.ActivateInputField ();
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
			sugitems = GConsole.GetSuggestionItems (input.text);	//Ask GConsole for suggestions.
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
		input.text = line.Split(' ')[0];
		input.Select ();
		input.ActivateInputField ();
		input.MoveTextEnd (false);
	}
}
