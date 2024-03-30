using Delegates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Creater<T> : MonoBehaviour where T : Item<T>
{
	protected Delegate<T> createdDelegate = null;
	protected Delegate onCreateModeDelegate = null;
	protected Delegate offCreateModeDelegate = null;
	protected Delegate<Delegate<Flag>> setFlagsOnClickEventDelegate = null;
	protected Delegate<Delegate<Flag>> setFlagsOnMouseEnterEventDelegate = null;
	protected Delegate<Delegate<Flag>> setFlagsOnMouseExitEventDelegate = null;

	protected Manager<T> m_Manager = null;
	protected GameObject m_BotPrefab = null;

	protected virtual void OnEnable()
	{
		// 켤 때 마다 플래그의 모드를 설정
		onCreateModeDelegate?.Invoke();
	}

	protected virtual void OnDisable()
	{
		// 끌 때 마다 플래그의 모드를 설정하고, SetOnclickEvent = null;
		offCreateModeDelegate?.Invoke();
		clear();
	}

	protected virtual void Update()
	{
		if (GameManager.GameMode != EGameMode.CreateBot)
		{
			this.gameObject.SetActive(false);
		}
	}

	public virtual void Init(in Manager<T> _manager, in GameObject _prefab)
	{
		m_Manager = _manager;
		m_BotPrefab = _prefab;

		init();
	}

	public virtual void SetDelegate(in CreaterDelegates<T> _delegates)
	{
		createdDelegate = _delegates.CreatedDelegate;
		onCreateModeDelegate = _delegates.OnCreateModeDelegate;
		offCreateModeDelegate = _delegates.OffCreateModeDelegate;
		setFlagsOnClickEventDelegate = _delegates.SetFlagsOnClickEventDelegate;
		setFlagsOnMouseEnterEventDelegate = _delegates.SetFlagsOnMouseEnterEventDelegate;
		setFlagsOnMouseExitEventDelegate = _delegates.SetFlagsOnMouseExitEventDelegate;

		setDelegate(_delegates);
	}


	public void SetActive(in bool _isActive)
	{
		this.gameObject.SetActive(_isActive);
	}

	

	protected virtual void cancel_Callback()
	{
		this.gameObject.SetActive(false);
	}

	protected virtual T create(in Vector3 _createPos, in ItemInfo<T> _info)
	{
		T item = Instantiate(m_BotPrefab, _createPos, Quaternion.identity, m_Manager.transform).GetComponent<T>();
		item.Init(_info);
		
		createdDelegate?.Invoke((T)item);

		clear();

		return item;
	}

	protected abstract void init();
	protected abstract void clear();
	protected abstract void setDelegate(in CreaterDelegates<T> _delegates);
}


public class CreaterDelegates<T>
{
	public CreaterDelegates(in Delegate<T> _createdCallback,
							in Delegate _onCreateModeCallback,
							in Delegate _offCreateModeCallback,
							in Delegate<Delegate<Flag>> _setFlagsOnClickEventCallback,
							in Delegate<Delegate<Flag>> _setFlagsOnMouseEnterEventCallback,
							in Delegate<Delegate<Flag>> _setFlagsOnMouseExitEventDelegate)
	{
		CreatedDelegate = _createdCallback;
		OnCreateModeDelegate = _onCreateModeCallback;
		OffCreateModeDelegate = _offCreateModeCallback;
		SetFlagsOnClickEventDelegate = _setFlagsOnClickEventCallback;
		SetFlagsOnMouseEnterEventDelegate = _setFlagsOnMouseEnterEventCallback;
		SetFlagsOnMouseExitEventDelegate = _setFlagsOnMouseExitEventDelegate;
	}

	public Delegate<T> CreatedDelegate { get; set; }
	public Delegate OnCreateModeDelegate { get; }
	public Delegate OffCreateModeDelegate { get; }
	public Delegate<Delegate<Flag>> SetFlagsOnClickEventDelegate { get; }
	public Delegate<Delegate<Flag>> SetFlagsOnMouseEnterEventDelegate { get; }
	public Delegate<Delegate<Flag>> SetFlagsOnMouseExitEventDelegate { get; }
}
