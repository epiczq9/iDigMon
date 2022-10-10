using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Cinemachine;
using Timers;

public class Phase3 : MonoBehaviour
{
    public GameObject slimeContainer, slimeParent;
    public Transform slimeContainerLiftPos, slimeContainerSetAsidePos, slimeSetDownPos;
    public GameObject[] slimes;

    public bool containerRaised = false;
    public bool containerCanBeRaised = false;

    public SwipeDetector swipeDet;
    void Start() {
        containerCanBeRaised = true;
    }
    void Update() {
        if (swipeDet.outputText.text == "UP" || (Input.GetAxis("Vertical") > 0)) {
            if (!containerRaised && containerCanBeRaised) {
                SetAsideContainer();
            }
            swipeDet.outputText.text = "EMPTY";
        } else if (swipeDet.outputText.text == "LEFT" || (Input.GetAxis("Horizontal") < 0)) {
            
            swipeDet.outputText.text = "EMPTY";
        } else if (swipeDet.outputText.text == "RIGHT" || (Input.GetAxis("Horizontal") > 0)) {
            
            swipeDet.outputText.text = "EMPTY";
        } else if (swipeDet.outputText.text == "DOWN" || (Input.GetAxis("Vertical") < 0)) {
            
            swipeDet.outputText.text = "EMPTY";
        }
    }
    public void SetAsideContainer() {
        slimeParent.transform.parent = null;
        Sequence containerSequence = DOTween.Sequence();
        containerSequence.Append(slimeContainer.transform.DOMove(slimeContainerLiftPos.position, 1f));
        containerSequence.Append(slimeContainer.transform.DOMove(slimeContainerSetAsidePos.position, 0.5f)).OnComplete(SetDownSlime);
        containerRaised = true;
    }
    public void SetDownSlime() {
        Destroy(slimeContainer);
        slimeParent.transform.DOMove(slimeSetDownPos.position, 0.5f);
        slimeParent.transform.DORotateQuaternion(slimeSetDownPos.rotation, 0.5f);
    }
}
