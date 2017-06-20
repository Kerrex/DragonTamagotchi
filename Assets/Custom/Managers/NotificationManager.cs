using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationManager : MonoBehaviour {

    /**
    *  Notification texts
    */
    public string HungerNotificationName;
    public string HungerNotificationText;

    public string SleepNotificationName;
    public string SleepNotificationText;

    public string JoyNotificationName;
    public string JoyNotificationText;

    public string ToiletNotificationName;
    public string ToiletNotificationText;

    /**
    *  Constants
    */
    private const int SLEEP_NOTIFICATION_ID = 0;
    private const int HUNGER_NOTIFICATION_ID = 1;
    private const int JOY_NOTIFICATION_ID = 2;
    private const int TOILET_NOTIFICATION_ID = 3;
    public int NotificationDelay = 30;

    private Statistics _statistics;

    // Use this for initialization
	void Start () {
	    _statistics = GameObject.Find("Dragon").GetComponent<Statistics>();
	    //RegisterNotifications();
	}


    // Update is called once per frame
	void Update () {
		
	}

    void OnApplicationFocus(bool hasFocus) {
        if (!hasFocus) {
            RegisterNotifications();
        }
        else {
            UnregisterNotifications();
        }
    }


    private void UnregisterNotifications() {
        AndroidNotification.CancelNotification(HUNGER_NOTIFICATION_ID);
        AndroidNotification.CancelNotification(JOY_NOTIFICATION_ID);
        AndroidNotification.CancelNotification(SLEEP_NOTIFICATION_ID);
        AndroidNotification.CancelNotification(TOILET_NOTIFICATION_ID);
    }

    private void RegisterNotifications() {
        if (_statistics.CurrentHunger > NotificationDelay) {
            AndroidNotification.SendNotification(HUNGER_NOTIFICATION_ID,
                TimeSpan.FromSeconds(_statistics.CurrentHunger - NotificationDelay),
                HungerNotificationName, HungerNotificationText);
            /*AndroidNotification.SendRepeatingNotification(HUNGER_NOTIFICATION_ID,
                _statistics.CurrentHunger - NotificationDelay,
                NotificationDelay, HungerNotificationName, HungerNotificationText, Color.white);*/
        }

        if (_statistics.CurrentSleep > NotificationDelay) {
            AndroidNotification.SendNotification(SLEEP_NOTIFICATION_ID,
                TimeSpan.FromSeconds(_statistics.CurrentSleep - NotificationDelay),
                SleepNotificationName, SleepNotificationText);
            /*AndroidNotification.SendRepeatingNotification(SLEEP_NOTIFICATION_ID,
                _statistics.CurrentSleep - NotificationDelay,
                NotificationDelay, SleepNotificationName, SleepNotificationText, Color.white);*/
        }

        if (_statistics.CurrentJoy > NotificationDelay) {
            AndroidNotification.SendNotification(JOY_NOTIFICATION_ID,
                TimeSpan.FromSeconds(_statistics.CurrentJoy - NotificationDelay),
                JoyNotificationName, JoyNotificationText);
            /*AndroidNotification.SendRepeatingNotification(JOY_NOTIFICATION_ID,
                _statistics.CurrentJoy - NotificationDelay,
                NotificationDelay, JoyNotificationName, JoyNotificationText, Color.white);*/
        }

        if (_statistics.CurrentHunger > NotificationDelay) {
            AndroidNotification.SendNotification(TOILET_NOTIFICATION_ID,
                TimeSpan.FromSeconds(_statistics.CurrentJoy - NotificationDelay),
                ToiletNotificationName, ToiletNotificationText);
            /*AndroidNotification.SendRepeatingNotification(TOILET_NOTIFICATION_ID,
                _statistics.CurrentHunger - NotificationDelay,
                NotificationDelay, ToiletNotificationName, ToiletNotificationText, Color.white);*/
        }

    }

    /*private void OnApplicationQuit() {
        UnregisterNotifications();
        RegisterNotifications();
    }*/
}
