using Delegates;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public abstract class Manager<T> : MonoBehaviour where T : Item<T>
{
	protected Delegate<Creater> createCreaterDelegate = null;
	protected Delegate<T> createItemDelegate = null; 

	[SerializeField] protected GameObject m_Prefab = null;
	[SerializeField] protected GameObject m_PreviewPrefab = null;
	[SerializeField] protected GameObject m_CreaterPrefab = null;

	[SerializeField] protected Toggle m_CreateModeToggle = null;

	[SerializeField] protected EGameMode m_CreateMode;
	public EGameMode CreateMode => m_CreateMode;

	protected Creater<T> m_Creater = null;

	protected Preview<T> m_Preview = null;
	protected readonly List<T> m_List = new List<T>();
	public List<T> List => m_List;

	public virtual void Init(in CreaterDelegates<T> _delegates)
	{
		SetCreater(_delegates);
		setEvent();
	}

	public virtual void SetOnClickEvent(in Delegate<T> _onClickEvent)
	{
		foreach (var item in m_List)
		{
			item.SetOnClickEvent(_onClickEvent);
		}
	}

	public virtual void SetOnMouseEnterEvent(in Delegate<T> _onMouseEnterEvent)
	{
		foreach (var item in m_List)
		{
			item.SetOnMouseEnterEvent(_onMouseEnterEvent);
		}
	}

	public virtual void SetOnMouseExitEvent(in Delegate<T> _onMouseExitEvent)
	{
		foreach (var item in m_List)
		{
			item.SetOnMouseExitEvent(_onMouseExitEvent);
		}
	}

	public virtual void StartSimulation()
	{
		foreach(var item in m_List)
		{
			item.StartSimulation();
		}
	}

	public virtual void FinishSimulation()
	{
		foreach (var item in m_List)
		{
			item.FinishSimulation();
		}
	}

	public virtual void Create(in Vector3 _createPos)
	{
		T item = Instantiate(m_Prefab, _createPos, Quaternion.identity, this.transform).GetComponent<T>();
		m_List.Add(item);
	}

	public void SetCreater(in CreaterDelegates<T> _delegates)
	{
		m_Creater = Instantiate(m_CreaterPrefab).GetComponent<Creater<T>>();
	
		m_Preview = Instantiate(m_PreviewPrefab, this.transform).GetComponent<Preview<T>>();
		m_Preview.Init(this);
		m_Preview.SetActive(false);

		setCreater(_delegates);

		m_Creater.Init(this, m_Preview, m_Prefab);
		m_Creater.SetDelegate(_delegates);
		m_Creater.SetActive(false);

		createCreaterDelegate?.Invoke(m_Creater);
	}

	protected virtual void setEvent()
	{
		m_CreateModeToggle.onValueChanged.AddListener((_isOn) =>
		{
			if (_isOn)
			{
				if (GameManager.GameMode != EGameMode.Edit)
				{
					m_CreateModeToggle.SetIsOnWithoutNotify(false);
					return;
				}
				else
				{
					startCreateMode();
				}
			}
			else
			{
				finishCreateMode();
			}
		});
	}
	protected virtual void startCreateMode()
	{
		GameManager.GameMode = CreateMode;
		m_Preview.SetActive(true);
	}

	protected virtual void finishCreateMode()
	{
		GameManager.GameMode = EGameMode.Edit;
		m_Preview.SetActive(false);
	}

	public virtual void SetDelegate(in ManagerDelegates<T> _delegates)
	{
		createCreaterDelegate = _delegates.CreateCreaterDelegate;
		setDelegate(_delegates);
	}
	public abstract void setCreater(in CreaterDelegates<T> _delegates);

	protected abstract void setDelegate(in ManagerDelegates<T> _delegates);
}

public class ManagerDelegates<T> where T : Item<T>
{
	public ManagerDelegates(in Delegate<Creater> _createCreaterCallback, in Delegate<T> _createItemCallback)
	{
		CreateCreaterDelegate = _createCreaterCallback;
		CreateItemDelegate = _createItemCallback;
	}

	public Delegate<Creater> CreateCreaterDelegate { get; }
	public Delegate<T> CreateItemDelegate { get; }
}