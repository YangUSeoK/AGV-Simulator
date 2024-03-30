using Delegates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	public FlagCreaterDelegates(in Delegate<Flag> _createdCallback, in Delegate _onCreateModeCallback, in Delegate _offCreateModeCallback, in Delegate<Delegate<Flag>> _setFlagsOnClickEventCallback, in Delegate<Delegate<Flag>> _setFlagsOnMouseEnterEventCallback, in Delegate<Delegate<Flag>> _setFlagsOnMouseExitEventDelegate) : base(_createdCallback, _onCreateModeCallback, _offCreateModeCallback, _setFlagsOnClickEventCallback, _setFlagsOnMouseEnterEventCallback, _setFlagsOnMouseExitEventDelegate)
	{
	}

	public FlagCreaterDelegates() : base() { }
}