using Delegates;

public class BotManager : Manager<Bot>
{
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
		createItemDelegate?.Invoke(_newBot);
	}

	public override void setCreater(in CreaterDelegates<Bot> _delegates)
	{
		var delegates = _delegates as BotCreaterDelegates;
		delegates.CreatedDelegate = addNewBot;
	}

	protected override void setDelegate(in ManagerDelegates<Bot> _delegates)
	{
	}
}

public class BotManagerDelegates : ManagerDelegates<Bot>
{
	public BotManagerDelegates(in Delegate<Creater> _createCreaterCallback, in Delegate<Item> _createItemCallback) : base(_createCreaterCallback, _createItemCallback)
	{
	}
}
