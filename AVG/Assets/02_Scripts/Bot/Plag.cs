using UnityEngine;
using System;

public class Plag : MonoBehaviour, IScaler
{
	private Action<Plag> onClickEvent = null;
	private Action<Plag> onMouseOverEvent = null;

	[SerializeField] private Material m_SelectedMaterial = null;
	[SerializeField] private Material m_OrigMaterial = null;


	public bool IsSpawnPlag { get; set; }
	public Vector3 Pos => this.transform.position;

	private Bot m_CurBot = null;
	public Bot CurBot { set { m_CurBot = value; } }
	public bool IsBotHere => (m_CurBot == null);



	private bool m_IsAddMode = false;
	public bool IsAddMode { set { m_IsAddMode = value; } }

	private readonly float m_UpScaleValue = 1.5f;
	private Vector3 m_UpScale => new Vector3(m_OrigScale.x * m_UpScaleValue, m_OrigScale.y * m_UpScaleValue, m_OrigScale.z * m_UpScaleValue);
	private Vector3 m_OrigScale = Vector3.zero;

	private Renderer m_Renderer = null;


	private void Awake()
	{
		m_OrigScale = this.transform.localScale;
		m_Renderer = GetComponentInChildren<Renderer>();
	}

	public void OnMouseUp()
	{
		if (!m_IsAddMode) return;
		onClickEvent?.Invoke(this);
	}

	public void OnMouseEnter()
	{
		if (!m_IsAddMode) return;
		onMouseOverEvent?.Invoke(this);

		SetScale(m_UpScale);
	}
	public void OnMouseExit()
	{
		if (!m_IsAddMode) return;
		SetScale(m_OrigScale);
	}

	public void Selected(bool _isSelected)
	{
		if (_isSelected)
		{
			m_Renderer.material = m_SelectedMaterial;
		}
		else
		{
			m_Renderer.material = m_OrigMaterial;
		}
	}

	public void SetScale(Vector3 _scale)
	{
		this.transform.localScale = _scale;
	}

	public void SetOnClickEvent(Action<Plag> _onClickEvent)
	{
		onClickEvent = _onClickEvent;
	}

	public void SetOnMouseEnterEvent(Action<Plag> _onMouseEnterEvent)
	{
		onMouseOverEvent = _onMouseEnterEvent;
	}
}

