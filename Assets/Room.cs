using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Room : MonoBehaviour {
    private Vector3 position;

    private Camera camera;

    private int listPosition;

    private ObjectManager objectManager;

    private float availableErrorDist = 1f;

    // Use this for initialization
    void Start() {
        position = GetComponent<Transform>().position;
        camera = Camera.main;
        objectManager = GameObject.Find("ObjectManager").GetComponent<ObjectManager>();
        listPosition = objectManager.RoomsList.IndexOf(gameObject);
    }

    // Update is called once per frame
    void Update() { }

    public void SetThisRoom() {
        Debug.Log("Setting room " + name + " on position " + listPosition);
        objectManager.RoomsList.ToList().ForEach(x => x.GetComponent<Room>().StopSwitching());
        StartCoroutine("SwitchingRoomAnimation");

//        float oldY = camera.transform.position.y;
//        float oldZ = camera.transform.position.z;
//        camera.transform.position = new Vector3(position.x, oldY, oldZ);
    }

    public IEnumerator SwitchingRoomAnimation() {
        float dist = camera.transform.position.x - position.x;
        Debug.Log("Distance: " + dist);
        while (Math.Abs(dist) > availableErrorDist) {
            dist = camera.transform.position.x - position.x;
            Debug.Log("Distance: " + dist);
            Vector3 newVector = calculateNewVector(dist);
            camera.transform.position = newVector;
            yield return new WaitForSeconds(0.005f);
        }
        //Correction
        camera.transform.position = new Vector3(position.x, camera.transform.position.y, camera.transform.position.z);
    }

    public void StopSwitching() {
        StopCoroutine("SwitchingRoomAnimation");
    }

    private Vector3 calculateNewVector(float dist) {
        return new Vector3(camera.transform.position.x - availableErrorDist * (dist / Math.Abs(dist)),
            camera.transform.position.y, camera.transform.position.z);
    }

}