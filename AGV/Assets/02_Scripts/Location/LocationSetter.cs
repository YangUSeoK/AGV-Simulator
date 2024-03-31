using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Delegates;

public class LocationSetter : CreaterWindow<Location>
{
	private Delegate applyDelegate = null;
	private Delegate cancelDelegate = null;

	[SerializeField] private Button m_ApplyButton = null;
	[SerializeField] private Button m_LoadLocationModeButton = null;
	[SerializeField] private Button m_UnloadLocationModeButton = null;
	[SerializeField] private Button m_CancelButton = null;

	private bool m_IsLoadLocationSetMode = true;

	private readonly List<Flag> m_TempLoadLocationList = new List<Flag>();
	private readonly List<Flag> m_TempUnloadLocationList = new List<Flag>();


	public override void Init()
	{
		clear();
	}

	protected override void clear()
	{
		m_Item = null;
		m_TempLoadLocationList.Clear();
		m_TempUnloadLocationList.Clear();
	}

	protected override void setButtonEvent()
	{
		m_ApplyButton.onClick.AddListener(() =>
		{
			foreach(var loadLocation in m_TempLoadLocationList)
			{
				loadLocation.LoadLocation = m_Item;
				m_Item.AddLoadLocationList(loadLocation);
			}

			foreach(var unloadLocation in m_TempUnloadLocationList)
			{
				unloadLocation.UnloadLocation = m_Item;
				m_Item.AddUnloadLocationList(unloadLocation);
			}

			applyDelegate?.Invoke();
			clear();
		});

		m_LoadLocationModeButton.onClick.AddListener(() =>
		{
			m_IsLoadLocationSetMode = true;
			m_Item.ClearLoadLocationList();
		});

		m_UnloadLocationModeButton.onClick.AddListener(() =>
		{
			m_IsLoadLocationSetMode = false;
			m_Item.ClearUnloadLocationList();
		});

		m_CancelButton.onClick.AddListener(() =>
		{
			cancelDelegate?.Invoke();
			clear();
		});
	}

	protected override void setDelegate(in CreaterWindowDelegates<Location> _delegates)
	{
		var delegates = _delegates as LocationSetterDelegates;

		applyDelegate = delegates.ApplyDelegate;
		cancelDelegate = delegates.CancelDelegate;
	}

	protected override void flagsOnClickEvent(in Flag _flag)
	{
		if (m_IsLoadLocationSetMode)
		{
			_flag.LoadLocation = m_Item;
			m_TempLoadLocationList.Add(_flag);
		}
		else
		{
			_flag.UnloadLocation = m_Item;
			m_TempUnloadLocationList.Add(_flag);
		}

		_flag.Selected(true);
	}

	public void startSetLocation(in Location _location)
	{
		Debug.Log("StartSetLocation");
		this.gameObject.SetActive(true);
		m_Item = _location;
	}
}

public class LocationSetterDelegates : CreaterWindowDelegates<Location>
{
	public LocationSetterDelegates(in Delegate<Delegate<Flag>> _setModeCallback, in Delegate _applyCallback, in Delegate _cancelCallback) : base(_setModeCallback)
	{
		ApplyDelegate = _applyCallback;
		CancelDelegate = _cancelCallback;
	}

	public Delegate ApplyDelegate { get; set; }
	public Delegate CancelDelegate { get; set; }
}
