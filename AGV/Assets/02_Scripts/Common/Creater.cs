using Delegates;
using System.Collections.Generic;
using UnityEngine;

public abstract class Creater : MonoBehaviour
{
	protected Delegate<EGameMode> startCreateModeDelegate = null;
	protected Delegate finishCreateModeDelegate = null;
	protected Delegate<Delegate<Flag>> setFlagsOnClickEventDelegate = null;
	protected Delegate<Delegate<Flag>> setFlagsOnMouseEnterEventDelegate = null;
	protected Delegate<Delegate<Flag>> setFlagsOnMouseExitEventDelegate = null;

	protected GameObject m_Prefab = null;

	protected readonly List<CreaterWindow> m_WindowList = new List<CreaterWindow>();

	protected virtual void OnDisable()
	{
		// 끌 때 마다 플래그의 모드를 설정하고, SetOnclickEvent = null;
		finishCreateModeDelegate?.Invoke();
		clear();
	}

	public void SetActive(in bool _isActive)
	{
		this.gameObject.SetActive(_isActive);
	}

	protected virtual void cancel_Callback()
	{
		this.gameObject.SetActive(false);
	}

	protected abstract void init();
	protected abstract void clear();
}

public abstract class Creater<T> : Creater where T : Item<T>
{
	protected Delegate<T> createdDelegate = null;

	protected Manager<T> m_Manager = null;
	protected Preview<T> m_Preview = null;

	protected virtual void OnEnable()
	{
		// 켤 때 마다 플래그의 모드를 설정
		startCreateModeDelegate?.Invoke(m_Manager.CreateMode);
	}

	protected virtual void Update()
	{
		if (GameManager.GameMode != m_Manager.CreateMode)
		{
			this.gameObject.SetActive(false);
		}
	}

	public void Init(in Manager<T> _manager, in Preview<T> _preview, in GameObject _prefab)
	{
		m_Manager = _manager;
		m_Preview = _preview;
		m_Prefab = _prefab;

		init();
	}

	public virtual void SetDelegate(in CreaterDelegates<T> _delegates)
	{
		createdDelegate = _delegates.CreatedDelegate;
		startCreateModeDelegate = _delegates.StartCreateModeDelegate;
		finishCreateModeDelegate = _delegates.FinishCreateModeDelegate;
		setFlagsOnClickEventDelegate = _delegates.SetFlagsOnClickEventDelegate;
		setFlagsOnMouseEnterEventDelegate = _delegates.SetFlagsOnMouseEnterEventDelegate;
		setFlagsOnMouseExitEventDelegate = _delegates.SetFlagsOnMouseExitEventDelegate;

		setDelegate(_delegates);
	}

	public virtual T Create(in Vector3 _createPos, in ItemInfo<T> _info = null)
	{
		T item = Instantiate(m_Prefab, _createPos, Quaternion.identity, m_Manager.transform).GetComponent<T>();
		item.Init(_info);

		createdDelegate?.Invoke(item);

		clear();

		return item;
	}

	protected abstract void setDelegate(in CreaterDelegates<T> _delegates);
}


public class CreaterDelegates<T>
{
	public CreaterDelegates(in Delegate<T> _createdCallback,
							in Delegate<EGameMode> _startCreateModeCallback,
							in Delegate _finishCreateModeCallback,
							in Delegate<Delegate<Flag>> _setFlagsOnClickEventCallback,
							in Delegate<Delegate<Flag>> _setFlagsOnMouseEnterEventCallback,
							in Delegate<Delegate<Flag>> _setFlagsOnMouseExitEventDelegate)
	{
		CreatedDelegate = _createdCallback;
		StartCreateModeDelegate = _startCreateModeCallback;
		FinishCreateModeDelegate = _finishCreateModeCallback;
		SetFlagsOnClickEventDelegate = _setFlagsOnClickEventCallback;
		SetFlagsOnMouseEnterEventDelegate = _setFlagsOnMouseEnterEventCallback;
		SetFlagsOnMouseExitEventDelegate = _setFlagsOnMouseExitEventDelegate;
	}

	public CreaterDelegates(in Delegate<Delegate<Flag>> _setFlagsOnClickEventCallback)
	{
		SetFlagsOnClickEventDelegate = _setFlagsOnClickEventCallback;
	}

	public CreaterDelegates()
	{
	}

	public Delegate<T> CreatedDelegate { get; set; }
	public Delegate<EGameMode> StartCreateModeDelegate { get; }
	public Delegate FinishCreateModeDelegate { get; }
	public Delegate<Delegate<Flag>> SetFlagsOnClickEventDelegate { get; }
	public Delegate<Delegate<Flag>> SetFlagsOnMouseEnterEventDelegate { get; }
	public Delegate<Delegate<Flag>> SetFlagsOnMouseExitEventDelegate { get; }
}
