using System.Collections.Generic;
using UnityEngine;
using Delegates;

public class BotManager : Manager<Bot>
{
	private Delegate<Bot> createBotDelegate = null;

	private BotCreater m_Creater = null;

	
	public override void SetDelegate(in DelegatesInfo<Bot> _delegates)
	{
		var delegates = _delegates as BotManagerDelegates;

		createBotDelegate = delegates.CreateBotDelegate;
	}

	public void SetBotCreater(in BotCreater _creater, in BotCreaterDelegates _botCreaterDelegates)
	{
		m_Creater = _creater;
		_creater.Init(this, m_Prefab);

		_botCreaterDelegates.CreatedDelegate = addNewBot;
		_creater.SetDelegate(_botCreaterDelegates);
	}

	protected override void startCreateMode()
	{
		GameManager.GameMode = CreateMode;
		m_Creater.SetActive(true);
	}

	protected override void finishCreateMode()
	{
		GameManager.GameMode = EGameMode.Edit;
		m_Creater.SetActive(false);
	}

	private void addNewBot(in Bot _newBot)
	{
		m_List.Add(_newBot);
		createBotDelegate?.Invoke(_newBot);
	}
}

public class BotManagerDelegates : DelegatesInfo<Bot>
{
	public BotManagerDelegates(in Delegate<Bot> _createBotCallback)
	{
		CreateBotDelegate = _createBotCallback;
	}
	public Delegate<Bot> CreateBotDelegate { get; }
}