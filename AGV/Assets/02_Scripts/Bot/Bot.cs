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

public class Bot : Item<Bot>
{
	[SerializeField] List<Material> m_StateMaterialList = new List<Material>();

	private BotStateMachine m_SM = null;
	private NavMeshAgent m_Agent = null;

	public float CurSpeed => m_Agent.speed;
	public Vector3 Dest => m_Agent.destination;

	private List<Flag> m_Path = null;
	public List<Flag> Path => m_Path;

	private Flag m_LoadFlag = null;
	public Flag LoadFlag => m_LoadFlag;

	private Flag m_UnloadFlag = null;
	public Flag UnloadFlag => m_UnloadFlag;

	private Flag m_SpawnFlag = null;
	public Flag SpawnFlag => m_SpawnFlag;

	private int m_Priority = 0;
	public int Priority => m_Priority;

	public bool IsArrive => Vector3.Distance(Dest, Pos) <= CurSpeed * Time.deltaTime;

	// inComingBot이 없거나, 자기 자신이면 갈 수 있음
	public bool CanGoNext => !m_Path[nextIdx].IsExistIncomingtBot
						   || m_Path[nextIdx].IncomingBot == this;

	private int m_CurIdx = 0;
	public int CurIdx => m_CurIdx;
	private int nextIdx => (m_CurIdx + 1) % m_Path.Count;

	protected override void Awake()
	{
		base.Awake();
		m_Agent = GetComponentInChildren<NavMeshAgent>();
	}

	private void Update()
	{
		if (GameManager.GameMode == EGameMode.Play)
		{
			m_SM.Update();
		}
	}

	public override void StartSimulation()
	{
		m_CurIdx = m_Path.IndexOf(m_SpawnFlag);
		m_Path[nextIdx].EnqueueIncomingBot(this);

		m_SM.StartSimulation();
	}

	public override void FinishSimulation()
	{
		clear();
	}

	public void ArriveAtDestFlag()
	{
		if (m_Path[m_CurIdx] == m_LoadFlag)
		{
			m_SM.SetState(m_SM.LoadWait);
		}
		else if (m_Path[m_CurIdx] == m_UnloadFlag)
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

	public override void Init(in ItemInfo<Bot> _botContainer)
	{
		BotInfo container = _botContainer as BotInfo;

		m_Path = container.Path;
		m_LoadFlag = container.LoadFlag;
		m_UnloadFlag = container.UnloadFlag;
		m_SpawnFlag = container.SpawnFlag;
		m_Priority = container.Priority;

		m_SM = new BotStateMachine(this);
	}

	protected override void onClick()
	{
	}

	protected override void onMouseEnter()
	{
	}

	protected override void onMouseExit()
	{
	}

	protected override void clear()
	{
		m_Agent.ResetPath();
		SetNavMeshStop(true);
		m_Agent.velocity = Vector3.zero;
		SetMaterialByStateEnum(EBotState.Move);

		this.transform.position = m_SpawnFlag.Pos;
		this.transform.rotation = Quaternion.Euler(Vector3.zero);
	}

	private void setMoveState()
	{
		m_Path[nextIdx].EnqueueIncomingBot(this);
		m_SM.SetState(m_SM.MoveWait);
	}
}

public class BotInfo : ItemInfo<Bot>
{
	public BotInfo(in List<Flag> _path, in Flag _loadFlag, in Flag _unloadFlag, in Flag _spawnFlag, in int _priority)
	{
		Path = _path;
		LoadFlag = _loadFlag;
		UnloadFlag = _unloadFlag;
		SpawnFlag = _spawnFlag;
		Priority = _priority;
	}

	public List<Flag> Path { get;}
	public Flag LoadFlag { get;}
	public Flag UnloadFlag { get;}
	public Flag SpawnFlag { get;}
	public int Priority { get;}
}
