using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class HowToPlayController : MonoBehaviour
{

    public void Return()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
