using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FlagPreview : Preview<Flag>
{
	protected override bool canMake()
	{
		return true;
	}
}
