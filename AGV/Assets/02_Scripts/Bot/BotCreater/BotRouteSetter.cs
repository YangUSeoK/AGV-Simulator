using Delegates;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


// 이미 추가한 플래그 다시 클릭하면 리무브 호출하기

public class BotRouteSetter : BotCreaterWindow
{
	private Delegate<List<Flag>> applyRouteDelegate = null;

	[SerializeField] private TMP_Text m_PathText = null;
	[SerializeField] private Button m_BackspaceButton = null;
	[SerializeField] private Button m_ClearButton = null;
	[SerializeField] private Button m_ApplyButton = null;

	private readonly List<Flag> m_SelectedFlagList = new List<Flag>();

	protected override void clear()
	{
		foreach (var flag in m_SelectedFlagList)
		{
			flag.Selected(false);
		}
		m_SelectedFlagList.Clear();
		m_PathText.text = string.Empty;
	}

	public override void Init()
	{
		clear();
	}

	protected override void setDelegate(in CreaterWindowDelegates<Bot> _delegates)
	{
		var delegates = _delegates as BotRouteSetterDelegates;
		applyRouteDelegate = delegates.ApplyRouteDelegate;
	}

	protected override void setButtonEvent()
	{
		m_BackspaceButton.onClick.AddListener(() =>
		{
			if (m_SelectedFlagList.Count == 0) return;

			Flag lastFlag = m_SelectedFlagList.Last();
			lastFlag.Selected(false);
			m_PathText.text = m_PathText.text[..(m_PathText.text.Length - 4 - lastFlag.name.Length)];
			m_SelectedFlagList.Remove(lastFlag);

		});

		m_ClearButton.onClick.AddListener(() =>
		{
			clear();
		});

		m_ApplyButton.onClick.AddListener(() =>
		{
			applyRouteDelegate?.Invoke(m_SelectedFlagList.ToList());
			clear();
		});
	}

	protected override void flagsOnClickEvent(in Flag _flag)
	{
		m_SelectedFlagList.Add(_flag);

		m_PathText.text += $" => {_flag.name}";

		_flag.Selected(true);
	}

	
}

public class BotRouteSetterDelegates : BotCreaterWindowDelegates
{
	public BotRouteSetterDelegates(in Delegate<Delegate<Flag>> _setModeCallback, 
								   in Delegate<List<Flag>> _applyRouteCallback) 
							     : base(_setModeCallback)
	{
		ApplyRouteDelegate = _applyRouteCallback;
	}

	public Delegate<List<Flag>> ApplyRouteDelegate { get; set; }
}
