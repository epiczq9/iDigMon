using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int nextLevel;
    public void LoadScene() {
        SceneManager.LoadScene(nextLevel);
    }
}
