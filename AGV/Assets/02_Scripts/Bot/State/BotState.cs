using Delegates;

public abstract class BotState
{
	public BotState(in Bot _bot, in BotStateMachine _machine)
	{
		m_Bot = _bot;
		m_Machine = _machine;
	}

	protected Bot m_Bot = null;
	protected BotStateMachine m_Machine = null;
	
	public abstract void EnterState();
	public abstract void CheckState();
	public abstract void UpdateState();
	public abstract void ExitState();
}
