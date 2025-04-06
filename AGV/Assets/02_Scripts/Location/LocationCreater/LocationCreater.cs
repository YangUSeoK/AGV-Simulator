using Delegates;
using System.Collections.Generic;
using UnityEngine;

public class LocationCreater : Creater<Location>
{
	private LocationSetter m_LocationSetter = null;

	private void Awake()
	{
		m_LocationSetter = GetComponentInChildren<LocationSetter>();
		m_WindowList.Add(m_LocationSetter);
	}

	public void SetPreviewPos(LocationPreview _preview, Vector3 _previewPos)
	{
		m_LocationSetter.Preview = _preview;
	}

	protected override void clear()
	{
		foreach (var setter in m_WindowList)
		{
			setter.SetActive(false);
		}
		m_LocationSetter.SetActive(true);
	}

	protected override void init()
	{
		foreach (var setter in m_WindowList)
		{
			setter.Init();
		}

		clear();
	}

	protected override void setDelegate(in CreaterDelegates<Location> _delegates)
	{
		var locationSetterDelegates = new LocationSetterDelegates(startLocationSetter, applyLocationSetter, cancelLocationSetter);
		m_LocationSetter.SetDelegate(locationSetterDelegates);
	}

	private void startLocationSetter(in Delegate<Flag> _callback)
	{
		this.gameObject.SetActive(true);
		setFlagsOnClickEventDelegate?.Invoke(_callback);
	}

	private void applyLocationSetter(in List<Flag> _loadFlagList, in List<Flag> _unloadFlagList, in Vector3 _createPos)
	{
		var location = Create(_createPos);

		foreach (var flag in _loadFlagList)
		{
			flag.LoadLocation = location;
			location.AddLoadLocationList(flag);
		}

		foreach (var flag in _unloadFlagList)
		{
			flag.UnloadLocation = location;
			location.AddUnloadLocationList(flag);
		}

		this.gameObject.SetActive(false);
	}

	private void cancelLocationSetter()
	{
		this.gameObject.SetActive(false);
	}
}

public class LocationCreaterDelegates : CreaterDelegates<Location>
{
	public LocationCreaterDelegates(in Delegate<Location> _createdCallback, in Delegate<EGameMode> _startCreateModeCallback, in Delegate _finishCreateModeCallback, in Delegate<Delegate<Flag>> _setFlagsOnClickEventCallback, in Delegate<Delegate<Flag>> _setFlagsOnMouseEnterEventCallback, in Delegate<Delegate<Flag>> _setFlagsOnMouseExitEventDelegate) : base(_createdCallback, _startCreateModeCallback, _finishCreateModeCallback, _setFlagsOnClickEventCallback, _setFlagsOnMouseEnterEventCallback, _setFlagsOnMouseExitEventDelegate)
	{
	}

	public LocationCreaterDelegates(in Delegate<Delegate<Flag>> _setFlagsOnClickEventCallback) : base(_setFlagsOnClickEventCallback)
	{
	}
}
