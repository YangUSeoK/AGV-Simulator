using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EBotState
{
	Move = 0,
	MoveWait,
	Load,
	Unload,
	LoadWait,

	Length,
}

public class Bot : MonoBehaviour
{
	[SerializeField] List<Material> m_StateMaterialList = new List<Material>();
	private Renderer m_Renderer = null;

	private BotStateMachine m_SM = null;
	private NavMeshAgent m_Agent = null;

	public float CurSpeed => m_Agent.speed;
	public Vector3 Dest => m_Agent.destination;
	public Vector3 Pos => this.transform.position;

	private List<Flag> m_Path = null;
	public List<Flag> Path => m_Path;

	private Flag m_LoadPlag = null;
	public Flag LoadPlag => m_LoadPlag;

	private Flag m_UnloadPlag = null;
	public Flag UnloadPlag => m_UnloadPlag;

	private Flag m_SpawnPlag = null;
	public Flag SpawnPlag => m_SpawnPlag;

	private int m_Priority = 0;
	public int Priority => m_Priority;

	public bool IsArrive => Vector3.Distance(Dest, Pos) <= CurSpeed * Time.deltaTime;

	// inComingBot이 없거나, 자기 자신이면 갈 수 있음
	public bool CanGoNext => !m_Path[nextIdx].IsExistIncomingtBot
						   || m_Path[nextIdx].IncomingBot == this;

	private int m_CurIdx = 0;
	public int CurIdx => m_CurIdx;
	private int nextIdx => (m_CurIdx + 1) % m_Path.Count;

	private void Awake()
	{
		m_Agent = GetComponentInChildren<NavMeshAgent>();
		m_Renderer = GetComponentInChildren<Renderer>();
	}

	private void Update()
	{
		m_SM.Update();
	}

	public void StartSimulation()
	{
		m_CurIdx = m_Path.IndexOf(m_SpawnPlag);
		m_Path[nextIdx].EnqueueIncomingBot(this);

		m_SM.StartSimulation();
	}

	public void ArriveAtDestFlag()
	{
		if (m_Path[m_CurIdx] == m_LoadPlag)
		{
			m_SM.SetState(m_SM.LoadWait);
		}
		else if (m_Path[m_CurIdx] == m_UnloadPlag)
		{
			m_SM.SetState(m_SM.Unload);
		}
		else
		{
			setMoveState();
		}
	}

	public void FinishLoadUnload()
	{
		setMoveState();
	}

	public void GoNextFlag()
	{
		m_Agent.SetDestination(m_Path[nextIdx].Pos);
		m_CurIdx = nextIdx;

		m_SM.SetState(m_SM.Move);
	}

	public void SetMaterialByStateEnum(EBotState _state)
	{
		m_Renderer.material = m_StateMaterialList[(int)_state];
	}

	public void SetNavMeshStop(bool _isStop)
	{
		m_Agent.isStopped = _isStop;
	}

	public void Init(in List<Flag> _path, in Flag _loadPlag, in Flag _unloadPlag, in Flag _spawnPlag, in int _priority)
	{
		m_Path = _path;
		m_LoadPlag = _loadPlag;
		m_UnloadPlag = _unloadPlag;
		m_SpawnPlag = _spawnPlag;
		m_Priority = _priority;

		m_SM = new BotStateMachine(this);
	}

	private void setMoveState()
	{
		m_Path[nextIdx].EnqueueIncomingBot(this);
		m_SM.SetState(m_SM.MoveWait);
	}

	
}
