using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase6 : MonoBehaviour
{
    public GameObject phaseToActivate;
    public void ActivatePhase7() {
        gameObject.SetActive(false);
        phaseToActivate.SetActive(true);
    }
}
