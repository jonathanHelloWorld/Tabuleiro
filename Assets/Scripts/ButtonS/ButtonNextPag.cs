using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonNextPag : MonoBehaviour
{
	public int pageID;
	private Button _btn;

    void Start()
    {
		_btn = GetComponent<Button>();

		_btn.onClick.AddListener(() =>
	   {
		   Pagination.Instance.OpenPage(pageID);
	   });
    }
}
