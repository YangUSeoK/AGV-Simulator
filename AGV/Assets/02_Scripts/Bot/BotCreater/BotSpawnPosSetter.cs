using Delegates;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotSpawnPosSetter : BotCreateSetter
{
	private Delegate<Plag> applyDelegate = null;

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


	// 플래그의 마우스Enter 이벤트에 프리뷰 함수 등록
	private void OnMouseEnterEvent(in Plag _plag)
	{
		m_Preview?.OnPlagEnterEvent(_plag);
	}

	public void SetCallback(Delegate<Plag> _applyCallback, Delegate<Delegate<Plag>> _onMouseEnterCallback)
	{
		_onMouseEnterCallback?.Invoke(OnMouseEnterEvent);

		applyDelegate = _applyCallback;
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

		applyDelegate?.Invoke(_plag);
		Init();
	}

}
