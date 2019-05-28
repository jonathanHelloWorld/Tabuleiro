using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class RegisterEnableOnFieldsOkJN : MonoBehaviour
{
	private CanvasGroup _cv;
	private InitKeyboard _registerController;

	private  void Start()
	{
		_registerController = GameObject.FindObjectOfType<InitKeyboard>();
		_cv = GetComponent<CanvasGroup>();
	}

	private void Update()
	{
		_cv.interactable = !_registerController.FieldsBools.Contains(false);
	}
}
