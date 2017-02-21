using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Room : MonoBehaviour {
    private Vector3 _position;

    private Camera _camera;

    private int _listPosition;

    private ObjectManager _objectManager;

    private float availableErrorDist = 1f;

    // Use this for initialization
    void Start() {
        _position = GetComponent<Transform>().position;
        _camera = Camera.main;
        _objectManager = GameObject.Find("ObjectManager").GetComponent<ObjectManager>();
        _listPosition = _objectManager.RoomsList.IndexOf(gameObject);
    }

    // Update is called once per frame
    void Update() { }

    public void SetThisRoom() {
        Debug.Log("Setting room " + name + " on position " + _listPosition);
        _objectManager.RoomsList.ToList().ForEach(x => x.GetComponent<Room>().StopSwitching());
        StartCoroutine("SwitchingRoomAnimation");
    }

    public IEnumerator SwitchingRoomAnimation() {
        float dist = _camera.transform.position.x - _position.x;
        Debug.Log("Distance: " + dist);
        while (Math.Abs(dist) > availableErrorDist) {
            dist = _camera.transform.position.x - _position.x;
            Vector3 newVector = CalculateNewVector(dist);
            _camera.transform.position = newVector;
            yield return new WaitForSeconds(0.005f);
        }
        //Correction
        _camera.transform.position = new Vector3(_position.x, _camera.transform.position.y, _camera.transform.position.z);
    }

    public void StopSwitching() {
        StopCoroutine("SwitchingRoomAnimation");
    }

    private Vector3 CalculateNewVector(float dist) {
        return new Vector3(_camera.transform.position.x - availableErrorDist * (dist / Math.Abs(dist)),
            _camera.transform.position.y, _camera.transform.position.z);
    }

}