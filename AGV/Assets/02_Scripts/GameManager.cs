using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EGameMode
{
	Edit = 0,
	AddBot,
	Play,

	Length,
}

public class GameManager : MonoBehaviour
{
	[SerializeField] private Canvas m_Canvas = null;

	private BotManager m_BotManager = null;
	private UIManager m_UIManager = null;

	private void Awake()
	{
		m_BotManager = GetComponentInChildren<BotManager>();
		m_UIManager = GetComponentInChildren<UIManager>();

		m_UIManager.SetDelegate(startSimulation, instBotCreaterCallback);

		m_UIManager.Init();
	}

	private void startSimulation()
	{
		m_BotManager.StartSimulation();
		m_UIManager.StartSimulation();
	}

	private void instBotCreaterCallback(in BotCreater _botCreater)
	{
		m_BotManager.SetBotCreater(_botCreater);
	}

}
