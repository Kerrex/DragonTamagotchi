using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeImageOnCollision : MonoBehaviour
{

    public Sprite NoCollisionSprite { get; set; }
    public Sprite CollisionSprite { get; set; }
    public GameObject GroundObject;
    public bool ColidingWithGround { get; private set; }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.Equals(GroundObject))
        {
            Debug.Log("Collision image changed!");
            ColidingWithGround = true;
            GetComponent<SpriteRenderer>().sprite = CollisionSprite;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        GetComponent<SpriteRenderer>().sprite = NoCollisionSprite;
        ColidingWithGround = false;
    }
}
