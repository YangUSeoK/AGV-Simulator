using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


// 이미 추가한 플래그 다시 클릭하면 리무브 호출하기

public class RouteSetter : BotCreateSetter
{
	private Action<List<Plag>> applyRouteCallback = null;

	[SerializeField] private TMP_Text m_PathText = null;
	[SerializeField] private Button m_BackspaceButton = null;
	[SerializeField] private Button m_ClearButton = null;
	[SerializeField] private Button m_ApplyButton = null;

	private readonly List<Plag> m_SelectedPlagList = new List<Plag>();

	public void SetCallback(Action<List<Plag>> _applyRouteCallback)
	{
		applyRouteCallback = _applyRouteCallback;
	}

	public override void Init()
	{
		foreach(var plag in m_SelectedPlagList)
		{
			plag.Selected(false);
		}
		m_SelectedPlagList.Clear();
		m_PathText.text = string.Empty;
	}

	protected override void SetButtonEvent()
	{
		m_BackspaceButton.onClick.AddListener(() =>
		{
			if (m_SelectedPlagList.Count == 0) return;

			Plag lastPlag = m_SelectedPlagList.Last();
			lastPlag.Selected(false);
			m_PathText.text = m_PathText.text[..(m_PathText.text.Length - 4 - lastPlag.name.Length)];
			m_SelectedPlagList.Remove(lastPlag);

		});

		m_ClearButton.onClick.AddListener(() =>
		{
			Init();
		});

		m_ApplyButton.onClick.AddListener(() =>
		{
			applyRouteCallback?.Invoke(m_SelectedPlagList.ToList());
			Init();
		});
	}

	protected override void SetPlagsOnClickEvent(Plag _plag)
	{
		m_SelectedPlagList.Add(_plag);

		m_PathText.text += $" => {_plag.name}";

		_plag.Selected(true);
	}
}
