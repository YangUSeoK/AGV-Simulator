using UnityEngine;
using Delegates;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FlagManager : MonoBehaviour
{
	[SerializeField] private GameObject m_FlagPrefab = null;
	[SerializeField] private GameObject m_FlagPreviewPrefab = null;

	[SerializeField] private Toggle m_CreateFlagModeToggle = null;

	private FlagPreview m_Preview = null;

	private readonly List<Flag> m_FlagList = new List<Flag>();
	public List<Flag> FlagList => m_FlagList;


	private void Update()
	{
		if (GameManager.GameMode != EGameMode.CreateFlag) return;

		if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
		{
			createFlag(m_Preview.Pos);
		}
	}

	public void SetFlagsOnClickEvent(in Delegate<Flag> _onClickEvent)
	{
		foreach (var flag in m_FlagList)
		{
			flag.SetOnClickEvent(_onClickEvent);
		}
	}

	public void SetPlagsOnMouseEnterEvent(in Delegate<Flag> _onMouseEnterEvent)
	{
		foreach (var flag in m_FlagList)
		{
			flag.SetOnMouseEnterEvent(_onMouseEnterEvent);
		}
	}

	public void Init()
	{
		m_Preview = Instantiate(m_FlagPreviewPrefab, this.transform).GetComponent<FlagPreview>();
		m_Preview.SetActive(false);

		setEvent();
	}

	private void setEvent()
	{
		m_CreateFlagModeToggle.onValueChanged.AddListener((_isOn) =>
		{
			if (_isOn)
			{
				if (GameManager.GameMode != EGameMode.Edit)
				{
					m_CreateFlagModeToggle.SetIsOnWithoutNotify(false);
					return;
				}
				else
				{
					startCreateFlagMode();
				}
			}
			else
			{
				finishCreateFlagMode();
			}

		});
	}

	private void startCreateFlagMode()
	{
		GameManager.GameMode = EGameMode.CreateFlag;
		m_Preview.SetActive(true);
	}

	private void finishCreateFlagMode()
	{
		GameManager.GameMode = EGameMode.Edit;
		m_Preview.SetActive(false);
	}

	private void createFlag(in Vector3 _createPos)
	{
		Flag flag = Instantiate(m_FlagPrefab, _createPos, Quaternion.identity, this.transform).GetComponent<Flag>();
		m_FlagList.Add(flag);
	}
}
