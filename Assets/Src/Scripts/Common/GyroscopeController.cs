using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GyroscopeController
{
    public static bool EnableGyroscope()
    {
        if (!SystemInfo.supportsGyroscope) return false;
        Gyroscope gyroscope = Input.gyro;
        return gyroscope.enabled = true;
    }
}
