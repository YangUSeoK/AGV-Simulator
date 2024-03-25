using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Delegates;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
	private Delegate startSimulationDelegate = null;
	private Delegate<BotCreater> instBotCreaterDelegate = null;

	private static UIManager m_Instance;
	public static UIManager Instance => m_Instance;

	[SerializeField] private GameObject BotCreaterPrefab = null;

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
	}

	public void StartSimulation()
	{
		offAllUI();
	}

	public void OnClick_StartSimulation()
	{
		startSimulationDelegate?.Invoke();
	}

	public void Init()
	{
		m_BotCreater = Instantiate(BotCreaterPrefab, this.transform).GetComponent<BotCreater>();
		instBotCreaterDelegate?.Invoke(m_BotCreater);

		m_BotCreater.SetActive(false);

		setEvent();
	}

	public void SetDelegate(Delegate _startSimulationCallback, Delegate<BotCreater> _instBotCreaterCallback)
	{
		startSimulationDelegate = _startSimulationCallback;
		instBotCreaterDelegate = _instBotCreaterCallback;
	}

	private void setEvent()
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
