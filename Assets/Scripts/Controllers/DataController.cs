using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataController : Singleton<DataController>
{
	public Dictionary<string, string> registers =  new Dictionary<string, string>();
	public event Events.SimpleEvent OnReset;
	public Account account;

	private void Start()
	{
		OnReset += Reset;
		LoadSearch();
	}

	public void Reset()
	{
		registers.Clear();
		account = null;
	}

	public void ResetGame()
	{
		if (OnReset != null) OnReset();
	}

	public void AddValueRegister(string key, string value)
	{
		if (!ContaisKey(key))
		{
			registers.Add(key, value);
		}
	}
    
	private bool ContaisKey(string key)
	{
		return registers.ContainsKey(key);
	}

	public void SubmitRegisters()
	{
		GameRequest.Instance.PostRegister(registers);
	}

	public void LoadSearch()
	{
		GameRequest.Instance.LoadQuestions();
	}
}
