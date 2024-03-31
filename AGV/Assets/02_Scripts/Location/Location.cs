using UnityEngine;
using System.Collections.Generic;

public class Location : Item<Location>
{
	private List<Flag> m_LoadLocationList = new List<Flag>();
	private List<Flag> m_UnloadLocationList = new List<Flag>();

	private int m_MaxLoadCnt = 5;
	public int MaxLoadCnt => m_MaxLoadCnt;

	private int m_CurLoadCnt = 0;
	public int CurLoadCnt => m_CurLoadCnt;

	public void Load(in int _cnt)
	{
		m_CurLoadCnt = Mathf.Clamp(m_CurLoadCnt + _cnt, m_CurLoadCnt + _cnt, m_MaxLoadCnt);
	}

	public void Unload(in int _cnt)
	{
		m_CurLoadCnt = Mathf.Clamp(m_CurLoadCnt, 0, m_CurLoadCnt - _cnt);
	}

	public void ClearLoadLocationList()
	{
		m_LoadLocationList.Clear();
	}

	public void ClearUnloadLocationList()
	{
		m_UnloadLocationList.Clear();
	}

	public void AddLoadLocationList(in Flag _loadFlag)
	{
		m_LoadLocationList.Add(_loadFlag);
	}

	public void AddUnloadLocationList(in Flag _unloadFlag)
	{
		m_UnloadLocationList.Add(_unloadFlag);
	}

	public void RemoveLoadLocationList(in Flag _loadFlag)
	{
		if (m_LoadLocationList.Contains(_loadFlag))
		{
			m_LoadLocationList.Remove(_loadFlag);
		}
	}

	public void RemoveUnloadLocationList(in Flag _unloadFlag)
	{
		if(m_UnloadLocationList.Contains(_unloadFlag))
		{
			m_UnloadLocationList.Remove(_unloadFlag);
		}
	}

	public override void StartSimulation()
	{
	}

	public override void FinishSimulation()
	{
	}

	protected override void onClick()
	{
		onClickDelegate?.Invoke(this);
	}

	protected override void onMouseEnter()
	{
		onMouseEnterDelegate?.Invoke(this);
	}

	protected override void onMouseExit()
	{
		onMouseExitDelegate?.Invoke(this);
	}

	protected override void clear()
	{
	}

	public override void Init(in ItemInfo<Location> _container)
	{

	}
}

public class LocationInfo : ItemInfo<Location>
{

}
