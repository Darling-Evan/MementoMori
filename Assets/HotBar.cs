using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HotBar : MonoBehaviour
{
	public static HotBar Instance { get; private set; }

	private void Awake()
	{
		Instance = this;
	}
}
