using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public List<GameObject> BarsList;

    public List<GameObject> RoomsList;

    public List<GameObject> RoomCanvasList;

    public int CurrentRoomPosition { get; set; }

    public GameObject BallGameObject;

    public List<GameObject> FoodTableGroups;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}