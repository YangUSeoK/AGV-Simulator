using System.Collections.Generic;
using UnityEngine;
using Delegates;

public class BotManager : MonoBehaviour
{
	private Delegate createBotDelegate = null;

	private readonly List<Bot> m_BotList = new List<Bot>();

	public void StartSimulation()
	{ 
		foreach(var bot in m_BotList)
		{
			bot.StartSimulation();
		}
	}

	public void FinishSimulation()
	{
		foreach(var bot in m_BotList)
		{
			bot.FinishSimulation();
		}
	}

	public void Init()
	{
		
	}

	public void SetDelegate(in Delegate _createBotCallback)
	{
		createBotDelegate = _createBotCallback;
	}

	public void SetBotCreater(in BotCreater _botCreater, in Delegate _createBotCallback,
							  in Delegate _startCreateBotModeCallback, in Delegate _finishCreateBotModeCallback,
							  in Delegate<Delegate<Flag>> _setFlagsOnClickEventCallback, in Delegate<Delegate<Flag>> _setFlagsOnMouseEnterEventDelegate)
	{
		_botCreater.Init(this);
		_botCreater.SetCallback(addNewBot, _startCreateBotModeCallback, _finishCreateBotModeCallback,
								_setFlagsOnClickEventCallback, _setFlagsOnMouseEnterEventDelegate);
	}

	private void addNewBot(in Bot _newBot)
	{
		m_BotList.Add(_newBot);
		createBotDelegate?.Invoke();
	}
}

