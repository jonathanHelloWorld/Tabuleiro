using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupController : MonoBehaviour
{
	public Image popup;
	public List<Sprite> sprites;

    void Start()
    {
		GameManager.Instance.OnPopup += SetPopup;
    }

	public void SetPopup(int id)
	{
		popup.sprite = sprites[id];
	}
}
