using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Cinemachine;
using Timers;

public class Phase4 : MonoBehaviour
{
    public GameObject bag, slimeFull, slimeHalfed, slimeHalf1, slimeHalf2, doll1, doll2;
    public Transform bagPulledTran, slimeHalfTran1, slimeHalfTran2, bagOpeningTran, dollInBagTran, dollPullOutTran, bagSetAsideTran;
    public CinemachineVirtualCamera vCamStart, vCamBagOpening, vCamDollZoomIn;
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
            if(!dollPulledOut && bagOpened) {
                PullOutDoll();
            }
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
        }
    }


    public void PullOutBag() {
        //slimeFull.SetActive(false);
        //slimeHalfed.SetActive(true);
        bag.transform.parent = null;
        //slimeHalf1.transform.parent = null;
        //slimeHalf2.transform.parent = null;
        bag.transform.DOMove(bagPulledTran.position, 0.75f);
        bag.transform.DORotate(bagPulledTran.eulerAngles, 0.75f);
        /*
        slimeHalf1.transform.DOMove(slimeHalfTran1.position, 0.75f);
        slimeHalf1.transform.DORotate(slimeHalfTran1.eulerAngles, 0.75f);
        slimeHalf2.transform.DOMove(slimeHalfTran2.position, 0.75f);
        slimeHalf2.transform.DORotate(slimeHalfTran2.eulerAngles, 0.75f).OnComplete(PositionBagForOpening);
        */
    }
    public void PositionBagForOpening() {
        SwitchCamsForBagOpening();
        bag.transform.DOMove(bagOpeningTran.position, 0.75f);
        bag.transform.DORotate(bagOpeningTran.eulerAngles, 0.75f).OnComplete(EnableBagOpening);
    }
    public void SwitchCamsForBagOpening() {
        vCamStart.Priority = 10;
        vCamBagOpening.Priority = 20;
    }
    public void EnableBagOpening() {
        bagCanBeOpened = true;
        //doll.transform.position = dollInBagTran.position;
        //doll.transform.rotation = dollInBagTran.rotation;
    }
    public void OpenBag() {
        bag.GetComponent<Animator>().Play("OpenBag");
        bagOpened = true;
    }
    public void PullOutDoll() {
        doll1.transform.parent = null;
        dollPulledOut = true;
        doll1.transform.DOMove(dollPullOutTran.position, 0.5f).OnComplete(BagSetAside);
    }
    public void BagSetAside() {
        SwitchCamsToDollZoomIn();
        bag.transform.DOMove(bagSetAsideTran.position, 1.5f).OnComplete(ActivatePhase5);
    }
    public void SwitchCamsToDollZoomIn() {
        vCamBagOpening.Priority = 10;
        vCamDollZoomIn.Priority = 20;
    }
    public void ActivatePhase5() {
        //doll2.transform.position = dollPullOutTran.position;
        //doll1.SetActive(false);
        gameObject.SetActive(false);
        phaseToActivate.SetActive(true);
    }
}
