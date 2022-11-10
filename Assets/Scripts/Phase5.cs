using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Timers;
using DG.Tweening;

public class Phase5 : MonoBehaviour
{
    public GameObject doll, paintBrush, middleMirror, hornsMirror;
    public PaintIn3D.P3dChangeCounter changeCounter;
    public GameObject[] nextMaskButtons, masks;
    public Transform frontView, sideView;
    public int currentMask = 0;
    public float currentRatio = 0;
    public float oldRatio = 0;
    public bool armButtonActivated = false, earsButtonActivated = false,
        legsButtonActivated = false, faceButtonActivated = false, finishedButtonActivated = false;
    public CinemachineVirtualCamera[] vCamsPainting;

    public SwipeDetector swipeDet;

    public GameObject phaseToActivate;
    void Start() {
        //ActivateBodyPainting();
        ActivateNextMaskButtonTimer();
        SwitchToSideView();
        ActivateMiddleMirror();
    }

    // Update is called once per frame
    void Update() {
        ButtonActivation();
        if (Input.GetButtonDown("Fire2")) {
            ActivateNextMaskButton();
        }
    }

    public void ActivateMiddleMirror() {
        middleMirror.SetActive(true);
    }
    public void DeactivateMiddleMirror() {
        middleMirror.SetActive(false);
    }
    public void ActivateHornsMirror() {
        hornsMirror.SetActive(true);
    }
    public void DeactivateHornsMirror() {
        hornsMirror.SetActive(false);
    }
    public int GetPercentPainted() {
        return (int)(changeCounter.Ratio * 100);
    }
    public void DebugPercent() {
        Debug.Log(GetPercentPainted());
    }
    public void ActivateNextMaskButtonTimer() {
        TimersManager.SetTimer(this, 3f, ActivateNextMaskButton);
        ActivateSingleMask(currentMask);
    }
    public void ActivateNextMaskButton() {
        nextMaskButtons[currentMask].SetActive(true);
        currentMask++;
    }
    public void SwitchToSideView() {
        doll.transform.DORotate(sideView.eulerAngles, 0.3f);
    }
    public void SwitchToFrontView() {
        doll.transform.DORotate(frontView.eulerAngles, 0.3f);
    }
    public void ButtonActivation() {
        /*
        currentRatio = GetPercentPainted();
        if (currentRatio > 0 && !armButtonActivated) {
            nextMaskButtons[0].SetActive(true);
            armButtonActivated = true;
        } else if (currentRatio > oldRatio && !earsButtonActivated && armButtonActivated) {
            nextMaskButtons[1].SetActive(true);
            earsButtonActivated = true;
        } else if (currentRatio > oldRatio && !legsButtonActivated && earsButtonActivated) {
            nextMaskButtons[2].SetActive(true);
            legsButtonActivated = true;
        } else if (currentRatio > oldRatio && !faceButtonActivated && legsButtonActivated) {
            nextMaskButtons[3].SetActive(true);
            faceButtonActivated = true;
        } else if (currentRatio > oldRatio && !finishedButtonActivated && faceButtonActivated) {
            nextMaskButtons[4].SetActive(true);
            finishedButtonActivated = true;
        }

        /*
        if (35.5f <= GetPercentPainted() && !armButtonActivated) {
            nextMaskButtons[0].SetActive(true);
            armButtonActivated = true;
        } else if (41.5f <= GetPercentPainted() && !earsButtonActivated) {
            nextMaskButtons[1].SetActive(true);
            earsButtonActivated = true;
        } else if (48f <= GetPercentPainted() && !legsButtonActivated) {
            nextMaskButtons[2].SetActive(true);
            legsButtonActivated = true;
        } else if (56f <= GetPercentPainted() && !faceButtonActivated) {
            nextMaskButtons[3].SetActive(true);
            faceButtonActivated = true;
        } else if (59f <= GetPercentPainted() && !finishedButtonActivated) {
            nextMaskButtons[4].SetActive(true);
            finishedButtonActivated = true;
        }
        */

    }
    public void ActivateSingleMask(int maskToActivateIndex) {
        for (int i = 0; i < masks.Length; i++) {
            masks[i].SetActive(false);
        }
        masks[maskToActivateIndex].SetActive(true);
    }
    public void SwitchCamsForPainting(CinemachineVirtualCamera vCam1, CinemachineVirtualCamera vCam2) {
        vCam1.Priority = 10;
        vCam2.Priority = 20;
    }
    public void ActivateBodyPainting() {
        ActivateSingleMask(0);
    }
    public void ActivateWingsBasePainting() {
        ActivateSingleMask(1);
        //SwitchCamsForPainting(vCamsPainting[0], vCamsPainting[1]);
    }
    public void ActivateEarsPainting() {
        ActivateSingleMask(2);
        //SwitchCamsForPainting(vCamsPainting[1], vCamsPainting[2]);
    }
    public void ActivateLegsPainting() {
        ActivateSingleMask(3);
        //SwitchCamsForPainting(vCamsPainting[2], vCamsPainting[3]);
    }
    public void ActivateFacePainting() {
        ActivateSingleMask(4);
        //SwitchCamsForPainting(vCamsPainting[3], vCamsPainting[4]);
    }
    public void FinishedPainting() {
        ActivatePhase6();
    }
    public void ActivatePhase6() {
        gameObject.SetActive(false);
        phaseToActivate.SetActive(true);
    }
}
