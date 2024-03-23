using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotPreview : MonoBehaviour
{
    [SerializeField] private Material m_CanMaterial = null;
    [SerializeField] private Material m_CantMaterial = null;

    private Renderer m_Renderer = null;

	private void Awake()
	{
        m_Renderer = GetComponentInChildren<Renderer>();
	}

    public void OnPlagEnterEvent(Plag _plag)
    {
        this.transform.position = _plag.Pos;

        if (_plag.IsSpawnPlag)
        {
            m_Renderer.material = m_CantMaterial;
        }
        else
        {
            m_Renderer.material = m_CanMaterial;
        }
    }

	public void SetActive(bool _isActive)
    {
        this.gameObject.SetActive(_isActive);
    }
}
