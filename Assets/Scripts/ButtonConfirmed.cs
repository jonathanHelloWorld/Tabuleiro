using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonConfirmed : MonoBehaviour
{
	public bool isTablet = false;
	private Button _btn;

    void Start()
    {
		_btn = GetComponent<Button>();

		_btn.onClick.AddListener(() =>
		{
			AudioManagerr.Instance.PlayLetterSong();

			if (!isTablet)
				GameRequest.Instance.SubmitSearchs();
			else
			{
				PesquisaController.Instance.AddValuesInQuestion();
				PesquisaController.Instance.ResetAlternativaColor();
				////TESTE
				//PesquisaController.Instance.indexCurrent = idTeste;
				//PesquisaController.Instance.SetCurrentQuestion();
			}
		});
    }

    void Update()
    {
		_btn.interactable = PesquisaController.Instance.AnswersIsCompleted();
    }
}
