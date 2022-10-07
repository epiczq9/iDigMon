using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class Phase2 : MonoBehaviour
{
    public GameObject slimeBox, slimeLid, tools, slimeContainer, slime, slimeTape;
    public Transform lidRaisedPos, lidSetAsidePos, toolsOutPos, toolsSetAsidePos, tapePeeledPos, slimeTurnOverPos;
    public CinemachineVirtualCamera vCamStart, vCamLidPeeling;

    public bool lidRaised = false;

    public SwipeDetector swipeDet;

    private void Update() {
        if (swipeDet.outputText.text == "UP" || (Input.GetAxis("Vertical") > 0)) {
            if (!lidRaised) {
                RaiseLid();
            }
        }
    }

    public void RaiseLid() {
        slimeLid.transform.DOMove(lidRaisedPos.position, 1f).OnComplete(TakeOutTools);
        lidRaised = true;
    }
    public void TakeOutTools() {
        tools.transform.parent = null;
        tools.transform.DOMove(toolsOutPos.position, 1f);
    }
    public void PeelOffTape() {

    }
    public void TurnOverSlimeBox() {

    }
}
