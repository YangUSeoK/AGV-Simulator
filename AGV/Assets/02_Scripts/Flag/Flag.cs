using UnityEngine;
using System.Collections.Generic;

public class Flag : Item<Flag>
{
	public bool IsSpawnFlag => m_SpawnedBot != null;

	public bool IsLoadLocation => m_LoadLocation != null;
	public bool IsUnloadLocation => m_UnloadLocation != null;

	public bool IsExistIncomingtBot => m_IncomingBot != null;

	private readonly Queue<Bot> m_IncomingBotQueue = new Queue<Bot>();
	private Bot m_IncomingBot = null;
	public Bot IncomingBot => m_IncomingBot;

	private Bot m_SpawnedBot = null;
	public Bot SpawnedBot { set { m_SpawnedBot = value; } }

	private Location m_LoadLocation = null;
	public Location LoadLocation { set { m_LoadLocation = value; } }

	private Location m_UnloadLocation = null;
	public Location UnloadLocation { set { m_UnloadLocation = value; } }

	public bool CanLoad => m_LoadLocation.CanLoad;

	private readonly float m_LeaveDistance = 1.5f;
	private float m_IncomingBotPostDistance = Mathf.Infinity;


	private void Update()
	{
		if (m_IncomingBot == null)
		{
			if (m_IncomingBotQueue.Count > 0)
			{
				m_IncomingBot = m_IncomingBotQueue.Dequeue();
				m_IncomingBot.GoNextFlag();
			}
		}
		else
		{
			float distance = Vector3.Distance(m_IncomingBot.Pos, this.transform.position);

			// 방금 전 프레임보다 지금이 더 멀다면 => 나가는 중
			if (m_IncomingBotPostDistance <= distance)
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

	public void EnqueueIncomingBot(in Bot _incomingBot)
	{
		if (m_IncomingBotQueue.Contains(_incomingBot)) return;

		m_IncomingBotQueue.Enqueue(_incomingBot);
	}

	public override void FinishSimulation()
	{
		m_IncomingBotQueue.Clear();
		onClickDelegate = null;
		onMouseEnterDelegate = null;
		onMouseExitDelegate = null;
	}

	public int Load(in int _cnt)
	{
		return m_LoadLocation.Load(_cnt);
	}

	public void Unload(in int _cnt)
	{
		m_UnloadLocation.Unload(_cnt);
	}

	protected override void onClick()
	{
		onClickDelegate?.Invoke(this);
	}

	protected override void onMouseEnter()
	{
		onMouseEnterDelegate?.Invoke(this);
		SetScale(m_UpScale);
	}

	protected override void onMouseExit()
	{
		onMouseExitDelegate?.Invoke(this);
		SetScale(m_OrigScale);
	}

	protected override void clear()
	{
		m_IncomingBotQueue.Clear();
		m_SpawnedBot = null;
		m_LoadLocation = null;
		m_UnloadLocation = null;
	}


	public override void StartSimulation() { }
	public override void Init(in ItemInfo<Flag> _container) { }
}

