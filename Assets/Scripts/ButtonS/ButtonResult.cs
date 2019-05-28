using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonResult : MonoBehaviour
{
	private Button _btn;

	void Start()
	{
		_btn = GetComponent<Button>();

		_btn.onClick.AddListener(() =>
		{
			AudioManagerr.Instance.PlayLetterSong();
			GameRequest.Instance.SubmitResultFinal();
		});
	}

	void Update()
	{
		_btn.interactable = PesquisaController.Instance.ResultIsCompleted();
	}
}
