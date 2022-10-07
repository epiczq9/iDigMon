using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using Timers;

public class Phase2 : MonoBehaviour
{
    public GameObject slimeBox, slimeLid, tools, slimeContainer, slime, slimeTape;
    public Transform lidRaisedPos, lidSetAsidePos1, lidSetAsidePos2, toolsOutPos, toolsSetAsidePos1, toolsSetAsidePos2, tapePeeledPos, slimeTurnOverPos;
    public CinemachineVirtualCamera vCamStart, vCamLidPeeling;
    public Animator tapeAnimator;

    public bool lidRaised = false;
    public bool lidCanBeRaised = false;
    public bool tapePeeled = false;
    public bool tapeCanBePeeled = false;

    public SwipeDetector swipeDet;

    private void Update() {
        if (swipeDet.outputText.text == "UP" || (Input.GetAxis("Vertical") > 0)) {
            if (!lidRaised) {
                RaiseLid();
            }
        } else if(swipeDet.outputText.text == "LEFT" || (Input.GetAxis("Horizontal") < 0)) {
            if (!tapePeeled && tapeCanBePeeled) {
                PeelOffTape();
            }
        }
    }

    public void RaiseLid() {
        slimeLid.transform.DOMove(lidRaisedPos.position, 1f).OnComplete(TakeOutTools);
        lidRaised = true;
    }
    public void TakeOutTools() {
        tools.transform.parent = null;
        tools.transform.DOMove(toolsOutPos.position, 1f).OnComplete(SetAsideTop1);
    }
    public void SetAsideTop1() {
        slimeLid.transform.DOMove(lidSetAsidePos1.position, 1f);
        tools.transform.DOMove(toolsSetAsidePos1.position, 1f).OnComplete(SetAsideTop2);
    }
    public void SetAsideTop2() {
        slimeLid.transform.DOMove(lidSetAsidePos2.position, 1f);
        slimeLid.transform.DORotate(lidSetAsidePos2.eulerAngles, 1f);
        tools.transform.DOMove(toolsSetAsidePos2.position, 1f);
        tools.transform.DORotate(toolsSetAsidePos2.eulerAngles, 1f).OnComplete(SwitchCams);
    }
    public void SwitchCams() {
        vCamStart.Priority = 10;
        vCamLidPeeling.Priority = 20;
        tapeCanBePeeled = true;
    }
    public void PeelOffTape() {
        tapeAnimator.Play("PeelTape");
        TimersManager.SetTimer(this, 0.75f, FallOffTape);
    }
    public void FallOffTape() {
        slimeTape.transform.DOMove(tapePeeledPos.position, 1.5f).SetEase(Ease.Linear).OnComplete(DestroyTape);
        slimeTape.transform.DORotate(tapePeeledPos.eulerAngles, 1f);
    }
    public void TurnOverSlimeBox() {

    }

    void DestroyTape() {
        Destroy(slimeTape);
    }
}
