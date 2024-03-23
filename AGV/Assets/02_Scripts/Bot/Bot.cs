using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
	private int m_Priority = 0;
	public int Priority => m_Priority;

	#region State
	private BotStateMachine m_SM = null;

	#endregion





	private BotMovement m_Move = null;

	private Plag m_SpawnPlag = null;
	private List<Plag> m_Path = null;
	private Plag m_LoadPlag = null;
	private Plag m_UnloadPlag = null;


	private void Awake()
	{
		m_SM = GetComponentInChildren<BotStateMachine>();
	}


	public void StartSimulation()
	{
		m_Move.StartSimulation();
	}




	public void SetMember(in Plag _spawnPlag, in Plag _loadPlag, in Plag _unloadPlag, in int _priority, in List<Plag> _path)
	{
		m_SpawnPlag = _spawnPlag;
		m_LoadPlag = _loadPlag;
		m_UnloadPlag = _unloadPlag;
		m_Priority = _priority;
		m_Path = _path;

		int startIdx = m_Path.IndexOf(m_SpawnPlag);
		m_Move.SetMember(m_Path, startIdx);
	}
}
