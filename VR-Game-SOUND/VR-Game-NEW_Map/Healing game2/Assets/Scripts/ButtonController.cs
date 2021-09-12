using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.Events;
using System;


public class ButtonController : MonoBehaviour
{
    private InputDeviceRole deviceRole;
   
    List<InputDevice> devices;
    InputFeatureUsage<bool> selectedFeature;
    Vector2 input_key;

    private InputFeatureUsage<Vector2> primary2DAxis_button;
        
    public UnityEvent OnPress;
    public UnityEvent OnPre;
    private bool isPressed;
    private void Awake()
    {
        primary2DAxis_button = CommonUsages.primary2DAxis;
        deviceRole = InputDeviceRole.LeftHanded;
        input_key = new Vector2();
        devices = new List<InputDevice>();
        
        //InputDevices.GetDevicesWithRole(deviceRole, devices);
    }
    void Update()
    {
        InputDevices.GetDevicesWithRole(deviceRole, devices);
        for (int i = 0; i < devices.Count; i++)
        {
            devices[i].TryGetFeatureValue(primary2DAxis_button, out input_key);
            if(input_key.x != 0)
            {
                Debug.Log(input_key.x.ToString());
                OnPress.Invoke();
            }
            else if(input_key.x == 0)
            {
                Debug.Log(input_key.x.ToString());
                OnPre.Invoke();
            }
        }
    }
}
