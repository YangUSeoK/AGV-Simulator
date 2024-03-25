using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Delegates;

public class BotCreater : MonoBehaviour
{
	private Delegate<Bot> createBotDelegate = null;


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

		clear();
	}

	private void OnEnable()
	{
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
		clear();
	}

	public void SetActive(in bool _isActive)
	{
		this.gameObject.SetActive(_isActive);
	}

	public void Init(in BotManager _manager, in Plag[] _plags)
	{
		m_Manager = _manager;
		m_Plags = _plags;
	}

	public void SetCallback(Delegate<Bot> _createBotCallback)
	{
		createBotDelegate = _createBotCallback;

		foreach (var setter in m_SetterList)
		{
			setter.SetModeCallback(setPlagsOnClickEvent);
		}

		m_RouteSetter.SetCallback(applyRoute_Callback);
		m_LoadAndUnloadPlaceSetter.SetCallback(applyLoadAndUnloadPlace_Callback);
		m_SpawnPosSetter.SetCallback(applySpawnPlag_Callback, setPlagsOnMouseEnterEvent);
		m_PrioritySetter.SetCallback(applyCreateBot_Callback, cancel_Callback);
	}

	private void applyRoute_Callback(in List<Plag> _routePlagList)
	{
		m_PathList = _routePlagList;
		m_RouteSetter.SetActive(false);
		m_LoadAndUnloadPlaceSetter.SetActive(true);
	}

	private void applyLoadAndUnloadPlace_Callback(in Plag _loadPlag, in Plag _unloadPlag)
	{
		m_LoadPlag = _loadPlag;
		m_UnloadPlag = _unloadPlag;

		m_LoadAndUnloadPlaceSetter.SetActive(false);
		m_SpawnPosSetter.SetActive(true);
	}

	private void applySpawnPlag_Callback(in Plag _spawnPlag)
	{
		m_SpawnPlag = _spawnPlag;

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
		Bot bot = Instantiate(m_BotPrefab, m_SpawnPlag.Pos, Quaternion.identity, m_Manager.transform).GetComponent<Bot>();

		bot.Init(m_PathList.ToList(), m_LoadPlag, m_UnloadPlag, m_SpawnPlag, m_Priority);

		createBotDelegate?.Invoke(bot);
		clear();
		SetActive(false);
	}

	private void setPlagsOnClickEvent(in Delegate<Plag> _event)
	{
		foreach (var plag in m_Plags)
		{
			plag.SetOnClickEvent(_event);
		}
	}

	private void setPlagsOnMouseEnterEvent(in Delegate<Plag> _event)
	{
		foreach (var plag in m_Plags)
		{
			plag.SetOnMouseEnterEvent(_event);
		}
	}

	private void clear()
	{
		m_PathList.Clear();
		m_SpawnPlag = null;
		m_LoadPlag = null;
		m_UnloadPlag = null;

		foreach (var setter in m_SetterList)
		{
			setter.SetActive(false);
		}
		m_RouteSetter.SetActive(true);
	}
}
