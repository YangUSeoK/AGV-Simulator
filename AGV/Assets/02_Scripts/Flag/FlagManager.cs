
using Delegates;

public class FlagManager : Manager<Flag>
{
	public override void setCreater(in CreaterDelegates<Flag> _delegates)
	{
	}

	public override void SetDelegate(in ManagerDelegates<Flag> _delegates)
	{

	}

	protected override void setDelegate(in ManagerDelegates<Flag> _delegates)
	{
	}
}

public class FlagManagerDelegates : ManagerDelegates<Flag>
{
	public FlagManagerDelegates(in Delegate<Creater> _createCreaterCallback, in Delegate<Flag> _createItemCallback) : base(_createCreaterCallback, _createItemCallback)
	{
	}
}
