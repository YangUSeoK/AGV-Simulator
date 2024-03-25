using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotState_MoveWait : BotState
{
	public BotState_MoveWait(in Bot _bot, in BotStateMachine _machine) : base(_bot, _machine) { }

	public override void EnterState()
	{
		Debug.Log("MoveWait.Enter");
	}

	public override void CheckState()
	{
		// 내가 갈 다음 노드를 목적지로 하고있는 로봇이 있다면, 그 로봇이 우선순위가 높다면 Wait
		if (m_Bot.CanGoNext)
		{
			m_Machine.SetState(m_Machine.MoveState);
		}
	}

	public override void UpdateState()
	{
	}

	public override void ExitState()
	{
		Debug.Log("MoveWait.Exit");
	}
}
