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

            if (!bagOpened && bagCanBeOpened) {
                OpenBag();
            }
            swipeDet.outputText.text = "EMPTY";
        } else if (swipeDet.outputText.text == "DOWN" || Input.GetKeyDown(KeyCode.DownArrow)) {
            
            swipeDet.outputText.text = "EMPTY";
        }
    }

    public void MeltTheSlime1() {
        if(swipeCounter1 == 0) {
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
        //slimeFull.SetActive(false);
        //slimeHalfed.SetActive(true);
        doll.transform.parent = null;
        //slimeHalf1.transform.parent = null;
        //slimeHalf2.transform.parent = null;
        SwitchCamsForDollPulling();
        doll.transform.DOMove(dollPulledTran.position, 0.75f);
        doll.transform.DORotate(dollPulledTran.eulerAngles, 0.75f).OnComplete(SetAsideSlime);
        
        /*
        slimeHalf1.transform.DOMove(slimeHalfTran1.position, 0.75f);
        slimeHalf1.transform.DORotate(slimeHalfTran1.eulerAngles, 0.75f);
        slimeHalf2.transform.DOMove(slimeHalfTran2.position, 0.75f);
        slimeHalf2.transform.DORotate(slimeHalfTran2.eulerAngles, 0.75f).OnComplete(PositionBagForOpening);
        */
    }
    public void SetAsideSlime() {
        slimeHalfed.transform.DOMove(slimeSetAsideTran.position, 0.75f);
    }
    /*
    public void PositionBagForOpening() {
        SwitchCamsForBagOpening();
        doll.transform.DOMove(bagOpeningTran.position, 0.75f);
        doll.transform.DORotate(bagOpeningTran.eulerAngles, 0.75f).OnComplete(EnableBagOpening);
    }
    */
    public void SwitchCamsForDollPulling() {
        vCamStart.Priority = 10;
        vCamDollPulling.Priority = 20;
    }
    public void EnableBagOpening() {
        bagCanBeOpened = true;
        uiSwipeRight.SetActive(true);
        //doll.transform.position = dollInBagTran.position;
        //doll.transform.rotation = dollInBagTran.rotation;
    }
    public void OpenBag() {
        doll.GetComponent<Animator>().Play("OpenBag");
        TimersManager.SetTimer(this, 0.5f, EnableDollTakeOut);
    }
    public void EnableDollTakeOut() {
        bagOpened = true;
        uiSwipeUp.SetActive(true);
    }
    public void BagSetAside() {
        SwitchCamsToDollZoomIn();
        //bag.transform.DOMove(bagSetAsideTran.position, 0.75f).OnComplete(ActivatePhase5);
    }
    public void SwitchCamsToDollZoomIn() {
        vCamDollPulling.Priority = 10;
        vCamDollZoomIn.Priority = 20;
    }
    public void ActivatePhase5() {
        //doll2.transform.position = dollPullOutTran.position;
        //doll1.SetActive(false);
        gameObject.SetActive(false);
        phaseToActivate.SetActive(true);
    }
    
}
