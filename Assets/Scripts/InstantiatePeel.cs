using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePeel : MonoBehaviour
{
    public GameObject peel;

    public void PeelThePeel() {
        Instantiate(peel, transform);
    }
}
