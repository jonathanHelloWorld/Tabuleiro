using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonResetData : MonoBehaviour
{
	private Button _btn;

	void Start()
	{
		_btn = GetComponent<Button>();

		_btn.onClick.AddListener(() =>
		{
			StartCoroutine(Wait());
		});
	}

	IEnumerator Wait()
	{
		yield return new WaitForSeconds(0.6f);
		DataController.Instance.ResetGame();
	}
}
