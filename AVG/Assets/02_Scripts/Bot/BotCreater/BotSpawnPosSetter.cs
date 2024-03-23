using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotSpawnPosSetter : BotCreateSetter
{
	private Action<Plag> applyCallback = null;

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
	private void OnMouseEnterEvent(Plag _plag)
	{
		m_Preview?.OnPlagEnterEvent(_plag);
	}

	public void SetCallback(Action<Plag> _applyCallback, Action<Action<Plag>> _onMouseEnterCallback)
	{
		_onMouseEnterCallback?.Invoke(OnMouseEnterEvent);

		applyCallback = _applyCallback;
	}

	public override void Init()
	{
		m_Preview?.SetActive(false);
	}

	protected override void SetButtonEvent()
	{
		//TODO
		m_BackButton.onClick.AddListener(() => { });
	}

	protected override void SetPlagsOnClickEvent(Plag _plag)
	{
		if (_plag.IsSpawnPlag) return;

		_plag.Selected(true);
		applyCallback?.Invoke(_plag);
		Init();
	}

}
