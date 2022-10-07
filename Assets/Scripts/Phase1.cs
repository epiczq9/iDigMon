using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class Phase1 : MonoBehaviour
{
    public GameObject plushie, slimeBox;
    public Transform slimeBoxOutPos, slimeBoxStraightPos, monSetAsidePos;

    bool slimeActive = false;

    public GameObject phaseToActivate;

    public SwipeDetector swipeDet;

    private void Update() {
        if(swipeDet.outputText.text == "UP" && slimeActive) {
            TakeOutSlimeBox();
        } else if (Input.GetAxis("Vertical") > 0) {
            TakeOutSlimeBox();
        }
    }

    public void PlushieOpenMouth() {
        plushie.GetComponent<Plushie>().openMouthBool = true;
        slimeBox.transform.parent = null;
        slimeActive = true;
    }

    public void TakeOutSlimeBox() {
        slimeBox.transform.DOMove(slimeBoxOutPos.position, 1.5f).OnComplete(StraightSlimeBox);
    }

    public void StraightSlimeBox() {
        slimeBox.transform.DOMove(slimeBoxStraightPos.position, 1f);
        slimeBox.transform.DORotate(slimeBoxStraightPos.eulerAngles, 1f).OnComplete(StartPhase2);
        plushie.transform.DOMove(monSetAsidePos.position, 1f);
    }

    void StartPhase2() {
        gameObject.SetActive(false);
        phaseToActivate.SetActive(true);
    }
}
