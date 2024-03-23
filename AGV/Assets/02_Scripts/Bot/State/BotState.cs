using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BotState
{
	public BotState(Bot _bot)
	{
		m_Bot = _bot;
	}
	protected Bot m_Bot = null;

	public abstract void EnterState();
	public abstract void UpdateState();
	public abstract void ExitState();
}
