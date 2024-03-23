using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


// 경로 밖의 플래그를 선택하면 에러 띄우기

public class LoadAndUnloadPlaceSetter : BotCreateSetter
{
	private Action<Plag, Plag> applyCallback = null;

	[SerializeField] private TMP_Text m_LoadText = null;
	[SerializeField] private TMP_Text m_UnloadText = null;
	[SerializeField] private Button m_SetLoadPlaceButton = null;
	[SerializeField] private Button m_SetUnloadPlaceButton = null;
	[SerializeField] private Button m_ApplyButton = null;
	[SerializeField] private Button m_BackButton = null;

	private Plag m_LoadPlag = null;
	private Plag m_UnloadPlag = null;


	// 플래그를 클릭하면 실행될 함수
	private void SetLoadPlace(Plag _loadPlag)
	{
		m_LoadPlag?.Selected(false);
		m_LoadPlag = _loadPlag;
		_loadPlag.Selected(true);
		m_LoadText.text = $"Load : {_loadPlag.name}";
	}

	private void SetUnloadPlace(Plag _unloadPlag)
	{
		m_UnloadPlag?.Selected(false);
		m_UnloadPlag = _unloadPlag;
		_unloadPlag.Selected(true);
		m_UnloadText.text = $"Unload : {_unloadPlag.name}";
	}


	public void SetCallback(Action<Plag, Plag> _applyLoadAndUnloadPlaceCallback)
	{
		applyCallback = _applyLoadAndUnloadPlaceCallback;
	}
	public override void Init()
	{
		m_LoadPlag?.Selected(false);
		m_UnloadPlag?.Selected(false);
		m_LoadPlag = null;
		m_UnloadPlag = null;
		m_LoadText.text = "Load : ";
		m_UnloadText.text = "Unload : ";
	}

	protected override void SetButtonEvent()
	{
		m_ApplyButton.onClick.AddListener(() =>
		{
			applyCallback?.Invoke(m_LoadPlag, m_UnloadPlag);
			Init();
		});
		m_SetLoadPlaceButton.onClick.AddListener(() => setModeCallback?.Invoke(SetLoadPlace));
		m_SetUnloadPlaceButton.onClick.AddListener(() => setModeCallback?.Invoke(SetUnloadPlace));

		//TODO
		m_BackButton.onClick.AddListener(() => { });
	}


	protected override void SetPlagsOnClickEvent(Plag _plag)
	{
		SetLoadPlace(_plag);
	}
}
