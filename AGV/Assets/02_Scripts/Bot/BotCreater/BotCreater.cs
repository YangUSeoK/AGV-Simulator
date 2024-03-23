using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BotCreater : MonoBehaviour
{
	private Action<Bot> createBotCallback = null;

	[SerializeField] private GameObject m_BotPrefab = null;
	
	private BotCreateSetter[] m_SetterList = null;
	private RouteSetter m_RouteSetter = null;
	private LoadAndUnloadPlaceSetter m_LoadAndUnloadPlaceSetter = null;
	private BotSpawnPosSetter m_SpawnPosSetter = null;
	private BotPrioritySetter m_PrioritySetter = null;

	private BotManager m_Manager = null;
	private Plag[] m_Plags = null;
	
	private List<Plag> m_PathList = new List<Plag>();
	private Plag m_LoadPlag = null;
	private Plag m_UnloadPlag = null;
	private int m_Priority = 0;

	private Plag m_SpawnPlag = null;

	private void Awake()
	{
		m_SetterList = GetComponentsInChildren<BotCreateSetter>();
		

		m_RouteSetter = GetComponentInChildren<RouteSetter>();
		m_LoadAndUnloadPlaceSetter = GetComponentInChildren<LoadAndUnloadPlaceSetter>();
		m_SpawnPosSetter = GetComponentInChildren<BotSpawnPosSetter>();
		m_PrioritySetter = GetComponentInChildren<BotPrioritySetter>();

		foreach(var setter in m_SetterList)
		{
			setter.SetActive(false);
		}
		m_RouteSetter.SetActive(true);
	}

	private void OnEnable()
	{
		Init();
		if (m_Plags == null) return;

		foreach (var plag in m_Plags)
		{
			plag.IsAddMode = true;
		}
	}

	private void OnDisable()
	{
		foreach (var plag in m_Plags)
		{
			plag.IsAddMode = false;
			plag.SetOnClickEvent(null);
		}
	}

	private void ApplyRoute_Callback(List<Plag> _routePlagList)
	{
		m_PathList = _routePlagList;
		m_RouteSetter.SetActive(false);
		m_LoadAndUnloadPlaceSetter.SetActive(true);
	}

	private void ApplyLoadAndUnloadPlace_Callback(Plag _loadPlag, Plag _unloadPlag)
	{
		m_LoadPlag = _loadPlag;
		m_UnloadPlag = _unloadPlag;

		m_LoadAndUnloadPlaceSetter.SetActive(false);
		m_SpawnPosSetter.SetActive(true);
	}

	private void ApplySpawnPlag_Callback(Plag _spawnPlag)
	{
		m_SpawnPlag = _spawnPlag;

		m_SpawnPosSetter.SetActive(false);
		m_PrioritySetter.SetActive(true);
	}

	private void ApplyCreateBot_Callback(int _priority)
	{
		m_Priority = _priority;
		CreateBot();
	}

	private void Cancel_Callback(int _num)
	{
		this.gameObject.SetActive(false);
	}

	private void CreateBot()
	{
		Bot bot = Instantiate(m_BotPrefab, m_SpawnPlag.Pos, Quaternion.identity, m_Manager.transform).GetComponent<Bot>();

		bot.SetMember(m_SpawnPlag, m_LoadPlag, m_UnloadPlag, m_Priority, m_PathList.ToList());

		createBotCallback?.Invoke(bot);
		Init();
	}

	private void SetPlagsOnClickEvent(Action<Plag> _event)
	{
		foreach (var plag in m_Plags)
		{
			plag.SetOnClickEvent(_event);
		}
	}

	private void SetPlagsOnMouseEnterEvent(Action<Plag> _event)
	{
		foreach (var plag in m_Plags)
		{
			plag.SetOnMouseEnterEvent(_event);
		}
	}

	public void SetActive(bool _isActive)
	{
		this.gameObject.SetActive(_isActive);
	}

	public void SetMember(BotManager _manager, Plag[] _plags)
	{
		m_Manager = _manager;
		m_Plags = _plags;
	}
	public void SetCallback(Action<Bot> _createBotCallback)
	{
		createBotCallback = _createBotCallback;

		foreach (var setter in m_SetterList)
		{
			setter.SetModeCallback(SetPlagsOnClickEvent);
		}

		m_RouteSetter.SetCallback(ApplyRoute_Callback);
		m_LoadAndUnloadPlaceSetter.SetCallback(ApplyLoadAndUnloadPlace_Callback);
		m_SpawnPosSetter.SetCallback(ApplySpawnPlag_Callback, SetPlagsOnMouseEnterEvent);
		m_PrioritySetter.SetCallback(ApplyCreateBot_Callback,Cancel_Callback);
	}

	public void Init()
	{
		m_PathList.Clear();
		m_SpawnPlag = null;
		m_LoadPlag = null;
		m_UnloadPlag = null;
	}
}
