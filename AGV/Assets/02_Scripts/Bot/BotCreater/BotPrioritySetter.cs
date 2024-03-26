using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Delegates;

public class BotPrioritySetter : BotCreateSetter
{
	private Delegate<int> applyDelegate = null;
	private Delegate<int> cancelDelegate = null;

	[SerializeField] private TMP_InputField m_PriorityInputField = null;
	[SerializeField] private Button m_ApplyButton = null;
	[SerializeField] private Button m_CancelButton = null;
	[SerializeField] private Button m_BackButton = null;

	public void SetCallback(Delegate<int> _applyCallback, Delegate<int> _cancelCallback)
	{
		applyDelegate = _applyCallback;
		cancelDelegate = _cancelCallback;
	}

	public override void Clear()
	{
		m_PriorityInputField.text = string.Empty;
	}

	protected override void setButtonEvent()
	{
		m_ApplyButton.onClick.AddListener(() =>
		{
			int priority = 0;
			if(m_PriorityInputField.text != string.Empty)
			{
				priority = Convert.ToInt32(m_PriorityInputField.text);
			}

			applyDelegate?.Invoke(priority);
		});

		m_CancelButton.onClick.AddListener(() =>
		{
			cancelDelegate?.Invoke(0);
		});

		//TODO
		m_BackButton.onClick.AddListener(() => { });
	}

	protected override void setPlagsOnClickEvent(in Flag _plag)
	{

	}
}
