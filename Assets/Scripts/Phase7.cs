using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using DG.Tweening;

public class Phase7 : MonoBehaviour
{
    public GameObject doll;
    [Header("Transforms")]
    public Transform dollOverShelf;
    public Transform dollOnShelf;

    [Header("VCams")]
    public CinemachineVirtualCamera vCamStart;
    public CinemachineVirtualCamera vCamShelf;
    public CinemachineVirtualCamera vCamZoomOut;

    private void Start() {
        TwistDoll();
    }

    public void TwistDoll() {
        Sequence twistSequence = DOTween.Sequence();
        twistSequence.Append(doll.transform.DORotate(new Vector3(270, -360, 0), 1f).SetEase(Ease.InQuint));
        twistSequence.Append(doll.transform.DORotate(new Vector3(270, -180, 0), 0.75f).SetEase(Ease.OutQuint)).OnComplete(PutOverShelf);
    }

    public void PutOverShelf() {
        SwitchCamsToShelf();
        doll.transform.DOMove(dollOverShelf.position, 2f).OnComplete(PutOnShelf);
        doll.transform.DORotate(dollOverShelf.eulerAngles, 2f);
        doll.transform.DOScale(dollOverShelf.localScale, 2f);
    }
    public void SwitchCamsToShelf() {
        vCamStart.Priority = 10;
        vCamShelf.Priority = 20;
    }
    public void PutOnShelf() {
        doll.transform.DOMove(dollOnShelf.position, 0.4f).OnComplete(SwitchCamsToZoomOut);
    }

    public void SwitchCamsToZoomOut() {
        vCamShelf.Priority = 10;
        vCamZoomOut.Priority = 20;
    }

    public void Reload() {
        SceneManager.LoadScene(0);
    }
}
