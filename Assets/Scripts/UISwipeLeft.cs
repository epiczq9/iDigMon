using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UISwipeLeft : MonoBehaviour
{
    public float xMovement;
    void Start() {
        xMovement = transform.position.x - Screen.width / 3f;
        transform.DOMoveX(xMovement, 1f).SetLoops(-1, LoopType.Restart);
    }

    void Update() {
        xMovement = transform.position.x - Screen.width / 3f;
    }
}
