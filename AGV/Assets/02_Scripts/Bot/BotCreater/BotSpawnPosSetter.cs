using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotSpawnPosSetter : BotCreateSetter
{
	private Delegates.VoidPlag applyCallback = null;

	[SerializeField] private Button m_BackButton = null;

	[SerializeField] private GameObject m_BotPreviewPrefab = null;
	private BotPreview m_Preview = null;

	private void Start()
	{
		m_Preview = Instantiate(m_BotPreviewPrefab).GetComponent<BotPreview>();
		Init();
		m_Preview?.SetActive(true);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		m_Preview?.SetActive(true);
	}

	private void OnDisable()
	{
		m_Preview?.SetActive(false);
	}


	// �÷����� ���콺Enter �̺�Ʈ�� ������ �Լ� ���
	private void OnMouseEnterEvent(in Plag _plag)
	{
		m_Preview?.OnPlagEnterEvent(_plag);
	}

	public void SetCallback(Delegates.VoidPlag _applyCallback, Delegates.VoidAction_VoidPlag _onMouseEnterCallback)
	{
		_onMouseEnterCallback?.Invoke(OnMouseEnterEvent);

		applyCallback = _applyCallback;
	}

	public override void Init()
	{
		m_Preview?.SetActive(false);
	}

	protected override void setButtonEvent()
	{
		//TODO
		m_BackButton.onClick.AddListener(() => { });
	}

	protected override void setPlagsOnClickEvent(in Plag _plag)
	{
		if (_plag.IsSpawnPlag) return;

		_plag.Selected(true);
		applyCallback?.Invoke(_plag);
		Init();
	}

}
