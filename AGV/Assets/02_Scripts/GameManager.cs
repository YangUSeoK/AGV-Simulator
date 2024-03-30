using UnityEngine;

public enum EGameMode
{
	Edit = 0,
	CreateBot,
	CreateFlag,
	CreateLocation,
	Play,

	Length,
}

public class GameManager : MonoBehaviour
{
	private static EGameMode m_GameMode = EGameMode.Edit;
	public static EGameMode GameMode
	{
		get { return m_GameMode; }
		set { m_GameMode = value; }
	}

	private BotManager m_BotManager = null;
	private UIManager m_UIManager = null;
	private FlagManager m_FlagManager = null;

	private void Awake()
	{
		m_BotManager = GetComponentInChildren<BotManager>();
		m_UIManager = GetComponentInChildren<UIManager>();
		m_FlagManager = GetComponentInChildren<FlagManager>();

		var botManagerDelegates = new BotManagerDelegates(createBotCallback);
		m_BotManager.SetDelegate(botManagerDelegates);
		m_UIManager.SetDelegate(startSimulation, finishSimulation, instBotCreaterCallback);

		m_BotManager.Init();
		m_UIManager.Init();
		m_FlagManager.Init();
	}

	private void startSimulation()
	{
		m_GameMode = EGameMode.Play;

		m_BotManager.StartSimulation();
		m_UIManager.StartSimulation();
	}

	private void finishSimulation()
	{
		m_GameMode = EGameMode.Edit;

		m_BotManager.FinishSimulation();
		m_FlagManager.FinishSimulation();
	}

	// From UIManager
	private void instBotCreaterCallback(in BotCreater _botCreater)
	{
		var botCreaterDelegates = new BotCreaterDelegates(null, startCreateBotMode, finishCreateBotMode,
									m_FlagManager.SetOnClickEvent, m_FlagManager.SetOnMouseEnterEvent,
									m_FlagManager.SetOnMouseExitEvent);
		m_BotManager.SetBotCreater(_botCreater, botCreaterDelegates); 
	}




	// From FlagManager




	// From BotManager
	private void createBotCallback(in Bot _createdBot)
	{
		// 봇이 생성되었습니다 메세지 출력
	}




	// events
	private void startCreateBotMode()
	{
		m_GameMode = EGameMode.CreateBot;
	}

	private void finishCreateBotMode()
	{
		m_GameMode = EGameMode.Edit;
		m_FlagManager.SetOnClickEvent(null);
	}
}
