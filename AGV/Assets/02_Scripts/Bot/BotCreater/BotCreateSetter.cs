using Delegates;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BotCreateSetter : MonoBehaviour
{
	protected Delegate<Delegate<Flag>> setModeDelegate = null;
	protected Delegate<Flag> setPlagsOnClickDelegate = null;

	protected virtual void Awake()
	{
		setButtonEvent();
	}

	protected virtual void OnEnable()
	{
		clear();
		setModeDelegate?.Invoke(setPlagsOnClickEvent);
	}

	protected virtual void OnDisable()
	{
		clear();
	}

	abstract public void Init();
	abstract protected void clear();
	abstract protected void setPlagsOnClickEvent(in Flag _plag);
	abstract protected void setButtonEvent();

	public void SetModeCallback(Delegate<Delegate<Flag>> _setModeCallback)
	{
		setModeDelegate = _setModeCallback;
	}

	public void SetActive(bool _isActive)
    {
        this.gameObject.SetActive(_isActive);
    }
}
