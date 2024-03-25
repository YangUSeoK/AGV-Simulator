using UnityEngine;
using System;
using Delegates;

public class Plag : MonoBehaviour, IScaler
{
	private Delegate<Plag> onClickEvent = null;
	private Delegate<Plag> onMouseOverEvent = null;

	[SerializeField] private Material m_SelectedMaterial = null;
	[SerializeField] private Material m_OrigMaterial = null;


	// 지금 현재 위치에 봇이 있는가?
	// 현재 위치에서 봇이 있다가 사라졌는가?
	// 나를 타겟으로 하고 있는 봇이 있는가?



	public bool IsSpawnPlag { get; set; }
	public Vector3 Pos => this.transform.position;

	private Bot m_CurBot = null;
	public Bot CurBot { set { m_CurBot = value; } }
	public bool IsBotHere => (m_CurBot != null) && Vector3.Distance(this.transform.position, m_CurBot.Pos) <= 1.5f;

	// TODO Queue로 만드는거 생각해보기. 우선순위 큐 쓰면 될듯?
	private Bot m_InComingBot = null;
	public Bot InComingBot
	{
		get => m_InComingBot;
		set { m_InComingBot = value; }
	}

	public bool IsNextBot => (m_InComingBot != null);


	private bool m_IsAddMode = false;
	public bool IsAddMode { set { m_IsAddMode = value; } }

	private readonly float m_UpScaleValue = 1.5f;
	private Vector3 m_UpScale => new Vector3(m_OrigScale.x * m_UpScaleValue, m_OrigScale.y * m_UpScaleValue, m_OrigScale.z * m_UpScaleValue);
	private Vector3 m_OrigScale = Vector3.zero;

	private Renderer m_Renderer = null;


	private void Awake()
	{
		m_OrigScale = this.transform.localScale;
		m_Renderer = GetComponentInChildren<Renderer>();
	}

	public void OnMouseUp()
	{
		if (!m_IsAddMode) return;
		onClickEvent?.Invoke(this);
	}

	public void OnMouseEnter()
	{
		if (!m_IsAddMode) return;
		onMouseOverEvent?.Invoke(this);

		SetScale(m_UpScale);
	}

	public void OnMouseExit()
	{
		if (!m_IsAddMode) return;
		SetScale(m_OrigScale);
	}

	public void ArriveBot(Bot _bot)
	{
		m_CurBot = _bot;
		m_InComingBot = null;
	}

	public void Selected(in bool _isSelected)
	{
		if (_isSelected)
		{
			m_Renderer.material = m_SelectedMaterial;
		}
		else
		{
			m_Renderer.material = m_OrigMaterial;
		}
	}

	public void SetScale(in Vector3 _scale)
	{
		this.transform.localScale = _scale;
	}

	public void SetOnClickEvent(Delegate<Plag> _onClickEvent)
	{
		onClickEvent = _onClickEvent;
	}

	public void SetOnMouseEnterEvent(Delegate<Plag> _onMouseEnterEvent)
	{
		onMouseOverEvent = _onMouseEnterEvent;
	}
}

