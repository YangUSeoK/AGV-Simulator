using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotMovement : MonoBehaviour
{
	private NavMeshAgent m_Agent = null;
	private List<Plag> m_Path = null;

	private int m_StartIdx = 0;
	private int m_CurIdx = 0;

	private bool isArrive => Vector3.Distance(m_Agent.destination, transform.position) > m_Agent.speed * Time.deltaTime;

	private void Awake()
	{
		m_Agent = GetComponentInChildren<NavMeshAgent>();
	}

	private void Update()
	{
		if (isArrive)
		{
			m_CurIdx = (++m_CurIdx) % m_Path.Count;
			m_Agent.SetDestination(m_Path[++m_CurIdx].Pos);
		}	
	}

	public void StartSimulation()
	{
		m_Agent.SetDestination(m_Path[m_StartIdx].Pos);
	}

	public void SetMember(in List<Plag> _path, in int _startIdx)
	{
		m_Path = _path;
		m_StartIdx = _startIdx;
	}
}
