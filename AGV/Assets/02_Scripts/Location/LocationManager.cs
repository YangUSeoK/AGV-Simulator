using Delegates;
using UnityEngine;

public class LocationManager : Manager<Location>
{
	public void CreatePreview(LocationPreview _preview, Vector3 _pos)
	{
		(m_Creater as LocationCreater).SetPreviewPos(_preview, _pos);
	}


	public override void setCreater(in CreaterDelegates<Location> _delegates)
	{
		var delegates = _delegates as LocationCreaterDelegates;
		delegates.CreatedDelegate = addLocation;
	}

	protected override void setDelegate(in ManagerDelegates<Location> _delegates)
	{
		
	}

	protected override void startCreateMode()
	{
		GameManager.GameMode = CreateMode;
		m_Creater.SetActive(true);
		m_Preview.SetActive(true);
	}

	protected override void finishCreateMode()
	{
		GameManager.GameMode = EGameMode.Edit;
		m_Creater.SetActive(false);
		m_Preview.SetActive(true);
	}

	private void addLocation(in Location _newLocation)
	{
		m_List.Add(_newLocation);
		m_CreateModeToggle.isOn = false;
		createItemDelegate?.Invoke(_newLocation);
	}
}

public class LocationManagerDelegates : ManagerDelegates<Location>
{
	public LocationManagerDelegates(in Delegate<Creater> _createCreaterCallback, in Delegate<Item> _createItemCallback) : base(_createCreaterCallback, _createItemCallback)
	{
	}
}