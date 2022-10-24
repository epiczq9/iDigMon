using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Timers;

public class UISwipeUp : MonoBehaviour
{
    public float yMovement;
    void Start() {
        yMovement = Screen.height / 3.8f;
        transform.DOMoveY(yMovement, 1f).SetLoops(-1, LoopType.Restart);
    }

    void Update() {
        yMovement = Screen.height / 3.105f;
    }
}
