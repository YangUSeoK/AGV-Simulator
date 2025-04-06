using UnityEngine;

public class BotPreview : Preview<Bot>
{
	protected override void Update()
	{
		if (GameManager.GameMode != m_Manager.CreateMode)
		{
			this.gameObject.SetActive(false);
		}

		Vector3 createVec = Input.mousePosition;
		createVec.z = Camera.main.transform.position.y;
		this.transform.position = Camera.main.ScreenToWorldPoint(createVec);
	}

	public void OnFlagEnterEvent(in Flag _flag)
	{
		this.transform.position = _flag.Pos;

		if (_flag.IsSpawnFlag)
		{
			m_Renderer.material = m_CantMaterial;
		}
		else
		{
			m_Renderer.material = m_CanMaterial;
		}
	}

	public void OnFlagExitEvent(in Flag _flag)
	{
		m_Renderer.material = m_CantMaterial;
	}

	protected override bool canMake()
	{
		return true;
	}

	protected override void clickAction(Vector3 _pos)
	{
	}
}
