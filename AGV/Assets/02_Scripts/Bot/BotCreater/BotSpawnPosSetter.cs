using Delegates;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotSpawnPosSetter : BotCreateSetter
{
	private Delegate<Flag> applyDelegate = null;

	[SerializeField] private Button m_BackButton = null;

	[SerializeField] private GameObject m_BotPreviewPrefab = null;
	private BotPreview m_Preview = null;

	private void Start()
	{
		m_Preview = Instantiate(m_BotPreviewPrefab).GetComponent<BotPreview>();
		Clear();
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
	private void OnMouseEnterEvent(in Flag _plag)
	{
		m_Preview?.OnPlagEnterEvent(_plag);
	}

	public void SetCallback(Delegate<Flag> _applyCallback, Delegate<Delegate<Flag>> _onMouseEnterCallback)
	{
		_onMouseEnterCallback?.Invoke(OnMouseEnterEvent);

		applyDelegate = _applyCallback;
	}

	public override void Clear()
	{
		m_Preview?.SetActive(false);
	}

	protected override void setButtonEvent()
	{
		//TODO
		m_BackButton.onClick.AddListener(() => { });
	}

	protected override void setPlagsOnClickEvent(in Flag _plag)
	{
		if (_plag.IsSpawnPlag) return;

		applyDelegate?.Invoke(_plag);
		Clear();
	}

}
