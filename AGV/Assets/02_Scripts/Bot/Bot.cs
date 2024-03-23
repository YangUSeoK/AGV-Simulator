using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
	private int m_Priority = 0;
	public int Priority => m_Priority;


	private Plag m_SpawnPlag = null;
	private List<Plag> m_RouteList = null;
	private Plag m_LoadPlag = null;
	private Plag m_UnloadPlag = null;

	public void SetMember(Plag _spawnPlag, Plag _loadPlag, Plag _unloadPlag, int _priority, List<Plag> _routeList)
	{
		m_SpawnPlag = _spawnPlag;
		m_LoadPlag = _loadPlag;
		m_UnloadPlag = _unloadPlag;
		m_Priority = _priority;
		m_RouteList = _routeList;
	}


	[ContextMenu("Test")]
	public void Test()
	{
		Debug.Log($"Spawn : {m_SpawnPlag}");
		Debug.Log($"Load : {m_LoadPlag}");
		Debug.Log($"Unload : {m_UnloadPlag}");
		Debug.Log($"Priority : {m_Priority}");

		Debug.Log(m_RouteList.Count);
		foreach (var plag in m_RouteList)
		{
			Debug.Log($"plag : {plag.name}");
		}

	}
}
