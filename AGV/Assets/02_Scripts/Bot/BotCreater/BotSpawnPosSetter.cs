using Delegates;
using UnityEngine;
using UnityEngine.UI;

public class BotSpawnPosSetter : BotCreaterWindow
{
	private Delegate<Flag> applyDelegate = null;
	private Delegate<Delegate<Flag>> setOnMouseEnterEventDelegate = null;
	private Delegate<Delegate<Flag>> setOnMouseExitEventDelegate = null;

	[SerializeField] private Button m_BackButton = null;

	[SerializeField] private GameObject m_BotPreviewPrefab = null;
	private BotPreview m_Preview = null;

	protected override void OnEnable()
	{
		base.OnEnable();

		m_Preview?.SetActive(true);

		// 마우스 이벤트 함수 등록
		setOnMouseEnterEventDelegate?.Invoke(OnMouseEnterEvent);
		setOnMouseExitEventDelegate?.Invoke(OnMouseExitEvent);
	}

	protected override void OnDisable()
	{
		base.OnDisable();

		// 마우스 이벤트 함수 등록 해제
		setOnMouseEnterEventDelegate?.Invoke(null);
		setOnMouseExitEventDelegate?.Invoke(null);
	}
	public void SetPreview(Manager<Bot> _creater)
	{
		m_Preview.Init(_creater);
	}

	public override void Init()
	{
		m_Preview = Instantiate(m_BotPreviewPrefab, this.transform).GetComponent<BotPreview>();
		clear();
	}

	protected override void setDelegate(in CreaterWindowDelegates<Bot> _delegates)
	{
		var delegates = _delegates as BotSpawnPosSetterDelegates;

		applyDelegate = delegates.ApplyDelegate;
		setOnMouseEnterEventDelegate = delegates.SetOnMouseEnterEventDelegate;
		setOnMouseExitEventDelegate = delegates.SetOnMouseExitEventDelegate;
	}


	protected override void clear()
	{
		m_Preview?.SetActive(false);
	}

	// 플래그의 마우스Enter 이벤트에 프리뷰 함수 등록
	private void OnMouseEnterEvent(in Flag _flag)
	{
		m_Preview?.OnFlagEnterEvent(_flag);
	}

	private void OnMouseExitEvent(in Flag _flag)
	{
		m_Preview?.OnFlagExitEvent(_flag);
	}

	protected override void setButtonEvent()
	{
		// TODO..
		m_BackButton.onClick.AddListener(() => { });
	}

	protected override void setFlagsOnClickEvent(in Flag _flag)
	{
		if (_flag.IsSpawnFlag) return;

		applyDelegate?.Invoke(_flag);
		clear();
	}

	
}

public class BotSpawnPosSetterDelegates : CreaterWindowDelegates<Bot>
{
	public BotSpawnPosSetterDelegates(in Delegate<Delegate<Flag>> _setModeCallback, 
									  in Delegate<Flag> _applyCallback, in Delegate<Delegate<Flag>> _setOnMouseEnterEventCallback,
									  in Delegate<Delegate<Flag>> _setOnMouseExitEventCallback) 
									: base(_setModeCallback)
	{
		ApplyDelegate = _applyCallback;
		SetOnMouseEnterEventDelegate = _setOnMouseEnterEventCallback;
		SetOnMouseExitEventDelegate = _setOnMouseExitEventCallback;
	}

	public Delegate<Flag> ApplyDelegate { get; }
	public Delegate<Delegate<Flag>> SetOnMouseEnterEventDelegate { get; }
	public Delegate<Delegate<Flag>> SetOnMouseExitEventDelegate { get; }

}
