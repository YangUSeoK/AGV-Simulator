using Delegates;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
	[SerializeField] protected Material m_SelectedMaterial = null;
	[SerializeField] protected Material m_OrigMaterial = null;

	[SerializeField] protected EGameMode m_GameMode;

	public Vector3 Pos => this.transform.position;

	protected readonly float m_UpScaleValue = 1.5f;
	protected Vector3 m_UpScale => new Vector3(m_OrigScale.x * m_UpScaleValue, m_OrigScale.y * m_UpScaleValue, m_OrigScale.z * m_UpScaleValue);
	protected Vector3 m_OrigScale = Vector3.zero;

	protected Renderer m_Renderer = null;

	protected virtual void Awake()
	{
		m_OrigScale = this.transform.localScale;
		m_Renderer = GetComponentInChildren<Renderer>();
	}

	public void OnMouseUp()
	{
		if (GameManager.GameMode == EGameMode.Play) return;

		onClick();
	}

	public void OnMouseEnter()
	{
		if (GameManager.GameMode == EGameMode.Play) return;

		onMouseEnter();
	}

	public void OnMouseExit()
	{
		if (GameManager.GameMode == EGameMode.Play) return;

		onMouseExit();
	}

	public void Selected(in bool _isSelected)
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

	protected void SetScale(in Vector3 _scale)
	{
		this.transform.localScale = _scale;
	}

	public abstract void StartSimulation();
	public abstract void FinishSimulation();

	protected abstract void onClick();
	protected abstract void onMouseEnter();
	protected abstract void onMouseExit();
	protected abstract void clear();
}

public abstract class Item<T> : Item where T : MonoBehaviour
{
	protected Delegate<T> onClickDelegate = null;
	protected Delegate<T> onMouseEnterDelegate = null;
	protected Delegate<T> onMouseExitDelegate = null;

	public void SetOnClickEvent(in Delegate<T> _onClickEvent)
	{
		onClickDelegate = _onClickEvent;
	}

	public void SetOnMouseEnterEvent(in Delegate<T> _onMouseEnterEvent)
	{
		onMouseEnterDelegate = _onMouseEnterEvent;
	}

	public void SetOnMouseExitEvent(in Delegate<T> _onMouseExitEvent)
	{
		onMouseExitDelegate = _onMouseExitEvent;
	}

	public abstract void Init(in ItemInfo<T> _container);
}

public class ItemInfo<T> { }
