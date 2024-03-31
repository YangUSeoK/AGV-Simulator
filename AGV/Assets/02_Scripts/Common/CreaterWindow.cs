using Delegates;
using UnityEngine;

public abstract class CreaterWindow : MonoBehaviour
{
	protected Delegate<Delegate<Flag>> setModeDelegate = null;

	protected virtual void Awake()
	{
		setButtonEvent();
	}

	protected virtual void OnEnable()
	{
		clear();
		setModeDelegate?.Invoke(flagsOnClickEvent);	// �÷��׸� Ŭ���ϸ� ����� �Լ��� ����
	}

	protected virtual void OnDisable()
	{
		clear();
	}

	public void SetActive(in bool _isActive)
	{
		this.gameObject.SetActive(_isActive);
	}

	public abstract void Init();
	
	protected abstract void clear();
	protected abstract void flagsOnClickEvent(in Flag _flag); // �÷��׸� Ŭ���ϸ� ����� �Լ�
	protected abstract void setButtonEvent();
}

public abstract class CreaterWindow<T> : CreaterWindow where T : Item<T>
{
	protected T m_Item = null;
	public T Item { set{ m_Item = value;} }

	public virtual void SetDelegate(in CreaterWindowDelegates<T> _delegates)
	{
		setModeDelegate = _delegates.setModeDelegate;

		setDelegate(_delegates);
	}

	protected abstract void setDelegate(in CreaterWindowDelegates<T> _delegates);
}

public class CreaterWindowDelegates<T>
{
	public CreaterWindowDelegates(in Delegate<Delegate<Flag>> _setModeCallback)
	{
		setModeDelegate = _setModeCallback;
	}

	public Delegate<Delegate<Flag>> setModeDelegate { get; }
}