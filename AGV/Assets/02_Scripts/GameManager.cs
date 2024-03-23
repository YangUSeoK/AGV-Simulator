using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField] private Canvas m_Canvas = null;

	private BotManager m_BotManager = null;
	private UIManager m_UIManager = null;

	private void Awake()
	{
		m_BotManager = GetComponentInChildren<BotManager>();
		m_UIManager = GetComponentInChildren<UIManager>();

		m_BotManager.SetMember(m_Canvas);
		m_BotManager.SetDelegate(instantiateBotCreaterCallback);



		m_UIManager.SetDelegate(startSimulation);
	}

	private void startSimulation()
	{
		m_BotManager.StartSimulation();
		m_UIManager.StartSimulation();
	}

	private void instantiateBotCreaterCallback(in BotCreater _botCreater)
	{
		m_UIManager.BotCreater = _botCreater;
	}
}
