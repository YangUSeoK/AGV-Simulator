using UnityEngine;
using static LocationManager;

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

	public EGameMode CurMode;

	private BotManager m_BotManager = null;
	private UIManager m_UIManager = null;
	private FlagManager m_FlagManager = null;
	private LocationManager m_LocationManager = null;

	private void Awake()
	{
		m_BotManager = GetComponentInChildren<BotManager>();
		m_UIManager = GetComponentInChildren<UIManager>();
		m_FlagManager = GetComponentInChildren<FlagManager>();
		m_LocationManager = GetComponentInChildren<LocationManager>();


		var uiManagerDelegates = new UIManagerDelegates(startSimulation, finishSimulation);
		m_UIManager.SetDelegate(uiManagerDelegates);
		m_UIManager.Init();


		var botManagerDelegates = new BotManagerDelegates(createrCreateCallback, createItemCallback);
		m_BotManager.SetDelegate(botManagerDelegates);
		var botCreaterDelegates = new BotCreaterDelegates(null, startCreateBotMode, finishCreateBotMode,
									m_FlagManager.SetOnClickEvent, m_FlagManager.SetOnMouseEnterEvent,
									m_FlagManager.SetOnMouseExitEvent);
		m_BotManager.Init(botCreaterDelegates);
		

		var flagCreaterDelegates = new FlagCreaterDelegates();
		m_FlagManager.Init(flagCreaterDelegates);

		var locationManagerDelegates = new LocationManagerDelegates(createrCreateCallback, createItemCallback);
		m_LocationManager.SetDelegate(locationManagerDelegates);
		var locationCreaterDelegates = new LocationCreaterDelegates(m_FlagManager.SetOnClickEvent);
		m_LocationManager.Init(locationCreaterDelegates);
	}

	// 맨 처음 시작 상태 설정
	private void Start()
	{
		m_GameMode = EGameMode.Edit;

		m_BotManager.FinishSimulation();
		m_FlagManager.FinishSimulation();
	}

	private void Update()
	{
		CurMode = m_GameMode;
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

	private void createrCreateCallback(in Creater _creater)
	{
		m_UIManager.SetCreater(_creater);
	}

	private void createItemCallback(in Item _createdItem)
	{

	}

	// From UIManager

	// From FlagManager




	// From BotManager


	// From LocationManager

	// events
	private void startCreateBotMode(in EGameMode _gameMode)
	{
		m_GameMode = _gameMode;
	}

	private void finishCreateBotMode()
	{
		m_GameMode = EGameMode.Edit;
		
		// TODO.. Edit Mode의 Click 이벤트 넣어줘야 함
		m_FlagManager.SetOnClickEvent(null);
	}
}
