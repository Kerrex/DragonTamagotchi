using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BallSwitchClickArea : MonoBehaviour, IPointerClickHandler
{

    public Sprite BallSprite;

    private SpriteRenderer ballSpriteRenderer;
    private GameObject ballObject;

    // Use this for initialization
    void Start()
    {
        ballObject = GameObject.Find("ObjectManager")
                                       .GetComponent<ObjectManager>()
                                       .BallGameObject;
        ballSpriteRenderer = ballObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ballSpriteRenderer.sprite = BallSprite;
        Debug.Log("Clicked on ball shelf!");
    }
}
