using Delegates;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BotCreateSetter : MonoBehaviour
{
	protected Delegate<Delegate<Flag>> setModeDelegate = null;
	protected Delegate<Flag> setPlagsOnClickDelegate = null;

	abstract public void Clear();

	protected virtual void Awake()
	{
		setButtonEvent();
	}

	protected virtual void OnEnable()
	{
		Clear();
		setModeDelegate?.Invoke(setPlagsOnClickEvent);
	}

	protected abstract void setPlagsOnClickEvent(in Flag _plag);
	protected abstract void setButtonEvent();

	public void SetModeCallback(Delegate<Delegate<Flag>> _setModeCallback)
	{
		setModeDelegate = _setModeCallback;
	}

	public void SetActive(bool _isActive)
    {
        this.gameObject.SetActive(_isActive);
    }
}
