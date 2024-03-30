using Delegates;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Manager<T> : MonoBehaviour where T : Item<T>
{
	[SerializeField] protected GameObject m_Prefab = null;
	[SerializeField] protected GameObject m_PreviewPrefab = null;

	[SerializeField] protected Toggle m_CreateModeToggle = null;

	[SerializeField] protected EGameMode m_CreateMode;
	public EGameMode CreateMode => m_CreateMode;

	protected Preview<T> m_Preview = null;
	protected readonly List<T> m_List = new List<T>();
	public List<T> List => m_List;

	
	public virtual void Init()
	{
		m_Preview = Instantiate(m_PreviewPrefab, this.transform).GetComponent<Preview<T>>();
		m_Preview.Init(this);
		m_Preview.SetActive(false);

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

	public abstract void SetDelegate(in DelegatesInfo<T> _delegates);

}