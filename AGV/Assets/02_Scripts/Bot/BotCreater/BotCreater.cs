using System.Collections.Generic;
using System.Linq;
using Delegates;

public class BotCreater : Creater<Bot>
{
	private readonly List<BotCreaterWindow> m_WindowList = new List<BotCreaterWindow>();
	private BotRouteSetter m_RouteSetter = null;
	private BotLocationSetter m_LocationSetter = null;
	private BotSpawnPosSetter m_SpawnPosSetter = null;
	private BotPrioritySetter m_PrioritySetter = null;

	private List<Flag> m_PathList = new List<Flag>();
	private Flag m_LoadFlag = null;
	private Flag m_UnloadFlag = null;
	private Flag m_SpawnFlag = null;

	private int m_Priority = 0;

	private void Awake()
	{
		m_RouteSetter = GetComponentInChildren<BotRouteSetter>();
		m_LocationSetter = GetComponentInChildren<BotLocationSetter>();
		m_SpawnPosSetter = GetComponentInChildren<BotSpawnPosSetter>();
		m_PrioritySetter = GetComponentInChildren<BotPrioritySetter>();

		m_WindowList.Add(m_RouteSetter);
		m_WindowList.Add(m_LocationSetter);
		m_WindowList.Add(m_SpawnPosSetter);
		m_WindowList.Add(m_PrioritySetter);
	}

	protected override void init()
	{
		foreach (var setter in m_WindowList)
		{
			setter.Init();
		}

		m_SpawnPosSetter.SetPreview(m_Manager);

		clear();
	}

	protected override void clear()
	{
		m_PathList.Clear();
		m_SpawnFlag = null;
		m_LoadFlag = null;
		m_UnloadFlag = null;

		foreach (var setter in m_WindowList)
		{
			setter.SetActive(false);
		}
		m_RouteSetter.SetActive(true);
	}

	protected override void setDelegate(in CreaterDelegates<Bot> _delegates)
	{
		var routeSetterDelegates = new BotRouteSetterDelegates(setFlagsOnClickEventDelegate, applyRoute_Callback);
		m_RouteSetter.SetDelegate(routeSetterDelegates);

		var lnuDelegates = new BotLocationSetterDelegates(setFlagsOnClickEventDelegate, applyLoadAndUnloadPlace_Callback, checkFlagInPath);
		m_LocationSetter.SetDelegate(lnuDelegates);

		var spawnPosSetterDelegates = new BotSpawnPosSetterDelegates(setFlagsOnClickEventDelegate, applySpawnFlag_Callback, setFlagsOnMouseEnterEventDelegate, setFlagsOnMouseExitEventDelegate);
		m_SpawnPosSetter.SetDelegate(spawnPosSetterDelegates);

		var prioritySetterDelegates = new BotPrioritySetterDelegates(setFlagsOnClickEventDelegate, applyCreateBot_Callback, cancel_Callback);
		m_PrioritySetter.SetDelegate(prioritySetterDelegates);
	}

	private void applyRoute_Callback(in List<Flag> _routeFlagList)
	{
		m_PathList = _routeFlagList;
		m_RouteSetter.SetActive(false);
		m_LocationSetter.SetActive(true);
	}

	private void applyLoadAndUnloadPlace_Callback(in Flag _loadFlag, in Flag _unloadFlag)
	{
		m_LoadFlag = _loadFlag;
		m_UnloadFlag = _unloadFlag;

		m_LocationSetter.SetActive(false);
		m_SpawnPosSetter.SetActive(true);
	}

	private bool checkFlagInPath(in Flag _flag)
	{
		return m_PathList.Contains(_flag);
	}

	private void applySpawnFlag_Callback(in Flag _spawnFlag)
	{
		m_SpawnFlag = _spawnFlag;

		m_SpawnPosSetter.SetActive(false);
		m_PrioritySetter.SetActive(true);
	}

	private void applyCreateBot_Callback(in int _priority)
	{
		m_Priority = _priority;

		BotInfo info = new BotInfo(m_PathList.ToList(), m_LoadFlag, m_UnloadFlag, m_SpawnFlag, m_Priority);
		create(m_SpawnFlag.Pos, info);
	}
}

public class BotCreaterDelegates : CreaterDelegates<Bot>
{
	public BotCreaterDelegates(Delegate<Bot> _createdCallback, Delegate _onCreateModeCallback, Delegate _offCreateModeCallback, Delegate<Delegate<Flag>> _setFlagsOnClickEventCallback, Delegate<Delegate<Flag>> _setFlagsOnMouseEnterEventCallback, Delegate<Delegate<Flag>> _setFlagsOnMouseExitEventDelegate) : base(_createdCallback, _onCreateModeCallback, _offCreateModeCallback, _setFlagsOnClickEventCallback, _setFlagsOnMouseEnterEventCallback, _setFlagsOnMouseExitEventDelegate) { }
}