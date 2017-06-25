using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MobileInputSwipeUI))]
public class UseToilet : MonoBehaviour
{

	public int ToiletToRaise;
	private MobileInputSwipeUI _swipeObject;

	private SpriteRenderer _spriteRenderer;

	private Statistics _dragonStatistics;

	private bool _coroutineStarted;
	// Use this for initialization
	void Start ()
	{
		var dragon = GameObject.Find("Dragon");
		_spriteRenderer = dragon.GetComponent<SpriteRenderer>();
		_swipeObject = GetComponent<MobileInputSwipeUI>();
		_dragonStatistics = dragon.GetComponent<Statistics>();
	}
	
	// Update is called once per frame
	void Update () {
		if (_swipeObject.IsOnRightSide() && !_coroutineStarted)
		{
			StartCoroutine(ToiletCoroutine());
			_coroutineStarted = true;
		}
		if (_swipeObject.IsOnLeftSide() && _coroutineStarted)
		{
			_coroutineStarted = false;
		}
	}

	private IEnumerator ToiletCoroutine()
	{
		_swipeObject.enabled = false;
		while (_spriteRenderer.color.a > 0)
		{
			Color color = _spriteRenderer.color;
			_spriteRenderer.color = new Color(color.r, color.g, color.b, color.a - 0.01f);
			yield return new WaitForSeconds(0.01f);
		}
		StartCoroutine(RaiseToilet());
		yield return new WaitForSeconds(3f);
		while (_spriteRenderer.color.a < 1)
		{
			Color color = _spriteRenderer.color;
			_spriteRenderer.color = new Color(color.r, color.g, color.b, color.a + 0.01f);
			yield return new WaitForSeconds(0.01f);
		}
		_swipeObject.enabled = true;
	}

	private IEnumerator RaiseToilet()
	{
		for (int i = 0; i < ToiletToRaise; i++)
		{
			_dragonStatistics.RaiseToiletBy(1);
			yield return new WaitForSeconds(0.01f);
		}
	}
}
