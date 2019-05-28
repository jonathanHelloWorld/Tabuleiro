using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(InputField))]
public class InputViewJN : MonoBehaviour, ISelectHandler
{
	[HideInInspector]
	public InputField input;

	private void Awake()
	{
		input = GetComponent<InputField>();
		input.onEndEdit.AddListener(EndEdit);
#if UNITY_5_3
            input.onValueChanged.AddListener(ValueChanged);
#else
		input.onValueChanged.AddListener(ValueChanged);
#endif
	}

	void Update()
    {
        
    }

	protected virtual void EndEdit(string value) { }
	protected virtual void ValueChanged(string value) { }
	public virtual void OnSelect(BaseEventData eventData) { }
}
