using Delegates;

public class FlagCreater : Creater<Flag>
{
	protected override void clear()
	{
		
	}

	protected override void init()
	{
		
	}

	protected override void setDelegate(in CreaterDelegates<Flag> _delegates)
	{
		
	}
}

public class FlagCreaterDelegates : CreaterDelegates<Flag>
{
	public FlagCreaterDelegates(in Delegate<Flag> _createdCallback, in Delegate<EGameMode> _startCreateModeCallback, in Delegate _finishCreateModeCallback, in Delegate<Delegate<Flag>> _setFlagsOnClickEventCallback, in Delegate<Delegate<Flag>> _setFlagsOnMouseEnterEventCallback, in Delegate<Delegate<Flag>> _setFlagsOnMouseExitEventDelegate) : base(_createdCallback, _startCreateModeCallback, _finishCreateModeCallback, _setFlagsOnClickEventCallback, _setFlagsOnMouseEnterEventCallback, _setFlagsOnMouseExitEventDelegate)
	{
	}

	public FlagCreaterDelegates() : base() { }
}