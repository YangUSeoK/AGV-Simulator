
using UnityEngine;

public class LocationManager : Manager<Location>
{
	public override void Create(in Vector3 _createPos)
	{
		base.Create(_createPos);
		m_Preview.SetActive(false);
		// 
	}

	public override void SetDelegate(in DelegatesInfo<Location> _delegates)
	{
		throw new System.NotImplementedException();
	}
}

public class LocationManagerDelegates : DelegatesInfo<Location>
{

}
