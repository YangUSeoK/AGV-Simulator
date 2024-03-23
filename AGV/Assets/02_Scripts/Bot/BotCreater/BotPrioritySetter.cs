using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BotPrioritySetter : BotCreateSetter
{
	private Delegates.VoidInt applyCallback = null;
	private Delegates.VoidInt cancelCallback = null;

	[SerializeField] private TMP_InputField m_PriorityInputField = null;
	[SerializeField] private Button m_ApplyButton = null;
	[SerializeField] private Button m_CancelButton = null;
	[SerializeField] private Button m_BackButton = null;

	public void SetCallback(Delegates.VoidInt _applyCallback, Delegates.VoidInt _cancelCallback)
	{
		applyCallback = _applyCallback;
		cancelCallback = _cancelCallback;
	}

	public override void Init()
	{
		m_PriorityInputField.text = string.Empty;
	}

	protected override void setButtonEvent()
	{
		m_ApplyButton.onClick.AddListener(() =>
		{
			applyCallback?.Invoke(Convert.ToInt32(m_PriorityInputField.text));
		});

		m_CancelButton.onClick.AddListener(() =>
		{
			cancelCallback?.Invoke(0);
		});

		//TODO
		m_BackButton.onClick.AddListener(() => { });
	}

	protected override void setPlagsOnClickEvent(in Plag _plag)
	{

	}
}
