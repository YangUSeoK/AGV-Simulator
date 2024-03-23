using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class BotState_Move : BotState
{
	public BotState_Move(Bot _bot) : base(_bot) { }

	private int m_CurIdx = 0;

	private bool isArrive => Vector3.Distance(m_Bot.Dest, m_Bot.Pos) <= m_Bot.CurSpeed * Time.deltaTime;

	public void StartSimulation(in int _startIdx)
	{
		m_Bot.SetDestination(m_Bot.Path[_startIdx].Pos);
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

	public override void ExitState()
	{
		m_Bot.SetNavMeshStop(true);
	}

	public override void UpdateState()
	{
		if (isArrive)
		{
			m_CurIdx = (++m_CurIdx) % m_Bot.Path.Count;
			m_Bot.SetDestination(m_Bot.Path[m_CurIdx].Pos);
		}
	}
}