using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Delegates;

public class UIManager : MonoBehaviour
{
	private Delegate startSimulationDelegate = null;
	private Delegate finishSimulationDelegate = null;

	[SerializeField] private Toggle m_PlayToggle = null;
	[SerializeField] private Transform m_CreaterPos = null;

	private readonly List<GameObject> m_UiList = new List<GameObject>();

	public void StartSimulation()
	{
		offAllUI();
	}

	public void SetCreater(Creater _creater)
	{
		_creater.transform.SetParent(m_CreaterPos);
		_creater.transform.localPosition = Vector3.zero;
		m_UiList.Add(_creater.gameObject);
	}

	public void Init()
	{
		setEvent();
	}

	public void SetDelegate(in UIManagerDelegates _delegates)
	{
		startSimulationDelegate = _delegates.StartSimulationDelegate;
		finishSimulationDelegate = _delegates.FinishSimulationDelegate;
	}

	private void setEvent()
	{
		m_PlayToggle.onValueChanged.AddListener((_isOn) =>
		{
			if(_isOn)
			{
				if(GameManager.GameMode != EGameMode.Edit)
				{
					m_PlayToggle.SetIsOnWithoutNotify(false);
					return;
				}

				offAllUI();
				startSimulationDelegate?.Invoke();
			}
			else
			{
				finishSimulationDelegate?.Invoke();
			}
		});
	}

	private void offAllUI()
	{
		foreach (var ui in m_UiList)
		{
			ui.SetActive(false);
		}
	}
}

public class UIManagerDelegates
{
	public UIManagerDelegates(in Delegate _startSimulationCallback,  in Delegate _finishSimulationCallback)
	{
		StartSimulationDelegate = _startSimulationCallback;
		FinishSimulationDelegate = _finishSimulationCallback;
	}

	public Delegate StartSimulationDelegate { get; }
	public Delegate FinishSimulationDelegate { get; }
}
