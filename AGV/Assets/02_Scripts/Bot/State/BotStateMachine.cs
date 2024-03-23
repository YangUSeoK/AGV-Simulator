using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotStateMachine
{
	public BotStateMachine(in Bot _bot)
	{
		m_Bot = _bot;

		m_Move = new BotState_Move(m_Bot);
	}

	private readonly Bot m_Bot = null;
	private readonly BotState_Move m_Move = null;

	private BotState m_CurState = null;


	public void Update()
	{
		m_CurState?.UpdateState();
	}

	public void StartSimulation(in int _startIdx)
	{
		setState(m_Move);
		(m_CurState as BotState_Move).StartSimulation(_startIdx);
	}

	public void StopSimulation()
	{
		setState(null);
	}

	private void setState(in BotState _state)
	{
		m_CurState?.ExitState();
		m_CurState = _state;
		m_CurState?.EnterState();
	}
}
