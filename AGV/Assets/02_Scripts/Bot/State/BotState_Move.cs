using UnityEngine;
using Delegates;

public class BotState_Move : BotState
{
	public BotState_Move(in Bot _bot, in BotStateMachine _matchine) : base(_bot, _matchine) { }

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
		m_Bot.SetMaterialByStateEnum(EBotState.Move);
	}

	public override void CheckState()
	{
		// 도착한 노드가 Load 노드라면 LoadWait
		// 도착한 노드가 Unload 노드라면 Unload
	}

	public override void UpdateState()
	{
		if (m_Bot.IsArrive)
		{
			m_Bot.ArriveAtDestFlag();
		}
	}

	public override void ExitState()
	{
		m_Bot.SetNavMeshStop(true);
	}
}