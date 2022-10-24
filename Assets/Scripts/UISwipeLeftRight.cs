using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UISwipeLeftRight : MonoBehaviour
{
    public float origXPos;
    public float xMovement;
    void Start() {
        origXPos = transform.position.x;
        xMovement = transform.position.x - Screen.width / 3f;
        Sequence lrSequence = DOTween.Sequence();
        lrSequence.Append(transform.DOMoveX(xMovement, 0.5f).SetEase(Ease.Linear));
        lrSequence.Append(transform.DOMoveX(origXPos, 0.5f).SetEase(Ease.Linear));
        lrSequence.SetLoops(-1);
    }

    void Update() {
        xMovement = transform.position.x - Screen.width / 3f;
    }
}
