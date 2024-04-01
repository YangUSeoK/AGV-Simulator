public class BotState_LoadWait : BotState
{
	public BotState_LoadWait(in Bot _bot, in BotStateMachine _matchine) : base(_bot, _matchine) { }

	public override void EnterState()
	{
		m_Bot.SetMaterialByStateEnum(EBotState.LoadWait);
	}

	public override void CheckState()
	{
		// 적재물이 있는지 확인 후 넘어가기
		m_SM.SetState(m_SM.Load);
	}

	public override void UpdateState()
	{
	}

	public override void ExitState()
	{
	}
}