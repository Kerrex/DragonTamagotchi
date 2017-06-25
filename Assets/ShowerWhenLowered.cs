using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShowerSwiping))]
public class ShowerWhenLowered : MonoBehaviour
{

	private ShowerSwiping _swipeObject;
	private bool _isShowering = false;
	private Statistics _dragonStatistics;
	// Use this for initialization
	void Start ()
	{
		_swipeObject = GetComponent<ShowerSwiping>();
		_dragonStatistics = GameObject.Find("Dragon").GetComponent<Statistics>();
	}
	
	// Update is called once per frame
	void Update () {
		if (IsShowerMoving() && !_isShowering)
		{
			_isShowering = true;
			StartCoroutine("IncreaseToilet");
		}
		if (!IsShowerMoving() && _isShowering)
		{
			_isShowering = false;
		}
	}

	private IEnumerator IncreaseToilet()
	{
		while (_isShowering)
		{
			_dragonStatistics.RaiseToiletBy(1);
			yield return new WaitForSeconds(0.01f);
		}
	}

	private bool IsShowerMoving()
	{
		return _swipeObject.NotInInitialPosition();
	}
}
