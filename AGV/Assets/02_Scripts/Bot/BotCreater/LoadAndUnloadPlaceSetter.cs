using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


// ��� ���� �÷��׸� �����ϸ� ���� ����

public class LoadAndUnloadPlaceSetter : BotCreateSetter
{
	public delegate void VoidPlagPlagDelegate(in Flag _loadPlag, in Flag _unloadPlag);
	private VoidPlagPlagDelegate applyCallback = null;

	[SerializeField] private TMP_Text m_LoadText = null;
	[SerializeField] private TMP_Text m_UnloadText = null;
	[SerializeField] private Button m_SetLoadPlaceButton = null;
	[SerializeField] private Button m_SetUnloadPlaceButton = null;
	[SerializeField] private Button m_ApplyButton = null;
	[SerializeField] private Button m_BackButton = null;

	private Flag m_LoadPlag = null;
	private Flag m_UnloadPlag = null;

	public override void Init()
	{
		m_LoadPlag?.Selected(false);
		m_UnloadPlag?.Selected(false);
		m_LoadPlag = null;
		m_UnloadPlag = null;
		m_LoadText.text = "Load : ";
		m_UnloadText.text = "Unload : ";
	}

	public void SetCallback(VoidPlagPlagDelegate _applyLoadAndUnloadPlaceCallback)
	{
		applyCallback = _applyLoadAndUnloadPlaceCallback;
	}
	
	protected override void setButtonEvent()
	{
		m_ApplyButton.onClick.AddListener(() =>
		{
			applyCallback?.Invoke(m_LoadPlag, m_UnloadPlag);
			Init();
		});
		m_SetLoadPlaceButton.onClick.AddListener(() => setModeDelegate?.Invoke(setLoadPlace));
		m_SetUnloadPlaceButton.onClick.AddListener(() => setModeDelegate?.Invoke(setUnloadPlace));

		//TODO
		m_BackButton.onClick.AddListener(() => { });
	}

	protected override void setPlagsOnClickEvent(in Flag _plag)
	{
		setLoadPlace(_plag);
	}

	// �÷��׸� Ŭ���ϸ� ����� �Լ�
	private void setLoadPlace(in Flag _loadPlag)
	{
		m_LoadPlag?.Selected(false);
		m_LoadPlag = _loadPlag;
		_loadPlag.Selected(true);
		m_LoadText.text = $"Load : {_loadPlag.name}";
	}

	private void setUnloadPlace(in Flag _unloadPlag)
	{
		m_UnloadPlag?.Selected(false);
		m_UnloadPlag = _unloadPlag;
		_unloadPlag.Selected(true);
		m_UnloadText.text = $"Unload : {_unloadPlag.name}";
	}
}
