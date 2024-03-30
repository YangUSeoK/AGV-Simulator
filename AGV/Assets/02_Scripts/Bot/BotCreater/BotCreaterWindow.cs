using Delegates;
using UnityEngine;

public abstract class BotCreaterWindow : CreaterWindow<Bot>
{
	
}

public class BotCreaterWindowDelegates : CreaterWindowDelegates<Bot>
{
	public BotCreaterWindowDelegates(in Delegate<Delegate<Flag>> _setModeCallback) : base(_setModeCallback)
	{
	}
}