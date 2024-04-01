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


	// ó�� �Դµ� ���繰�� ������ ��ٸ�
	// �ε带 ������ �� ���繰�� �������� or ���� �� á�ٸ� Finish

	public override void CheckState()
	{
		// ���繰�� ������ ��ٸ�
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
