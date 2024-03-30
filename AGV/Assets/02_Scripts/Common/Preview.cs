using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Preview<T> : MonoBehaviour where T : Item<T>
{
	[SerializeField] protected Material m_CanMaterial = null;
	[SerializeField] protected Material m_CantMaterial = null;

	protected Manager<T> m_Creater = null;
	protected Renderer m_Renderer = null;

	public Vector3 Pos => this.transform.position;

	protected virtual void Awake()
	{
		m_Renderer = GetComponentInChildren<Renderer>();
	}

	protected virtual void OnEnable()
	{
		clear();
	}

	protected virtual void Update()
	{
		if (GameManager.GameMode != m_Creater.CreateMode)
		{
			this.gameObject.SetActive(false);
		}

		Vector3 createVec = Input.mousePosition;
		createVec.z = Camera.main.transform.position.y;
		this.transform.position = Camera.main.ScreenToWorldPoint(createVec);

		if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
		{
			m_Creater.Create(this.transform.position);
		}
	}

	public void Init(in Manager<T> _creater)
	{
		m_Creater = _creater;
	}
	public void SetActive(in bool _isActive)
	{
		this.gameObject.SetActive(_isActive);
	}

	protected virtual void clear()
	{
		m_Renderer.material = m_CantMaterial;
	}

	public abstract bool CanMake();
}
