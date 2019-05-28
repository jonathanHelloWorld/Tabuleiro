using System.Collections;
using System.Collections.Generic;
using HeathenEngineering.OSK.v2;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public enum CheckType
{
	Generic = 0,
	Mail = 1,
	CPF = 2,
	Phone = 3,
	CRM = 4
}

public class InitKeyboard : MonoBehaviour
{
	public event Events.SimpleEvent OnSubmit, OnRegistryUpdate;

	[HideInInspector]
	public List<bool> FieldsBools;
	[HideInInspector]
	public RegisterInputJN ActiveField;

	public OnScreenKeyboard OSKeyboard;

	private void Awake()
	{
		FieldsBools = new List<bool>();
	}

	void Start()
    {
		if (OSKeyboard != null)
		{
			OSKeyboard.KeyPressed += new KeyboardEventHandler(KeyboardKeyPressed);
			EventSystem.current.SetSelectedGameObject(OSKeyboard.ActiveKey.gameObject);
		}
	}

	protected void KeyboardKeyPressed(OnScreenKeyboard sender, OnScreenKeyboardArguments args)
	{
		if (ActiveField)
		{
			int caretPos;

			InputField outputText = ActiveField.input;
			switch (args.KeyPressed.type)
			{
				case KeyClass.Backspace:
					if (outputText.text.Length > 0 || outputText.caretPosition > 0)
					{
						caretPos = outputText.caretPosition;

						outputText.text = outputText.text.Remove(outputText.caretPosition - 1, 1);

						outputText.caretPosition = caretPos - 1;
					}
					break;
				case KeyClass.Return:
					outputText.text += args.KeyPressed.ToString();
					break;
				case KeyClass.Shift:
					//No need to do anything here as the keyboard will sort that on its own
					break;
				case KeyClass.String:
					if (outputText.characterLimit > 0 && outputText.text.Length >= outputText.characterLimit) break;

					caretPos = outputText.caretPosition;
					string s = outputText.text;
					outputText.text = outputText.text.Insert(outputText.caretPosition, args.KeyPressed.ToString());
					outputText.caretPosition = caretPos + 1;
					break;
				case KeyClass.ModifySix:
					if (outputText.characterLimit > 0 && outputText.text.Length >= outputText.characterLimit) break;

					caretPos = outputText.caretPosition;
					string x = outputText.text;
					outputText.text = outputText.text.Insert(outputText.caretPosition, args.KeyPressed.ToString());
					outputText.caretPosition = caretPos + 6;
					break;
				case KeyClass.ModifyEight:
					if (outputText.characterLimit > 0 && outputText.text.Length >= outputText.characterLimit) break;

					caretPos = outputText.caretPosition;
					string z = outputText.text;
					outputText.text = outputText.text.Insert(outputText.caretPosition, args.KeyPressed.ToString());
					outputText.caretPosition = caretPos + 8;
					break;
				case KeyClass.ModifyFor:
					if (outputText.characterLimit > 0 && outputText.text.Length >= outputText.characterLimit) break;

					caretPos = outputText.caretPosition;
					string w = outputText.text;
					outputText.text = outputText.text.Insert(outputText.caretPosition, args.KeyPressed.ToString());
					outputText.caretPosition = caretPos + 4;
					break;
				case KeyClass.ModifyTree:
					if (outputText.characterLimit > 0 && outputText.text.Length >= outputText.characterLimit) break;

					caretPos = outputText.caretPosition;
					string o = outputText.text;
					outputText.text = outputText.text.Insert(outputText.caretPosition, args.KeyPressed.ToString());
					outputText.caretPosition = caretPos + 3;
					break;
			}

			ActiveField.AddValues();
		}
	}

	public void Submit()
	{
		//_data.Save();

		if (OnSubmit != null) OnSubmit();
	
		foreach( var print in DataController.Instance.registers)
		{
			Debug.Log(print.Key + ":" + print.Value);
		}

		DataController.Instance.SubmitRegisters();

	}

	void RegistryUpdate()
	{
		if (OnRegistryUpdate != null) OnRegistryUpdate();
	}

}
