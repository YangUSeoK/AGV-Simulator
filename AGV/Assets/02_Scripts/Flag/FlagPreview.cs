using UnityEngine;

public class FlagPreview : Preview<Flag>
{
	protected override bool canMake()
	{
		return true;
	}

	protected override void clickAction(Vector3 _pos)
	{
		m_Manager.Create(this.transform.position);
	}

	protected override void init()
	{
		m_IsCreateWithClick = true;
	}
}
