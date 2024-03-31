using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Delegates;

public class BotPrioritySetter : BotCreaterWindow
{
	private Delegate<int> applyDelegate = null;
	private Delegates.Delegate cancelDelegate = null;

	[SerializeField] private TMP_InputField m_PriorityInputField = null;
	[SerializeField] private Button m_ApplyButton = null;
	[SerializeField] private Button m_CancelButton = null;
	[SerializeField] private Button m_BackButton = null;

	public override void Init()
	{
		clear();
	}

	protected override void setDelegate(in CreaterWindowDelegates<Bot> _delegates)
	{
		var delegates = _delegates as BotPrioritySetterDelegates;

		applyDelegate = delegates.ApplyDelegate;
		cancelDelegate = delegates.CancelDelegate;
	}

	protected override void setButtonEvent()
	{
		m_ApplyButton.onClick.AddListener(() =>
		{
			int priority = 0;
			if (m_PriorityInputField.text != string.Empty)
			{
				priority = Convert.ToInt32(m_PriorityInputField.text);
			}

			applyDelegate?.Invoke(priority);
		});

		m_CancelButton.onClick.AddListener(() =>
		{
			cancelDelegate?.Invoke();
		});

		// TODO..
		m_BackButton.onClick.AddListener(() => { });
	}

	protected override void flagsOnClickEvent(in Flag _flag)
	{

	}

	protected override void clear()
	{
		m_PriorityInputField.text = string.Empty;
	}

}

public class BotPrioritySetterDelegates : BotCreaterWindowDelegates
{
	public BotPrioritySetterDelegates(in Delegate<Delegate<Flag>> _setModeCallback, 
									  in Delegate<int> _applyCallback,
									  in Delegates.Delegate _cancelCallback) 
									: base(_setModeCallback)
	{
		ApplyDelegate = _applyCallback;
		CancelDelegate = _cancelCallback;
	}

	public Delegate<int> ApplyDelegate { get; }
	public Delegates.Delegate CancelDelegate { get; }
}
