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

	public bool IsArrive => Vector3.Distance(Dest, Pos) <= CurSpeed * Time.deltaTime;

	public bool CantGoNext => (m_Path[nextIdx].IsNextBot
						   && m_Path[nextIdx].InComingBot != this)
						   || m_Path[nextIdx].IsBotHere;

	public bool CanGoNext => !CantGoNext;

	private int m_StartIdx = 0;
	private int m_CurIdx = 0;
	public int CurIdx => m_CurIdx;
	private int nextIdx => (m_CurIdx + 1) % m_Path.Count;



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
		m_CurIdx = m_Path.IndexOf(m_SpawnPlag);
		m_Path[m_CurIdx].ArriveBot(this);

		setDestination(m_StartIdx);
		m_SM.StartSimulation();
	}

	public void ArriveAtDest()
	{
		m_Path[m_CurIdx].ArriveBot(this);

		int prevIdx = m_CurIdx - 1;
		if (prevIdx < 0)
		{
			prevIdx = m_Path.Count - 1;
		}
		m_Path[prevIdx].CurBot = null;

		setDestination(nextIdx);
		m_CurIdx = nextIdx;
	}

	public void SetNavMeshStop(bool _isStop)
	{
		m_Agent.isStopped = _isStop;
	}

	public void Init(in List<Plag> _path, in Plag _loadPlag, in Plag _unloadPlag, in Plag _spawnPlag, in int _priority)
	{
		m_Path = _path;
		m_LoadPlag = _loadPlag;
		m_UnloadPlag = _unloadPlag;
		m_SpawnPlag = _spawnPlag;
		m_Priority = _priority;

		m_StartIdx = (m_Path.IndexOf(m_SpawnPlag) + 1) % m_Path.Count;
		Debug.Log($"{name}, {m_StartIdx}");

		m_SM = new BotStateMachine(this);
	}


	private void setDestination(in int _nextIdx)
	{
		m_Path[_nextIdx].InComingBot = this;
		m_Agent.SetDestination(m_Path[_nextIdx].Pos);
	}
}
