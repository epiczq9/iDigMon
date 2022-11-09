using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Cinemachine;
using Timers;

public class Phase4Dragon : MonoBehaviour
{
    public GameObject doll, slimeFull, slimeHalfed, slimeHalf1, slimeHalf2, dollPullOutButton;
    public GameObject uiLRUpper;
    public GameObject uiLRLower;
    public GameObject uiSwipeRight;
    public GameObject uiSwipeUp;
    public Transform dollPulledTran, slimeSetAsideTran;
    public CinemachineVirtualCamera vCamStart, vCamDollPulling, vCamDollZoomIn;
    public int swipeCounter1 = 0, swipeCounter2 = 0;

    bool slime1CanBeMelted = true;
    bool slime2CanBeMelted = false;
    bool bagCanBeOpened = false;
    bool bagOpened = false;
    bool dollPulledOut = false;

    public SwipeDetector swipeDet;

    public GameObject phaseToActivate;

    void Update() {
        if (swipeDet.outputText.text == "UP" || Input.GetKeyDown(KeyCode.UpArrow)) {
            
            swipeDet.outputText.text = "EMPTY";
        } else if (swipeDet.outputText.text == "LEFT" || Input.GetKeyDown(KeyCode.LeftArrow)) {
            if (slime1CanBeMelted) {
                MeltTheSlime1();
            } else if (slime2CanBeMelted) {
                MeltTheSlime2();
            }
            swipeDet.outputText.text = "EMPTY";
        } else if (swipeDet.outputText.text == "RIGHT" || Input.GetKeyDown(KeyCode.RightArrow)) {
            if (slime1CanBeMelted) {
                MeltTheSlime1();
            } else if (slime2CanBeMelted) {
                MeltTheSlime2();
            }
            swipeDet.outputText.text = "EMPTY";
        } else if (swipeDet.outputText.text == "DOWN" || Input.GetKeyDown(KeyCode.DownArrow)) {
            
            swipeDet.outputText.text = "EMPTY";
        }
    }

    public void MeltTheSlime1() {
        doll.SetActive(true);
        if (swipeCounter1 == 0) {
            slimeFull.SetActive(false);
            slimeHalfed.SetActive(true);
            slimeHalf1.GetComponent<MeltSlime>().doFirstSwipe = true;
            swipeCounter1 = 1;
        } else if (swipeCounter1 == 1) {
            slimeHalf1.GetComponent<MeltSlime>().doSecondSwipe = true;

            uiLRUpper.SetActive(false);
            uiLRLower.SetActive(true);

            slime2CanBeMelted = true;
            slime1CanBeMelted = false;
        }
    }
    public void MeltTheSlime2() {
        if (swipeCounter2 == 0) {
            slimeHalf2.GetComponent<MeltSlime>().doFirstSwipe = true;
            swipeCounter2 = 1;
        } else if (swipeCounter2 == 1) {
            slimeHalf2.GetComponent<MeltSlime>().doSecondSwipe = true;
            slime2CanBeMelted = false;
            uiLRLower.SetActive(false);
            dollPullOutButton.SetActive(true);
        }
    } 
    public void PullOutDoll() {
        doll.transform.parent = null;
        SwitchCamsForDollPulling();
        doll.transform.DOMove(dollPulledTran.position, 0.75f);
        doll.transform.DORotate(dollPulledTran.eulerAngles, 0.75f).OnComplete(SetAsideSlime);
    }
    public void SwitchCamsForDollPulling() {
        vCamStart.Priority = 10;
        vCamDollPulling.Priority = 20;
    }
    public void SetAsideSlime() {
        slimeHalfed.transform.DOMove(slimeSetAsideTran.position, 0.75f).OnComplete(ActivatePhase5);
    }
    
    public void ActivatePhase5() {
        gameObject.SetActive(false);
        phaseToActivate.SetActive(true);
    }
    
}
