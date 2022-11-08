using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Timers;

public class PeelPiece : MonoBehaviour
{
    void Start() {
        TimersManager.SetTimer(this, 0.5f, MovePeelPiece);
    }
    void MovePeelPiece() {
        transform.parent = null;
        transform.DOMoveY(3.281f, 2f).OnComplete(DestroyPiece);
    }
    void DestroyPiece() {
        Destroy(gameObject);
    }
}
