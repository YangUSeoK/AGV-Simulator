using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using System;

public class BotManager : MonoBehaviour
{
	[SerializeField] private GameObject BotCreaterPrefab = null;
	[SerializeField] private Canvas m_Canvas = null;

	private List<Bot> m_BotList = new List<Bot>();
	private BotCreater m_BotCreater = null;

	private Plag[] m_Plags = null;

	private void Awake()
	{
		m_Plags = GetComponentsInChildren<Plag>();

		GameObject go = Instantiate(BotCreaterPrefab, m_Canvas.transform);
	
		m_BotCreater = go.GetComponent<BotCreater>();
		m_BotCreater.SetMember(this, m_Plags);
		m_BotCreater.SetCallback(AddNewBot);

		go.SetActive(true);

		m_BotCreater.SetActive(false);
	}

	private void AddNewBot(Bot _newBot)
	{
		m_BotList.Add(_newBot);
	}
}
