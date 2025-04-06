using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Preview : MonoBehaviour
{
	[SerializeField] protected Material m_CanMaterial = null;
	[SerializeField] protected Material m_CantMaterial = null;
	
	public Vector3 Pos => this.transform.position;
	protected bool m_IsCreateWithClick = false;
	protected bool canMakeWithClick => m_IsCreateWithClick && Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && canMake();

	protected bool m_IsMoveToMouse = true;

	protected Renderer m_Renderer = null;

	protected virtual void Awake()
	{
		m_Renderer = GetComponentInChildren<Renderer>();
	}

	protected virtual void OnEnable()
	{
		clear();
	}

	public void SetActive(in bool _isActive)
	{
		this.gameObject.SetActive(_isActive);
	}

	protected virtual void clear()
	{
		m_Renderer.material = m_CantMaterial;
	}

	


	protected virtual void init() { }
	protected abstract void clickAction(Vector3 _pos);
	protected abstract bool canMake();
}

public abstract class Preview<T> : Preview where T : Item<T>
{
	protected Manager<T> m_Manager = null;
	
	protected virtual void Update()
	{
		if (GameManager.GameMode != m_Manager.CreateMode)
		{
			this.gameObject.SetActive(false);
		}

		if(m_IsMoveToMouse)
		{
			Vector3 createVec = Input.mousePosition;
			createVec.z = Camera.main.transform.position.y;
			this.transform.position = Camera.main.ScreenToWorldPoint(createVec);
		}

		if (canMakeWithClick)
		{
			clickAction(this.transform.position);
		}
	}

	public void Init(in Manager<T> _manager)
	{
		m_Manager = _manager;
		init();
	}
}
