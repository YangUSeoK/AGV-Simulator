using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Delegates;


// 경로 밖의 플래그를 선택하면 에러 띄우기

public class BotLocationSetter : BotCreaterWindow
{
	private Delegate<Flag, Flag> applyDelegate = null;
	private Delegate<Flag> onClickDelegate = null;
	private BoolDelegate<Flag> isFlagInPath = null;

	[SerializeField] private TMP_Text m_LoadText = null;
	[SerializeField] private TMP_Text m_UnloadText = null;
	[SerializeField] private Button m_SetLoadPlaceButton = null;
	[SerializeField] private Button m_SetUnloadPlaceButton = null;
	[SerializeField] private Button m_ApplyButton = null;
	[SerializeField] private Button m_BackButton = null;

	private Flag m_LoadFlag = null;
	private Flag m_UnloadFlag = null;

	public override void Init()
	{
		clear();
	}

	protected override void setDelegate(in CreaterWindowDelegates<Bot> _delegates)
	{
		var delegates = _delegates as BotLocationSetterDelegates;
		applyDelegate = delegates.ApplyDelegate;
		isFlagInPath = delegates.IsFlagInPath;
	}

	protected override void setButtonEvent()
	{
		m_ApplyButton.onClick.AddListener(() =>
		{
			applyDelegate?.Invoke(m_LoadFlag, m_UnloadFlag);
			clear();
		});
		m_SetLoadPlaceButton.onClick.AddListener(() => onClickDelegate = setLoadPlace);
		m_SetUnloadPlaceButton.onClick.AddListener(() => onClickDelegate = setUnloadPlace);

		// TODO..
		m_BackButton.onClick.AddListener(() => { });
	}

	protected override void setFlagsOnClickEvent(in Flag _plag)
	{
		onClickDelegate?.Invoke(_plag);
	}

	protected override void clear()
	{
		clearLoadFlag();
		clearUnloadFlag();
		onClickDelegate = setLoadPlace;
	}

	// 플래그를 클릭하면 실행될 함수
	private void setLoadPlace(in Flag _loadFlag)
	{
		clearLoadFlag();
		if (!canSetFlag(_loadFlag)) return;

		setLoadFlag(_loadFlag);
		onClickDelegate = setUnloadPlace;
	}

	private void setUnloadPlace(in Flag _unloadFlag)
	{
		clearUnloadFlag();
		if (!canSetFlag(_unloadFlag)) return;

		setUnloadFlag(_unloadFlag);
		onClickDelegate = setLoadPlace;
	}

	private bool canSetFlag(in Flag _flag)
	{
		return isFlagInPath(_flag) && (m_UnloadFlag != _flag);
	}

	private void clearLoadFlag()
	{
		clearFlag(ref m_LoadFlag);
		m_LoadText.text = $"Load : ";
	}

	private void clearUnloadFlag()
	{
		clearFlag(ref m_UnloadFlag);
		m_UnloadText.text = $"Unload : ";
	}

	private void clearFlag(ref Flag _flag)
	{
		_flag?.Selected(false);
		_flag = null;
	}

	private void setLoadFlag(in Flag _flag)
	{
		m_LoadFlag = _flag;
		_flag.Selected(true);
		m_LoadText.text = $"Load : {_flag.name}";
	}

	private void setUnloadFlag(in Flag _flag)
	{
		m_UnloadFlag = _flag;
		_flag.Selected(true);
		m_UnloadText.text = $"Unload : {_flag.name}";
	}
}

public class BotLocationSetterDelegates : BotCreaterWindowDelegates
{
	public BotLocationSetterDelegates(in Delegate<Delegate<Flag>> _setModeCallback, 
									  in Delegate<Flag, Flag> _applyLoadAndUnloadPlaceCallback,
									  in BoolDelegate<Flag> _checkFlagInPathFunc) 
									: base(_setModeCallback)
	{
		ApplyDelegate = _applyLoadAndUnloadPlaceCallback;
		IsFlagInPath = _checkFlagInPathFunc;
	}

	public Delegate<Flag, Flag> ApplyDelegate { get; }
	public BoolDelegate<Flag> IsFlagInPath { get; }
}