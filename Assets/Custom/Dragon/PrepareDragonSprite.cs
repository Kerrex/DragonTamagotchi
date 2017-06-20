using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareDragonSprite : MonoBehaviour {

    private const string EGG_ID = "EggId";

    public List<Sprite> DragonSprites;
    public int CurrentDragonId { get; set; }
	// Use this for initialization
	void Start () {
	    CurrentDragonId = PlayerPrefs.GetInt(EGG_ID);
	    GameObject.Find("Dragon").GetComponent<SpriteRenderer>().sprite = DragonSprites[CurrentDragonId];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
