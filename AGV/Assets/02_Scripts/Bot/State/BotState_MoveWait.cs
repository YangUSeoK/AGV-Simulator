using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotState_MoveWait : BotState
{
	public BotState_MoveWait(in Bot _bot, in BotStateMachine _machine) : base(_bot, _machine) { }

	public override void EnterState()
	{
		Debug.Log("MoveWait.Enter");
		m_Bot.SetMaterialByStateEnum(EBotState.MoveWait);
	}

	public override void CheckState()
	{
	}

	public override void UpdateState()
	{
	}

	public override void ExitState()
	{
		Debug.Log("MoveWait.Exit");
	}
}
