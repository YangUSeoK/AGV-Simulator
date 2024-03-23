using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BotState 
{
	public abstract void EnterState();
	public abstract void UpdateState();
	public abstract void ExitState();
}

public class MoveState : BotState
{
	public override void EnterState()
	{
		
	}

	public override void ExitState()
	{
		
	}

	public override void UpdateState()
	{
		
	}
}