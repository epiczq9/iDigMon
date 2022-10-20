using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Timers;
using Cinemachine;

public class Phase0 : MonoBehaviour
{
    public GameObject plushie, handFeet, ears;
    public Transform monPos, boxPos;
    public GameObject door;
    public GameObject box;

    public GameObject cinemachineMNGR;
    public CinemachineVirtualCamera vcam0, vcam1, vcam2;

    public GameObject pullMonButton;

    public GameObject phaseToActivate;

    public SwipeDetector swipeDet;

    void Start() {
        vcam0.Priority = 10;
        vcam1.Priority = 20;
    }

    // Update is called once per frame
    void Update() {

    }

    void PlayOpeningAnimation() {
        Sequence animSequence = DOTween.Sequence();
        animSequence.Append(door.transform.DORotate(new Vector3(-90, -30, 0), 1.5f, RotateMode.Fast));
        animSequence.Append(plushie.transform.DOMove(monPos.position, 1.5f));
        animSequence.Append(box.transform.DOMove(boxPos.position, 1f));
    }

    public void OpenBox() {
        door.transform.DORotate(new Vector3(-90, -30, 0), 1.5f, RotateMode.Fast).OnComplete(ActivatePullMonButton);
    }
    public void CloseBox() {
        door.transform.DORotate(new Vector3(-90, 0, 90), 2f, RotateMode.Fast);
    }
    public void TakeOutPlushie() {
        plushie.transform.DOMove(monPos.position, 1.5f).OnComplete(MoveBox);
    }
    public void Unfold() {
        handFeet.GetComponent<BlendControl>().doMaxBlend = true;
        ears.GetComponent<BlendControl>().doMaxBlend = true;
    }
    public void MoveBox() {
        box.transform.DOMove(boxPos.position, 1f).OnComplete(ActivatePhase1);
        SwitchCams();
    }
    public void SwitchCams() {
        Debug.Log("SWITCH");
        cinemachineMNGR.GetComponent<CinemachineManager>().SwitchCameras(vcam1, vcam2);
    }
    /*
    public void PlushieOpenMouth() {
        plushie.GetComponent<Plushie>().openMouthBool = true;
        slimeBox.transform.parent = null;
    }
    
    public void TakeOutSlimeBox() {
        slimeBox.transform.DOMove(slimeBoxOutPos.position, 1f).OnComplete(StraightSlimeBox);
    }

    public void StraightSlimeBox() {
        slimeBox.transform.DOMove(slimeBoxStraightPos.position, 1f);
        slimeBox.transform.DORotate(slimeBoxStraightPos.eulerAngles, 1f).OnComplete(StartPhase2);
        plushie.transform.DOMove(monSetAsidePos.position, 1f);
    }
    */


    void ActivatePullMonButton() {
        pullMonButton.SetActive(true);
    }

    void ActivatePhase1() {
        gameObject.SetActive(false);
        phaseToActivate.SetActive(true);
    }
}
