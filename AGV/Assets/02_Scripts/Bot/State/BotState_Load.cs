using UnityEngine;

public class BotState_Load : BotState
{
	public BotState_Load(in Bot _bot, in BotStateMachine _matchine) : base(_bot, _matchine) { }

	private readonly float loadTime = 1.0f;
	private float m_Timer = 0;

	public override void EnterState()
	{
		m_Timer = 0f;
		m_Bot.SetMaterialByStateEnum(EBotState.Load);
	}


	// 처음 왔는데 적재물이 없으면 기다림
	// 로드를 시작한 후 적재물이 없어지면 or 짐이 다 찼다면 Finish

	public override void CheckState()
	{
		// 적재물이 없으면 기다림
		if (!m_Bot.CanLoad) return;

		m_Timer += Time.deltaTime;
		if(m_Timer >= loadTime)
		{
			m_Bot.Load();
		}

		m_Bot.FinishLoadUnload();
	}

	public override void UpdateState()
	{
		
	}

	public override void ExitState()
	{
		m_Timer = 0f;
	}
}
