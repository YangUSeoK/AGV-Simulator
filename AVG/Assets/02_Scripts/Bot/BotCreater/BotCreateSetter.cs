using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BotCreateSetter : MonoBehaviour
{
	protected Action<Action<Plag>> setModeCallback = null;

    abstract public void Init();

	protected virtual void Awake()
	{
		SetButtonEvent();
	}

	protected virtual void OnEnable()
	{
		Init();
		setModeCallback?.Invoke(SetPlagsOnClickEvent);
	}

	protected abstract void SetPlagsOnClickEvent(Plag _plag);
	protected abstract void SetButtonEvent();

	public void SetModeCallback(Action<Action<Plag>> _setModeCallback)
	{
		setModeCallback = _setModeCallback;
	}

	public void SetActive(bool _isActive)
    {
        this.gameObject.SetActive(_isActive);
    }
}
