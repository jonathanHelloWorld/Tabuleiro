using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAsk : MonoBehaviour
{

	public string txtAsk;
	public Button buttonFriend;
	[Space]
	//public Color normal;
	//public Color Pressed;

	public Sprite normalS;
	public Sprite normalN;
	public Sprite pressed;

	private Button _btn;
	private QuestionP questionP;

    void Start()
    {
		questionP = GetComponentInParent<QuestionP>();
		_btn = GetComponent<Button>();
		_btn.onClick.AddListener(() =>
		{
			questionP.answer = txtAsk;
			_btn.image.sprite = pressed;
			buttonFriend.image.sprite = normalN;

			AudioManagerr.Instance.PlayLetterSong();

		});

		DataController.Instance.OnReset += Reset;
		PesquisaController.Instance.OnResetAlts += Reset;
    }

	public void Reset()
	{
		_btn.image.sprite = normalS;
	}
}
