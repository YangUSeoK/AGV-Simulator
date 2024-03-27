using UnityEngine;

public enum EGameMode
{
	Edit = 0,
	CreateBot,
	CreateFlag,
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

		m_BotManager.SetDelegate(createBotCallback);
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
	}

	// From UIManager
	private void instBotCreaterCallback(in BotCreater _botCreater)
	{
		m_BotManager.SetBotCreater(_botCreater, createBotCallback, startCreateBotMode, finishCreateBotMode,
									m_FlagManager.SetFlagsOnClickEvent, m_FlagManager.SetPlagsOnMouseEnterEvent); ;
	}




	// From FlagManager




	// From BotManager
	private void createBotCallback()
	{

	}




	// events
	private void startCreateBotMode()
	{
		m_GameMode = EGameMode.CreateBot;
	}

	private void finishCreateBotMode()
	{
		m_GameMode = EGameMode.Edit;
		m_FlagManager.SetFlagsOnClickEvent(null);
	}
}
