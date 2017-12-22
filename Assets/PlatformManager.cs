using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public enum SystemType
    {
        XBOX,
        PS4,
        PC
    }

    public static SystemType systemType = SystemType.PC;

    void Start()
    {
#if UNITY_WSA_10_0
        Application.targetFrameRate = 30;
        systemType = SystemType.XBOX;
#elif UNITY_EDITOR
        Application.targetFrameRate = 60;
        systemType = SystemType.PC;
#elif UNITY_STANDALONE
        Application.targetFrameRate = 60;
        systemType = SystemType.PC;
#elif UNITY_XBOXONE
        Application.targetFrameRate = 30;
        systemType = SystemType.XBOX;
#elif UNITY_PS4
        Application.targetFrameRate = 30;
        systemType = SystemType.PS4;
#endif
    }
}
