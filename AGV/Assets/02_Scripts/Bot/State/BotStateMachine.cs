public class BotStateMachine
{
	public BotStateMachine(in Bot _bot)
	{
		m_Bot = _bot;

		m_Move = new BotState_Move(m_Bot, this);
		m_MoveWait = new BotState_MoveWait(m_Bot, this);
		m_Load = new BotState_Load(m_Bot, this);
		m_Unload = new BotState_Unload(m_Bot, this);
		m_LoadWait = new BotState_LoadWait(m_Bot, this);
	}

	private readonly Bot m_Bot = null;

	private readonly BotState_Move m_Move = null;
	public BotState_Move Move => m_Move;

	private readonly BotState_MoveWait m_MoveWait = null;
	public BotState_MoveWait MoveWait => m_MoveWait;

	private readonly BotState_Load m_Load = null;
	public BotState_Load Load => m_Load;

	private readonly BotState_Unload m_Unload = null;
	public BotState_Unload Unload => m_Unload;

	private readonly BotState_LoadWait m_LoadWait = null;
	public BotState_LoadWait LoadWait => m_LoadWait;

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
		SetState(m_Move);
		(m_CurState as BotState_Move).StartSimulation();
	}

	public void StopSimulation()
	{
		SetState(null);
	}
}
