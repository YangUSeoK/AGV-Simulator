using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Delegates;

public class BotCreater : MonoBehaviour
{
	private Delegate<Bot> createBotDelegate = null;
	private Delegate onCreateModeDelegate = null;
	private Delegate offCreateModeDelegate = null;
	private Delegate<Delegate<Flag>> setFlagsOnClickEventDelegate = null;
	private Delegate<Delegate<Flag>> setFlagsOnMouseEnterEventDelegate = null;


	[SerializeField] private GameObject m_BotPrefab = null;

	private BotCreateSetter[] m_SetterList = null;
	private RouteSetter m_RouteSetter = null;
	private LoadAndUnloadPlaceSetter m_LoadAndUnloadPlaceSetter = null;
	private BotSpawnPosSetter m_SpawnPosSetter = null;
	private BotPrioritySetter m_PrioritySetter = null;

	private BotManager m_Manager = null;

	private List<Flag> m_PathList = new List<Flag>();
	private Flag m_LoadPlag = null;
	private Flag m_UnloadPlag = null;
	private int m_Priority = 0;

	private Flag m_SpawnFlag = null;

	private void Awake()
	{
		m_SetterList = GetComponentsInChildren<BotCreateSetter>();


		m_RouteSetter = GetComponentInChildren<RouteSetter>();
		m_LoadAndUnloadPlaceSetter = GetComponentInChildren<LoadAndUnloadPlaceSetter>();
		m_SpawnPosSetter = GetComponentInChildren<BotSpawnPosSetter>();
		m_PrioritySetter = GetComponentInChildren<BotPrioritySetter>();
	}

	private void OnEnable()
	{
		// 켤 때 마다 플래그의 모드를 CreateBot 으로 설정
		onCreateModeDelegate?.Invoke();
	}

	private void OnDisable()
	{
		// 끌 때 마다 플래그의 모드를 CreateBot 으로 설정하고, SetOnclickEvent = null;
		offCreateModeDelegate?.Invoke();
		clear();
	}

	private void Update()
	{
		if(GameManager.GameMode != EGameMode.CreateBot)
		{
			this.gameObject.SetActive(false);
		}
	}

	public void SetActive(in bool _isActive)
	{
		this.gameObject.SetActive(_isActive);
	}

	public void Init(in BotManager _manager)
	{
		m_Manager = _manager;

		foreach (var setter in m_SetterList)
		{
			setter.Init();
		}

		clear();
	}

	public void SetCallback(Delegate<Bot> _createBotCallback, 
							Delegate _onCreateModeCallback, Delegate _offCreateModeCallback,
							Delegate<Delegate<Flag>> _setFlagsOnClickEventCallback, Delegate<Delegate<Flag>> _setFlagsOnMouseEnterEventCallback)
	{
		createBotDelegate = _createBotCallback;
		onCreateModeDelegate = _onCreateModeCallback;
		offCreateModeDelegate = _offCreateModeCallback;
		setFlagsOnClickEventDelegate = _setFlagsOnClickEventCallback;
		setFlagsOnMouseEnterEventDelegate = _setFlagsOnMouseEnterEventCallback;



		foreach (var setter in m_SetterList)
		{
			setter.SetModeCallback(setFlagsOnClickEventDelegate);
		}

		m_RouteSetter.SetCallback(applyRoute_Callback);
		m_LoadAndUnloadPlaceSetter.SetCallback(applyLoadAndUnloadPlace_Callback, checkFlagInPath);
		m_SpawnPosSetter.SetCallback(applySpawnPlag_Callback, setFlagsOnMouseEnterEventDelegate);
		m_PrioritySetter.SetCallback(applyCreateBot_Callback, cancel_Callback);
	}

	private void applyRoute_Callback(in List<Flag> _routePlagList)
	{
		m_PathList = _routePlagList;
		m_RouteSetter.SetActive(false);
		m_LoadAndUnloadPlaceSetter.SetActive(true);
	}

	private void applyLoadAndUnloadPlace_Callback(in Flag _loadPlag, in Flag _unloadFlag)
	{
		m_LoadPlag = _loadPlag;
		m_UnloadPlag = _unloadFlag;

		m_LoadAndUnloadPlaceSetter.SetActive(false);
		m_SpawnPosSetter.SetActive(true);
	}

	private bool checkFlagInPath(in Flag _flag)
	{
		return m_PathList.Contains(_flag);
	}

	private void applySpawnPlag_Callback(in Flag _spawnFlag)
	{
		m_SpawnFlag = _spawnFlag;

		m_SpawnPosSetter.SetActive(false);
		m_PrioritySetter.SetActive(true);
	}

	private void applyCreateBot_Callback(in int _priority)
	{
		m_Priority = _priority;
		createBot();
	}

	private void cancel_Callback(in int _num)
	{
		this.gameObject.SetActive(false);
	}

	private void createBot()
	{
		Bot bot = Instantiate(m_BotPrefab, m_SpawnFlag.Pos, Quaternion.identity, m_Manager.transform).GetComponent<Bot>();
		bot.Init(m_PathList.ToList(), m_LoadPlag, m_UnloadPlag, m_SpawnFlag, m_Priority);

		createBotDelegate?.Invoke(bot);

		clear();
	}

	private void clear()
	{
		m_PathList.Clear();
		m_SpawnFlag = null;
		m_LoadPlag = null;
		m_UnloadPlag = null;

		foreach (var setter in m_SetterList)
		{
			setter.SetActive(false);
		}
		m_RouteSetter.SetActive(true);
	}
}
