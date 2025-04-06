using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

enum Enum1
{
	enum1,
	enum2,
	enum3,

	Length,
}

enum Enum2
{
	enum1,
	enum2,
	enum3,

	Length,
}

public class Test : MonoBehaviour
{
	public List<T> GetEnumList<T>(T[] _enumArr) where T : Enum
	{
		List<T> ret = new();

		foreach(var element in _enumArr)
		{

		}
	}
}