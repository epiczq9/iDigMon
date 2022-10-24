using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UISwipeDown : MonoBehaviour
{
    public float yMovement;
    void Start() {
        yMovement = transform.position.y - Screen.height / 4.5f;
        transform.DOMoveY(yMovement, 1f).SetLoops(-1, LoopType.Restart);
    }

    void Update() {
        yMovement = transform.position.y - Screen.height / 4.5f;
    }
}
