using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrepareBoardHandler : MonoBehaviour {

    public List<Sprite> handleSprites;
    public GameObject handle;

	// Use this for initialization
	void Start () {
        int selectedDragon = GetComponent<PrepareDragonSprite>().CurrentDragonId;
        Debug.Log("Selected dragon: " + selectedDragon);
        handle.GetComponent<Image>().sprite = handleSprites[selectedDragon];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
