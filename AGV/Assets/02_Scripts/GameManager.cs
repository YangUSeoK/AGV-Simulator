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

		var botManagerDelegates = new BotManagerDelegates(createrCreateCallback, createBotCallback);
		m_BotManager.SetDelegate(botManagerDelegates);

		var uiManagerDelegates = new UIManagerDelegates(startSimulation, finishSimulation);
		m_UIManager.SetDelegate(uiManagerDelegates);

		var botCreaterDelegates = new BotCreaterDelegates(null, startCreateBotMode, finishCreateBotMode,
									m_FlagManager.SetOnClickEvent, m_FlagManager.SetOnMouseEnterEvent,
									m_FlagManager.SetOnMouseExitEvent);
		m_BotManager.Init(botCreaterDelegates);
		m_UIManager.Init();

		var flagCreaterDelegates = new FlagCreaterDelegates();
		m_FlagManager.Init(flagCreaterDelegates);
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

	private void createrCreateCallback(in Creater _creater)
	{
		m_UIManager.SetCreater(_creater);
	}


}
