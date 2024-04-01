using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Delegates;
using System.Linq;

public class LocationSetter : CreaterWindow<Location>
{
	private Delegate<List<Flag>, List<Flag>, Vector3> applyDelegate = null;
	private Delegate cancelDelegate = null;

	[SerializeField] private Button m_ApplyButton = null;
	[SerializeField] private Toggle m_LoadLocationModeToggle = null;
	[SerializeField] private Toggle m_UnloadLocationModeToggle = null;
	[SerializeField] private Button m_CancelButton = null;

	private bool m_IsLoadLocationSetMode = true;

	private readonly List<Flag> m_TempLoadFlagList = new List<Flag>();
	private readonly List<Flag> m_TempUnloadFlagList = new List<Flag>();

	private LocationPreview m_Preview = null;
	public LocationPreview Preview { set { m_Preview = value; } }

	public override void Init()
	{
		clear();
	}

	protected override void clear()
	{
		m_Item = null;
		m_Preview = null;
		m_TempLoadFlagList.Clear();
		m_TempUnloadFlagList.Clear();
		m_LoadLocationModeToggle.isOn = true;
		m_UnloadLocationModeToggle.isOn = false;
	}

	protected override void setButtonEvent()
	{
		m_ApplyButton.onClick.AddListener(() =>
		{
			allSelectedFlagOff();

			applyDelegate?.Invoke(m_TempLoadFlagList.ToList(), m_TempUnloadFlagList.ToList(), m_Preview.Pos);
			
			clear();
		});

		m_LoadLocationModeToggle.onValueChanged.AddListener((_isOn) =>
		{
			if (_isOn)
			{
				m_IsLoadLocationSetMode = true;
				allSelectedFlagOff();
				m_TempLoadFlagList.Clear();
			}
			else
			{
				allSelectedFlagOff();
			}
		});

		m_UnloadLocationModeToggle.onValueChanged.AddListener((_isOn) =>
		{
			if(_isOn)
			{
				m_IsLoadLocationSetMode = false;
				allSelectedFlagOff();
				m_TempUnloadFlagList.Clear();
			}
			else
			{
				allSelectedFlagOff();
			}
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
			m_TempLoadFlagList.Add(_flag);
		}
		else
		{
			m_TempUnloadFlagList.Add(_flag);
		}

		_flag.Selected(true);
	}

	public void startSetLocation(in Location _location)
	{
		this.gameObject.SetActive(true);
		m_Item = _location;
	}

	private void allSelectedFlagOff()
	{
		foreach (var flag in m_TempLoadFlagList)
		{
			flag.Selected(false);
		}

		foreach (var flag in m_TempUnloadFlagList)
		{
			flag.Selected(false);
		}
	}
}

public class LocationSetterDelegates : CreaterWindowDelegates<Location>
{
	public LocationSetterDelegates(in Delegate<Delegate<Flag>> _setModeCallback, in Delegate<List<Flag>, List<Flag>, Vector3> _applyCallback, in Delegate _cancelCallback) : base(_setModeCallback)
	{
		ApplyDelegate = _applyCallback;
		CancelDelegate = _cancelCallback;
	}

	public Delegate<List<Flag>, List<Flag>, Vector3> ApplyDelegate { get; set; }
	public Delegate CancelDelegate { get; set; }
}
