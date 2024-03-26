using UnityEngine;
using System;
using Delegates;
using System.Collections.Generic;

public class Flag : MonoBehaviour, IScaler
{
	private Delegate<Flag> onClickEvent = null;
	private Delegate<Flag> onMouseOverEvent = null;

	[SerializeField] private Material m_SelectedMaterial = null;
	[SerializeField] private Material m_OrigMaterial = null;


	// 지금 현재 위치에 봇이 있는가?
	// 현재 위치에서 봇이 있다가 사라졌는가?
	// 나를 타겟으로 하고 있는 봇이 있는가?

	public bool IsSpawnPlag { get; set; }
	public Vector3 Pos => this.transform.position;

	private readonly float m_LeaveDistance = 1.5f;

	// TODO Queue로 만드는거 생각해보기. 우선순위 큐 쓰면 될듯?
	private Bot m_IncomingBot = null;
	public Bot IncomingBot
	{
		get => m_IncomingBot;
	}

	private readonly Queue<Bot> m_IncomingBotQueue = new Queue<Bot>();
	

	public bool IsIncomingtBot => (m_IncomingBot != null);

	public int IncomingBotCnt;

	private bool m_IsAddMode = false;
	public bool IsAddMode { set { m_IsAddMode = value; } }

	private readonly float m_UpScaleValue = 1.5f;
	private Vector3 m_UpScale => new Vector3(m_OrigScale.x * m_UpScaleValue, m_OrigScale.y * m_UpScaleValue, m_OrigScale.z * m_UpScaleValue);
	private Vector3 m_OrigScale = Vector3.zero;

	private Renderer m_Renderer = null;

	private float m_IncomingBotPostDistance = Mathf.Infinity;


	private void Awake()
	{
		m_OrigScale = this.transform.localScale;
		m_Renderer = GetComponentInChildren<Renderer>();
	}

	private void Update()
	{
		IncomingBotCnt = m_IncomingBotQueue.Count;

		if (m_IncomingBot == null)
		{
			if(m_IncomingBotQueue.Count > 0)
			{
				m_IncomingBot = m_IncomingBotQueue.Dequeue();
				m_IncomingBot.GoNextFlag();
			}
		}
		else
		{
			float distance = Vector3.Distance(m_IncomingBot.Pos, this.transform.position);

			// 방금 전 프레임보다 지금이 더 멀다면 => 나가는 중
			if(m_IncomingBotPostDistance <= distance)
			{
				if (m_LeaveDistance <= distance)
				{
					m_IncomingBot = null;
					m_IncomingBotPostDistance = Mathf.Infinity;
					return;
				}
			}

			m_IncomingBotPostDistance = distance;
		}
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

	public void EnqueueIncomingBot(Bot _incomingBot)
	{
		if (m_IncomingBotQueue.Contains(_incomingBot)) return;

		m_IncomingBotQueue.Enqueue(_incomingBot);
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

	public void SetOnClickEvent(Delegate<Flag> _onClickEvent)
	{
		onClickEvent = _onClickEvent;
	}

	public void SetOnMouseEnterEvent(Delegate<Flag> _onMouseEnterEvent)
	{
		onMouseOverEvent = _onMouseEnterEvent;
	}
}

