using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSendAnswer : ButtonRaiz
{
	
    void Start()
    {
		//btn = GetComponent<Button>();
		OnClick();
		btn.onClick.AddListener(SendAlternative);
		btn.onClick.AddListener(OffInteractable2);


		GameManager.Instance.OnGameStart += OffInteractable;
		GameManager.Instance.OnResposta += OnInteractable;
    }

	public override void OnClick()
	{
		base.OnClick();
	}

	public void OffInteractable()
	{
		btn.interactable = false;
	}

	public void OffInteractable2()
	{
		if (GameManager.Instance._btnPressed != null)
		{
			btn.interactable = false;
		}
	}

	public void OnInteractable()
	{
		btn.interactable = true;
	}

	public void SendAlternative()
	{
		ButtonAlternative refButton = GameManager.Instance._btnPressed;

		if (refButton != null)
		{
			Debug.Log("entrou");

			Dictionary<string, string> dir = new Dictionary<string, string>()
			{
				{"idAlternativa",refButton.letterTxt.text},
				{"pergunta", GameManager.Instance.GetIDQuestion() }
				//{"tempo", GameManager.Instance.timeAnswer.Substring(3)}
			};

			//GameRequest.Instance.PostPontos(dir);
			//GameManager.Instance.SocketEmit();
			GameManager.Instance.AnswerDone();
		}

		Debug.Log("SendAlternative");
	}
}
