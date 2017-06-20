using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FoodDropArea : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool IsInDropArea { get; set; }
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        IsInDropArea = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IsInDropArea = false;
    }
}