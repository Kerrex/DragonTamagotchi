using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DragonAnimationManager : MonoBehaviour
{

	private const string IsEatingAnimationParam = Eatable.IsEatingAnimationParam;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void DisableIsEatingAnimation()
	{
		GetComponent<Animator>().SetBool(IsEatingAnimationParam, false);
	}
}
