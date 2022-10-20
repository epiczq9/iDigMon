using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class Phase7 : MonoBehaviour
{
    public GameObject doll;
    public Transform dollOnShelf;
    public CinemachineVirtualCamera vCamStart, vCamZoomOut, vCamShelf;

    private void Start() {
        TwistDoll();
    }

    public void TwistDoll() {
        Sequence twistSequence = DOTween.Sequence();
        twistSequence.Append(doll.transform.DORotate(new Vector3(275.248901f, 178.507416f, 266.57f), 1f).SetEase(Ease.InQuint));
        twistSequence.Append(doll.transform.DORotate(new Vector3(275.248901f, 178.507416f, 446.57f), 0.75f).SetEase(Ease.OutQuint));
    }
}
