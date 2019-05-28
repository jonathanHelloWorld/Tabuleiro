using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalizarTxt : MonoBehaviour
{
	[HideInInspector]public Text _txt;

    void Start()
    {
		_txt = GetComponent<Text>();
		GameManager.Instance.OnFinish += UpdateTxt;
    }

	public void UpdateTxt(string txt)
	{
		_txt.text = txt;
	}
}
