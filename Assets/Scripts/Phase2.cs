using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using Timers;

public class Phase2 : MonoBehaviour
{
    public GameObject slimeBox, slimeLid, tools, slimeContainer, slimes, slimeTape;
    public GameObject uiUp, uiLeft, uiDown, uiTap;
    public Transform lidRaisedPos, lidSetAsidePos1, lidSetAsidePos2, toolsOutPos, toolsSetAsidePos1, toolsSetAsidePos2,
        tapePeeledPos, slimeTurnOverPos, slimePutDownPos;
    public Transform[] slimeSlamPos;
    int currentSlamPos = 0;
    public CinemachineVirtualCamera vCamStart, vCamLidPeeling, vCamBoxPutDown, vCamFinish;
    public Animator tapeAnimator;

    public bool lidRaised = false;
    public bool lidCanBeRaised = false;
    public bool tapePeeled = false;
    public bool tapeCanBePeeled = false;
    public bool boxTurnedOver = false;
    public bool boxCanBeTurnedOver = false;
    public bool boxPutDown = false;
    public bool boxCanBePutDown = false;

    int timesSmashed = 0;
    public GameObject smashButton;

    public SwipeDetector swipeDet;

    public GameObject phaseToActivate;

    private void Update() {
        if (swipeDet.outputText.text == "UP" || (Input.GetAxis("Vertical") > 0)) {
            if (!lidRaised) {
                RaiseLid();
            }
            swipeDet.outputText.text = "EMPTY";
        } else if (swipeDet.outputText.text == "LEFT" || (Input.GetAxis("Horizontal") < 0)) {
            if (!tapePeeled && tapeCanBePeeled) {
                PeelOffTape();
                tapeCanBePeeled = false;
            }
            swipeDet.outputText.text = "EMPTY";
        } else if (swipeDet.outputText.text == "RIGHT" || (Input.GetAxis("Horizontal") > 0)) {
            if (!boxTurnedOver && boxCanBeTurnedOver) {
                TurnOverSlimeBox();
            }
            swipeDet.outputText.text = "EMPTY";
        } else if (swipeDet.outputText.text == "DOWN" || (Input.GetAxis("Vertical") < 0)) {
            if (!boxTurnedOver && boxCanBeTurnedOver) {
                TurnOverSlimeBox();
            } else if (!boxPutDown && boxCanBePutDown) {
                PutDownSlimeBox();
            }
            swipeDet.outputText.text = "EMPTY";
        }
    }

    public void RaiseLid() {
        slimeLid.transform.DOMove(lidRaisedPos.position, 1f).OnComplete(TakeOutTools);
        lidRaised = true;
    }
    public void TakeOutTools() {
        tools.transform.parent = null;
        tools.transform.DOMove(toolsOutPos.position, 0.5f).OnComplete(SetAsideTop1);
    }
    public void SetAsideTop1() {
        slimeLid.transform.DOMove(lidSetAsidePos1.position, 1f);
        tools.transform.DOMove(toolsSetAsidePos1.position, 1f).OnComplete(SetAsideTop2);
    }
    public void SetAsideTop2() {
        slimeLid.transform.DOMove(lidSetAsidePos2.position, 0.7f);
        slimeLid.transform.DORotate(lidSetAsidePos2.eulerAngles, 0.7f);
        tools.transform.DOMove(toolsSetAsidePos2.position, 0.7f);
        tools.transform.DORotate(toolsSetAsidePos2.eulerAngles, 0.7f).OnComplete(SwitchCamsToPeel);
        slimeLid.transform.parent = null;
    }
    public void SwitchCamsToPeel() {
        vCamStart.Priority = 10;
        vCamLidPeeling.Priority = 20;
        tapeCanBePeeled = true;
        uiLeft.SetActive(true);
    }
    public void PeelOffTape() {
        tapeAnimator.Play("PeelTape");
        TimersManager.SetTimer(this, 0.75f, FallOffTape);
    }
    public void FallOffTape() {
        slimeTape.transform.DOMove(tapePeeledPos.position, 1.5f).SetEase(Ease.Linear).OnComplete(DestroyTape);
        slimeTape.transform.DORotate(tapePeeledPos.eulerAngles, 1f);
        boxCanBeTurnedOver = true;
        uiDown.SetActive(true);
    }
    public void TurnOverSlimeBox() {
        SwitchCamsToPutDown();
        slimeBox.transform.DORotate(slimeTurnOverPos.eulerAngles, 1f).OnComplete(EnableToPutDown);
        boxTurnedOver = true;
    }
    public void EnableToPutDown() {
        boxCanBePutDown = true;
        uiDown.SetActive(true);
    }
    public void SwitchCamsToPutDown() {
        vCamLidPeeling.Priority = 10;
        vCamBoxPutDown.Priority = 20;
    }
    public void PutDownSlimeBox() {
        SwitchCamsToFinish();
        slimeBox.transform.DOMove(slimePutDownPos.position, 0.5f).OnComplete(ActivateSmashButton);
        boxPutDown = true;
    }
    public void SwitchCamsToFinish() {
        vCamBoxPutDown.Priority = 10;
        vCamFinish.Priority = 20;
    }
    public void ActivateSmashButton() {
        smashButton.SetActive(true);
    }
    public void SmashBoxOnTable() {
        smashButton.SetActive(false);
        Sequence smashSequence = DOTween.Sequence();
        smashSequence.Append(slimeBox.transform.DOMoveY(2.5f, 0.5f));
        smashSequence.Append(slimeBox.transform.DOMoveY(2.26f, 0.07f).SetEase(Ease.OutQuad).OnComplete(LowerSlime));
        timesSmashed++;
        /*
        if (timesSmashed == 3) {
            smashButton.SetActive(false);
            StartPhase3();
        } else {
            smashButton.SetActive(true);
        }
        */
    }
    public void LowerSlime() {
        slimes.transform.DOMove(slimeSlamPos[currentSlamPos].position, 0.01f);
        currentSlamPos++;

        if (timesSmashed == 3) {
            smashButton.SetActive(false);
            StartPhase3();
        } else {
            smashButton.SetActive(true);
        }
    }
    
    void DestroyTape() {
        Destroy(slimeTape);
    }

    void StartPhase3() {
        gameObject.SetActive(false);
        phaseToActivate.SetActive(true);
    }
}
