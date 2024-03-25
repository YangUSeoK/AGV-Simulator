using UnityEngine;
using Delegates;

public class BotState_Move : BotState
{
	public BotState_Move(in Bot _bot, in BotStateMachine _matchine) : base(_bot, _matchine) { }

	
	private bool m_IsMoveWait = false;

	public void StartSimulation()
	{
		m_Bot.SetNavMeshStop(false);
	}

	public void StopSimulation()
	{
		m_Bot.SetNavMeshStop(true);
	}
	

	public override void EnterState()
	{
		m_Bot.SetNavMeshStop(false);
	}

	public override void CheckState()
	{
		// 내가 갈 다음 노드를 목적지로 하고있는 로봇이 있다면, 그 로봇이 우선순위가 높다면 Wait
		if (m_Bot.CantGoNext)
		{
			m_IsMoveWait = true;
		}

		// 도착한 노드가 Load 노드라면 Load
		// 도착한 노드가 Unload 노드라면 Unload
	}

	public override void UpdateState()
	{
		if (m_Bot.IsArrive)
		{
			if (m_IsMoveWait)
			{
				m_Machine.SetState(m_Machine.MoveWaitState);
				return;
			}
			m_Bot.ArriveAtDest();
		}
	}

	public override void ExitState()
	{
		m_IsMoveWait = false;
		m_Bot.SetNavMeshStop(true);
	}
}