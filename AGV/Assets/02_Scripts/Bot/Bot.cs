using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : MonoBehaviour
{
	private BotStateMachine m_SM = null;
	private NavMeshAgent m_Agent = null;

	public float CurSpeed => m_Agent.speed;
	public Vector3 Dest => m_Agent.destination;
	public Vector3 Pos => this.transform.position;

	private List<Plag> m_Path = null;
	public List<Plag> Path => m_Path;

	private Plag m_LoadPlag = null;
	public Plag LoadPlag => m_LoadPlag;

	private Plag m_UnloadPlag = null;
	public Plag UnloadPlag => m_UnloadPlag;

	private Plag m_SpawnPlag = null;
	public Plag SpawnPlag => m_SpawnPlag;

	private int m_Priority = 0;
	public int Priority => m_Priority;

	private int m_StartIdx = 0;


	private void Awake()
	{
		m_Agent = GetComponentInChildren<NavMeshAgent>();
	}

	private void Update()
	{
		m_SM.Update();
	}

	public void StartSimulation()
	{
		m_SM.StartSimulation(m_StartIdx);
	}

	// NavMesh Controll
	public void SetDestination(in Vector3 _destVec)
	{
		m_Agent.SetDestination(_destVec);
	}
	public void SetNavMeshStop(bool _isStop)
	{
		m_Agent.isStopped = _isStop;
	}

	public void SetMember(in List<Plag> _path, in Plag _loadPlag, in Plag _unloadPlag, in Plag _spawnPlag, in int _priority)
	{
		m_Path = _path;
		m_LoadPlag = _loadPlag;
		m_UnloadPlag = _unloadPlag;
		m_SpawnPlag = _spawnPlag;
		m_Priority = _priority;

		m_StartIdx = m_Path.IndexOf(m_SpawnPlag);

		m_SM = new BotStateMachine(this);
	}
}
