using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorButtons : MonoBehaviour
{
    [SerializeField] private Color[] brushColors;
    private Transform brushHair;

    private void Start() {

    }
    public void ChangeColor(int color) {
        GetComponent<PaintIn3D.P3dPaintSphere>().Color = brushColors[color];
    }
}

