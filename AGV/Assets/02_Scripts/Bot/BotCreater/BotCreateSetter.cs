using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BotCreateSetter : MonoBehaviour
{
	protected Delegates.VoidAction_VoidPlag setModeDelegate = null;
	protected Delegates.VoidPlag setPlagsOnClickDelegate = null;

	abstract public void Init();

	protected virtual void Awake()
	{
		setButtonEvent();
	}

	protected virtual void OnEnable()
	{
		Init();
		setModeDelegate?.Invoke(setPlagsOnClickEvent);
	}

	protected abstract void setPlagsOnClickEvent(in Plag _plag);
	protected abstract void setButtonEvent();

	public void SetModeCallback(Delegates.VoidAction_VoidPlag _setModeCallback)
	{
		setModeDelegate = _setModeCallback;
	}

	public void SetActive(bool _isActive)
    {
        this.gameObject.SetActive(_isActive);
    }
}
