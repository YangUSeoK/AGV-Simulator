using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	private Delegates.VoidVoid startSimulationDelegate = null;

	private static UIManager m_Instance;
	public static UIManager Instance => m_Instance;

	[SerializeField] private Button m_StartButton = null;
	[SerializeField] private Button m_CreateBotButton = null;

	private readonly List<GameObject> m_UiList = new List<GameObject>();

	private BotCreater m_BotCreater = null;
	public BotCreater BotCreater
	{
		set
		{
			m_BotCreater = value;
			m_UiList.Add(m_BotCreater.gameObject);
		}
	}

	private void Awake()
	{
		m_Instance = this;
		SetEvent();
	}

	public void StartSimulation()
	{
		offAllUI();
	}

	public void OnClick_StartSimulation()
	{
		startSimulationDelegate?.Invoke();
	}

	public void SetDelegate(Delegates.VoidVoid _startSimulationCallback)
	{
		startSimulationDelegate = _startSimulationCallback;
	}

	private void SetEvent()
	{
		m_CreateBotButton.onClick.AddListener(() => m_BotCreater.SetActive(true));
		m_StartButton.onClick.AddListener(() =>
		{
			offAllUI();
			startSimulationDelegate?.Invoke();
		});
	}

	private void offAllUI()
	{
		foreach (var ui in m_UiList)
		{
			ui.SetActive(false);
		}
	}
}
