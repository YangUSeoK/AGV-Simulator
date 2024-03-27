using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotPreview : Preview
{
    public void OnFlagEnterEvent(in Flag _plag)
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
}
