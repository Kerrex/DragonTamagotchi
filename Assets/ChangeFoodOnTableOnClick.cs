using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFoodOnTableOnClick : MonoBehaviour {
    
    public GameObject tableFood;
    public GameObject fridgeCover;
    public GameObject tableUIObject;
    private ToggleXPosition tableToogleScript;
    private ObjectManager objectManager;
    private bool isChangingFood = false;
	// Use this for initialization
	void Start () {
        tableToogleScript = GameObject.Find("Canvas/RoomCanvases/KitchenCanvas/Table").GetComponent<ToggleXPosition>();
        objectManager = GameObject.Find("ObjectManager").GetComponent<ObjectManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown() {
        if (FridgeCoverIsOpen() && TableIsOpened())
        {
            StartCoroutine(SwitchFood());
        }
    }

    private bool TableIsOpened()
    {
        MobileInputSwipeUI tableSwipe = tableUIObject.GetComponent<MobileInputSwipeUI>();
        return tableSwipe.IsOnRightSide();
    }

    private bool FridgeCoverIsOpen()
    {
        MobileInputSwipeWorldObject fridgeSwipe = fridgeCover.GetComponent<MobileInputSwipeWorldObject>();
        return fridgeSwipe.IsOnLeftSide();
    }

    private IEnumerator SwitchFood() {
        isChangingFood = true;
        if (!tableToogleScript.IsPositionAtX1()) {
            tableToogleScript.StartToggling();
        }
        while (tableToogleScript.isMoving) yield return new WaitForSeconds(0.2f);
        DeactivateCurrentTables();
        ActivateThisTable();
        tableToogleScript.StartToggling();
        while (tableToogleScript.isMoving) yield return new WaitForSeconds(0.2f);
        isChangingFood = false;
    }

    private void ActivateThisTable()
    {
        tableFood.SetActive(true);
    }

    private void DeactivateCurrentTables()
    {
        objectManager.FoodTableGroups.ForEach(group => group.SetActive(false));
    }
}
