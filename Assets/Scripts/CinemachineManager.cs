using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineManager : MonoBehaviour
{
    public void SwitchCameras(CinemachineVirtualCamera vcam1, CinemachineVirtualCamera vcam2) {
        vcam1.Priority = 10;
        vcam2.Priority = 20;
    }
}
