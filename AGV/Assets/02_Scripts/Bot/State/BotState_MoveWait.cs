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
		// ���� �� ���� ��带 �������� �ϰ��ִ� �κ��� �ִٸ�, �� �κ��� �켱������ ���ٸ� Wait
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
