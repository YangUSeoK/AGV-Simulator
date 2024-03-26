using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using System;
using Delegates;

public class BotManager : MonoBehaviour
{
	private readonly List<Bot> m_BotList = new List<Bot>();

	private Flag[] m_Plags = null;

	private void Awake()
	{
		m_Plags = GetComponentsInChildren<Flag>();
	}

	public void StartSimulation()
	{ 
		foreach(var bot in m_BotList)
		{
			bot.StartSimulation();
		}
	}

	public void SetBotCreater(BotCreater _botCreater)
	{
		_botCreater.Init(this, m_Plags);
		_botCreater.SetCallback(addNewBot);
	}

	private void addNewBot(in Bot _newBot)
	{
		m_BotList.Add(_newBot);
	}
}

