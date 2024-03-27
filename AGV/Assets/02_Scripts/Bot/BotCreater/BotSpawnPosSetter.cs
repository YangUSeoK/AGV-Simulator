using Delegates;
using UnityEngine;
using UnityEngine.UI;

public class BotSpawnPosSetter : BotCreateSetter
{
	private Delegate<Flag> applyDelegate = null;
	private Delegate<Delegate<Flag>> setOnMouseEnterEventDelegate = null;

	[SerializeField] private Button m_BackButton = null;

	[SerializeField] private GameObject m_BotPreviewPrefab = null;
	private BotPreview m_Preview = null;

	protected override void OnEnable()
	{
		base.OnEnable();

		m_Preview?.SetActive(true);
		setOnMouseEnterEventDelegate?.Invoke(OnMouseEnterEvent);
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		setOnMouseEnterEventDelegate?.Invoke(null);
	}

	protected override void clear()
	{
		m_Preview?.SetActive(false);
	}


	// 플래그의 마우스Enter 이벤트에 프리뷰 함수 등록
	private void OnMouseEnterEvent(in Flag _plag)
	{
		m_Preview?.OnFlagEnterEvent(_plag);
	}

	public override void Init()
	{
		m_Preview = Instantiate(m_BotPreviewPrefab, this.transform).GetComponent<BotPreview>();
		clear();
	}

	public void SetCallback(Delegate<Flag> _applyCallback, Delegate<Delegate<Flag>> _setOnMouseEnterEventCallback)
	{
		setOnMouseEnterEventDelegate = _setOnMouseEnterEventCallback;
		applyDelegate = _applyCallback;
	}

	protected override void setButtonEvent()
	{
		//TODO
		m_BackButton.onClick.AddListener(() => { });
	}

	protected override void setPlagsOnClickEvent(in Flag _plag)
	{
		if (_plag.IsSpawnPlag) return;

		applyDelegate?.Invoke(_plag);
		clear();
	}

	


}
