using UnityEngine;

public class LocationPreview : Preview<Location>
{
	protected override bool canMake()
	{
		return true;
	}

	protected override void clickAction(Vector3 _pos)
	{
		this.transform.position = _pos;
		m_IsMoveToMouse = false;

		(m_Manager as LocationManager).CreatePreview(this, _pos);
	}

	protected override void init()
	{
		m_IsCreateWithClick = true;
	}

	protected override void clear()
	{
		base.clear();
		m_IsMoveToMouse = true;
	}
}
