using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RegisterInputJN : InputViewJN
{
	protected InitKeyboard _registerController;
	public Text Label;
	[Space]
	public string DataName;
	public CheckType CheckType;
	public bool IsFirstSelected;

	[Space]
	public bool IsUnique;

	[HideInInspector]
	public bool _isOk;

	private bool _isReseting = false;

	public GameObject CheckFeddback;
	private int _id;

	private void Start()
	{
		_registerController = GameObject.FindObjectOfType<InitKeyboard>();
		Debug.Log(_registerController.name);
		_registerController.OnSubmit += Submit;

		//CheckFeddback.SetActive(CheckType != CheckType.Generic && !_isOk);
		CheckFeddback.SetActive(!_isOk);

		_registerController.FieldsBools.Add(_isOk);
		_id = _registerController.FieldsBools.Count - 1;

		DataController.Instance.OnReset += Reset;

		if (IsFirstSelected)
		{
			_registerController.ActiveField = this;
		}

		//_timeController.RegisterInJson += AddNewValues;
		//input.onEndEdit.AddListener(delegate { _registerController.CloseKeyboard(); });
		//input.OnSelect.AddListener(delegate { _registerController.CloseKeyboard(); });
	}

	void Reset()
	{
		Debug.Log("reset");

		if (input == null) return;

		_isReseting = true;
		input.text = "";
		ChackString(input.text);
		_registerController.FieldsBools[_id] = _isOk;

		CheckFeddback.SetActive(true);

		if (IsFirstSelected)
		{
			_registerController.ActiveField = this;
		}
		_isReseting = false;
	}

	void ChackString(string value)
	{
		switch (CheckType)
		{
			case CheckType.Generic:
				_isOk = !string.IsNullOrEmpty(value);
				break;
			case CheckType.CPF:
				_isOk = Utils.CpfValid(value);
				break;
			case CheckType.Mail:
				_isOk = Utils.EmailIsValid(value);
				break;
			case CheckType.Phone:
				string t = value.Replace("(", "").Replace(")", "");
				_isOk = t.Length >= 10;
				break;
			case CheckType.CRM:
				Match match = Regex.Match(value, @"[A-Za-z]{2}[0-9]{7}");
				_isOk = match.Success;
				break;
		}
	}
	public override void OnSelect(BaseEventData eventData)
	{
		if (_isReseting) return;
		base.OnSelect(eventData);

		//if (_bootstrap.IsMobile) return;
		//_registerController.ShowKeyboard();
		_registerController.ActiveField = this;
	}

	protected override void ValueChanged(string value)
	{
		if (_isReseting) return;

		ChackString(value);
		_registerController.FieldsBools[_id] = _isOk;
	}
	public void AddValues()
	{
		if (_isReseting) return;

		//if (_isOk)
		//	_registerController.AddRegisterValue(DataName, input.text, true);

		if (CheckFeddback != null)
		{
			CheckFeddback.SetActive(!_isOk);
		}
	}

	public void AddNewValues(string DataName, string Value)
	{
		//_registerController.AddRegisterValue(DataName, Value, true);

	}

	protected override void EndEdit(string value)
	{
		if (_isReseting) return;

		//if (_isOk)
		//	_registerController.AddRegisterValue(DataName, value, true);
		if (CheckFeddback != null)
		{
			CheckFeddback.SetActive(!_isOk);
		}
	}
	void Submit()
	{
		if (_isReseting) return;
		DataController.Instance.AddValueRegister(DataName,input.text);
	}
}

