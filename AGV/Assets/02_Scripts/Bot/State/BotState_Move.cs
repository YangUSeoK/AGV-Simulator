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
		// ���� �� ���� ��带 �������� �ϰ��ִ� �κ��� �ִٸ�, �� �κ��� �켱������ ���ٸ� Wait
		if (m_Bot.CantGoNext)
		{
			m_IsMoveWait = true;
		}

		// ������ ��尡 Load ����� Load
		// ������ ��尡 Unload ����� Unload
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