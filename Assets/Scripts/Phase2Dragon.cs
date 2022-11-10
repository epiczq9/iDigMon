using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using Timers;

public class Phase2Dragon : MonoBehaviour
{
    public GameObject slimeBox, eggShellTop, eggShellBottom, slimes;
    public GameObject uiUp, uiLeft, uiDown, uiTap;
    public Transform eggShellTopPos, eggShellBottomPos;
    public GameObject[] eggShells;
    int currentEggShell = 0;
    public CinemachineVirtualCamera vCamStart, vCamSmashingEgg, vCamBoxPutDown, vCamFinish;

    public bool eggshellTopRaised = false;
    public bool eggshellTopCanBeRaised = false;
    public bool eggshellBottomLowered = false;
    public bool eggshellBottomCanBeLowered = false;

    int timesSmashed = 0;
    public GameObject smashButton;

    public SwipeDetector swipeDet;

    public GameObject phaseToActivate;

    private void Start() {
        StartingPhase();
    }
    private void Update() {
        if (swipeDet.outputText.text == "UP" || (Input.GetAxis("Vertical") > 0)) {
            if (!eggshellTopRaised && eggshellTopCanBeRaised) {
                RaiseTop();
            }
            swipeDet.outputText.text = "EMPTY";
        } else if (swipeDet.outputText.text == "LEFT" || (Input.GetAxis("Horizontal") < 0)) {
            
            swipeDet.outputText.text = "EMPTY";
        } else if (swipeDet.outputText.text == "RIGHT" || (Input.GetAxis("Horizontal") > 0)) {
            
            swipeDet.outputText.text = "EMPTY";
        } else if (swipeDet.outputText.text == "DOWN" || (Input.GetAxis("Vertical") < 0)) {
            if (!eggshellBottomLowered && eggshellBottomCanBeLowered) {
                LowerBottom();
            }
            swipeDet.outputText.text = "EMPTY";
        }
    }

    public void StartingPhase() {
        SwitchCamsToSmashing();
        TimersManager.SetTimer(this, 1f, ActivateSmashButton);  
    }

    public void SwitchCamsToSmashing() {
        vCamStart.Priority = 10;
        vCamSmashingEgg.Priority = 20;
    }
    public void ActivateSmashButton() {
        smashButton.SetActive(true);
    }
    public void SmashEgg() {
        smashButton.SetActive(false);
        /*
        Sequence smashSequence = DOTween.Sequence();
        smashSequence.Append(slimeBox.transform.DOMoveY(2.5f, 0.5f));
        smashSequence.Append(slimeBox.transform.DOMoveY(2.26f, 0.07f).SetEase(Ease.OutQuad).OnComplete(LowerSlime));
        */
        timesSmashed++;
        EggShake();
        SwitchEggShell();
    }
    public void SwitchEggShell() {
        currentEggShell++;
        eggShells[currentEggShell].SetActive(true);
        eggShells[currentEggShell - 1].SetActive(false);

        if (timesSmashed == 3) {
            smashButton.SetActive(false);
            EggCracked();
        } else {
            smashButton.SetActive(true);
        }
    }
    public void EggCracked() {
        uiUp.SetActive(true);
        eggshellTopCanBeRaised = true;
    }
    public void EggShake() {
        Vector3 shakeAngle = new Vector3(0, 0, 15);
        Sequence shakeSequence = DOTween.Sequence();
        shakeSequence.Append(slimeBox.transform.DORotate(shakeAngle, 0.05f));
        shakeSequence.Append(slimeBox.transform.DORotate(-shakeAngle, 0.05f));
        shakeSequence.Append(slimeBox.transform.DORotate(shakeAngle / 2, 0.05f));
        shakeSequence.Append(slimeBox.transform.DORotate(-shakeAngle / 2, 0.05f));
        shakeSequence.Append(slimeBox.transform.DORotate(Vector3.zero, 0.05f));
    }

    public void RaiseTop() {
        eggShellTop.transform.DOMove(eggShellTopPos.position, 0.6f).OnComplete(EnableBottomLowering);
        eggshellTopRaised = true;
    }
    public void EnableBottomLowering() {
        uiDown.SetActive(true);
        eggshellBottomCanBeLowered = true;
    }
    public void LowerBottom() {
        eggShellBottom.transform.DOMove(eggShellBottomPos.position, 0.6f).OnComplete(StartPhase3);
        eggshellBottomLowered = true;
    }
    void StartPhase3() {
        gameObject.SetActive(false);
        phaseToActivate.SetActive(true);
    }
}
