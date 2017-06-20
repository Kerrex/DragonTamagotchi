using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Eatable : MonoBehaviour, IPointerUpHandler
{
	public const string IsEatingAnimationParam = "IsEating";
	public FoodDropArea foodDropArea;

	public int pointsToRaise;
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Eat()
	{
		GameObject dragon = GameObject.Find("Dragon");
		dragon.GetComponent<Statistics>().RaiseHungerBy(pointsToRaise);
		dragon.GetComponent<Animator>().SetBool(IsEatingAnimationParam, true);
		gameObject.SetActive(false);
	}

	
	public void OnPointerUp(PointerEventData eventData)
	{
		if (foodDropArea.IsInDropArea) Eat();
	}
}
