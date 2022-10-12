using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Cinemachine;
using Timers;

public class Phase4 : MonoBehaviour
{
    public GameObject bag, slimeFull, slimeHalfed, slimeHalf1, slimeHalf2;
    public Transform bagPulledTran, slimeHalfTran1, slimeHalfTran2;

    void Start() {
        
    }

    
    void Update() {
        
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
        slimeHalf2.transform.DORotate(slimeHalfTran2.eulerAngles, 0.75f);
    }
}
