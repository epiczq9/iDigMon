using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Cinemachine;
using Timers;

public class Phase4 : MonoBehaviour
{
    public GameObject bag, slimeFull, slimeHalfed, slimeHalf1, slimeHalf2, doll;
    public Transform bagPulledTran, slimeHalfTran1, slimeHalfTran2, bagOpeningTran, dollPullOutTran, bagSetAsideTran;
    public CinemachineVirtualCamera vCamStart, vCamBagOpening, vCamDollZoomIn;

    bool bagCanBeOpened = false;
    bool bagOpened = false;
    bool dollPulledOut = false;

    public SwipeDetector swipeDet;

    void Start() {
        
    }

    
    void Update() {
        if (swipeDet.outputText.text == "UP" || (Input.GetAxis("Vertical") > 0)) {
            if(!dollPulledOut && bagOpened) {
                PullOutDoll();
            }
            swipeDet.outputText.text = "EMPTY";
        } else if (swipeDet.outputText.text == "LEFT" || (Input.GetAxis("Horizontal") < 0)) {

            swipeDet.outputText.text = "EMPTY";
        } else if (swipeDet.outputText.text == "RIGHT" || (Input.GetAxis("Horizontal") > 0)) {
            if(!bagOpened && bagCanBeOpened) {
                OpenBag();
            }
            swipeDet.outputText.text = "EMPTY";
        } else if (swipeDet.outputText.text == "DOWN" || (Input.GetAxis("Vertical") < 0)) {
            
            swipeDet.outputText.text = "EMPTY";
        }
    }

    public void PullOutBag() {
        slimeFull.SetActive(false);
        slimeHalfed.SetActive(true);
        bag.transform.parent = null;
        slimeHalf1.transform.parent = null;
        slimeHalf2.transform.parent = null;
        bag.transform.DOMove(bagPulledTran.position, 0.75f);
        bag.transform.DORotate(bagPulledTran.eulerAngles, 0.75f);
        slimeHalf1.transform.DOMove(slimeHalfTran1.position, 0.75f);
        slimeHalf1.transform.DORotate(slimeHalfTran1.eulerAngles, 0.75f);
        slimeHalf2.transform.DOMove(slimeHalfTran2.position, 0.75f);
        slimeHalf2.transform.DORotate(slimeHalfTran2.eulerAngles, 0.75f).OnComplete(PositionBagForOpening);
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
    }
    public void OpenBag() {
        bag.GetComponent<Animator>().Play("OpenBag");
        bagOpened = true;
    }
    public void PullOutDoll() {
        doll.transform.parent = null;
        dollPulledOut = true;
        doll.transform.DOMove(dollPullOutTran.position, 0.5f).OnComplete(BagSetAside);
    }
    public void BagSetAside() {
        SwitchCamsToDollZoomIn();
        bag.transform.DOMove(bagSetAsideTran.position, 1.5f);
    }
    public void SwitchCamsToDollZoomIn() {
        vCamBagOpening.Priority = 10;
        vCamDollZoomIn.Priority = 20;
    }
}
