using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using System;

public class BotManager : MonoBehaviour
{
	private Delegates.VoidBotCreater instantiateBotCreaterDelegate = null;

	[SerializeField] private GameObject BotCreaterPrefab = null;

	private Canvas m_Canvas = null;

	private List<Bot> m_BotList = new List<Bot>();
	private BotCreater m_BotCreater = null;

	private Plag[] m_Plags = null;

	private void Awake()
	{
		m_Plags = GetComponentsInChildren<Plag>();

		GameObject go = Instantiate(BotCreaterPrefab, m_Canvas.transform);
	
		m_BotCreater = go.GetComponent<BotCreater>();
		m_BotCreater.SetMember(this, m_Plags);
		m_BotCreater.SetCallback(addNewBot);

		go.SetActive(true);

		m_BotCreater.SetActive(false);

		instantiateBotCreaterDelegate?.Invoke(m_BotCreater);
	}

	public void StartSimulation()
	{ 
		foreach(var bot in m_BotList)
		{
			bot.StartSimulation();
		}
	}

	public void SetMember(in Canvas _canvas)
	{
		m_Canvas = _canvas;
	}
	public void SetDelegate(in Delegates.VoidBotCreater _instantiateBotCreaterCallback)
	{
		instantiateBotCreaterDelegate = _instantiateBotCreaterCallback;
	}

	private void addNewBot(in Bot _newBot)
	{
		m_BotList.Add(_newBot);
	}
}

