using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Delegates;

public class UIManager : MonoBehaviour
{
	private Delegate startSimulationDelegate = null;
	private Delegate finishSimulationDelegate = null;
	private Delegate<BotCreater> instBotCreaterDelegate = null;

	[SerializeField] private GameObject BotCreaterPrefab = null;

	[SerializeField] private Toggle m_PlayToggle = null;
	[SerializeField] private Toggle m_CreateBotModeToggle = null;

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

	public void StartSimulation()
	{
		offAllUI();
	}

	public void Init()
	{
		m_BotCreater = Instantiate(BotCreaterPrefab, this.transform).GetComponent<BotCreater>();
		instBotCreaterDelegate?.Invoke(m_BotCreater);

		m_BotCreater.SetActive(false);

		setEvent();
	}

	public void SetDelegate(in Delegate _startSimulationCallback, in Delegate _finishSimulationCallback,
							in Delegate<BotCreater> _instBotCreaterCallback)
	{
		startSimulationDelegate = _startSimulationCallback;
		finishSimulationDelegate = _finishSimulationCallback;
		instBotCreaterDelegate = _instBotCreaterCallback;
	}

	private void setEvent()
	{
		m_PlayToggle.onValueChanged.AddListener((_isOn) =>
		{
			if(_isOn)
			{
				if(GameManager.GameMode != EGameMode.Edit)
				{
					m_PlayToggle.SetIsOnWithoutNotify(false);
					return;
				}

				offAllUI();
				startSimulationDelegate?.Invoke();
			}
			else
			{
				finishSimulationDelegate?.Invoke();
			}
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
