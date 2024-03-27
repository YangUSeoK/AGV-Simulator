using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preview : MonoBehaviour
{
	[SerializeField] protected Material m_CanMaterial = null;
	[SerializeField] protected Material m_CantMaterial = null;

	protected Renderer m_Renderer = null;

	protected void Awake()
	{
		m_Renderer = GetComponentInChildren<Renderer>();
	}

	public void SetActive(in bool _isActive)
	{
		this.gameObject.SetActive(_isActive);
	}
}
