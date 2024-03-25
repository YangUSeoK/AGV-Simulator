public class BotStateMachine
{
	public BotStateMachine(in Bot _bot)
	{
		m_Bot = _bot;

		m_MoveState = new BotState_Move(m_Bot, this);
		m_MoveWaitState = new BotState_MoveWait(m_Bot, this);
		m_LoadState = new BotState_Load(m_Bot, this);
		m_UnloadState = new BotState_Unload(m_Bot, this);
		m_WaitState = new BotState_Wait(m_Bot, this);
	}

	private readonly Bot m_Bot = null;

	private readonly BotState_Move m_MoveState = null;
	public BotState_Move MoveState => m_MoveState;

	private readonly BotState_MoveWait m_MoveWaitState = null;
	public BotState_MoveWait MoveWaitState => m_MoveWaitState;

	private readonly BotState_Load m_LoadState = null;
	public BotState_Load LoadState => m_LoadState;

	private readonly BotState_Unload m_UnloadState = null;
	public BotState_Unload UnloadState => m_UnloadState;

	private readonly BotState_Wait m_WaitState = null;
	public BotState_Wait WaitState => m_WaitState;

	private BotState m_CurState = null;


	public void Update()
	{
		if (m_CurState is null) return;

		m_CurState.CheckState();
		m_CurState.UpdateState();
	}

	public void SetState(in BotState _state)
	{
		m_CurState?.ExitState();
		m_CurState = _state;
		m_CurState?.EnterState();
	}

	public void StartSimulation()
	{
		SetState(m_MoveState);
		(m_CurState as BotState_Move).StartSimulation();
	}

	public void StopSimulation()
	{
		SetState(null);
	}
}
