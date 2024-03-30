
public class BotPreview : Preview<Bot>
{
	protected override void Update() { }

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

	public override bool CanMake()
	{
        return true;
	}

}
