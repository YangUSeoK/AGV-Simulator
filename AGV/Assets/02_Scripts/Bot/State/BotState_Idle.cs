public class BotState_Idle : BotState
{
	public BotState_Idle(in Bot _bot, in BotStateMachine _machine) : base(_bot, _machine) { }

	public override void EnterState()
	{
		m_Bot.SetNavMeshStop(true);
	}

	public override void CheckState()
	{
	}

	public override void UpdateState()
	{
	}

	public override void ExitState()
	{
	}
}