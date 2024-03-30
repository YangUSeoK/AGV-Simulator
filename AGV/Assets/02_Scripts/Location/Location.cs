using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : Item<Location>
{
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

	public override void StartSimulation()
	{
	}

	public override void FinishSimulation()
	{
	}

	protected override void onClick()
	{
	}

	protected override void onMouseEnter()
	{
	}

	protected override void onMouseExit()
	{
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
