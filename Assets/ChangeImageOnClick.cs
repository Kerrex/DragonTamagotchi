using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

[RequireComponent(typeof(Image))]
public class ChangeImageOnClick : Button
{

    public Sprite afterClickSprite;
    private Sprite beforeClickSprite;
    private Image imageRenderer;
    private Vector3 initialPosition;
    private bool _isSwiping;
    private Button button;

    // Use this for initialization

    // Update is called once per frame
    void Update()
    {
        if (_isSwiping)
        {
            transform.position = Input.mousePosition;
        }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        
        Debug.Log("Clicked " + gameObject.name);
        _isSwiping = true;
        initialPosition = transform.position;
        SetActiveSwipeableComponents(false);
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData) {
        _isSwiping = false;
        transform.position = initialPosition;
        SetActiveSwipeableComponents(true);
        base.OnPointerUp(eventData);
    }

    private void SetActiveSwipeableComponents(bool enable)
    {
        GameObject.Find("Table").GetComponent<MobileInputSwipeUI>().enabled = enable;
        GameObject.Find("FridgeCover").GetComponent<MobileInputSwipeWorldObject>().enabled = enable;
    }
}
