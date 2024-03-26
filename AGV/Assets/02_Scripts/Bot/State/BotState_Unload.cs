using Delegates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotState_Unload : BotState
{
	public BotState_Unload(in Bot _bot, in BotStateMachine _matchine) : base(_bot, _matchine) { }

	private readonly float loadTime = 5.0f;
	private float m_Timer = 0;

	public override void EnterState()
	{
		m_Bot.SetMaterialByStateEnum(EBotState.Unload);
		m_Timer = 0f;
	}

	public override void CheckState()
	{
		m_Timer += Time.deltaTime;
		if (m_Timer >= loadTime)
		{
			m_Bot.FinishLoadUnload();
		}
	}

	public override void UpdateState()
	{
	}

	public override void ExitState()
	{
		m_Timer = 0f;
	}
}
