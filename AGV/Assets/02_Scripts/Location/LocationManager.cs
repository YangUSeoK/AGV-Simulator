using Delegates;
using UnityEngine;

public class LocationManager : Manager<Location>
{
	public override void Create(in Vector3 _createPos)
	{
		base.Create(_createPos);
		m_Preview.SetActive(false);
		// 
	}

	public override void setCreater(in CreaterDelegates<Location> _delegates)
	{ }

	protected override void setDelegate(in ManagerDelegates<Location> _delegates)
	{
	}

	public class LocationManagerDelegates : ManagerDelegates<Location>
	{
		public LocationManagerDelegates(in Delegate<Creater> _createCreaterCallback, in Delegate<Location> _createItemCallback) : base(_createCreaterCallback, _createItemCallback)
		{
		}
	}
}
