using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class WinGame : MonoBehaviour
{
    public TextMeshProUGUI heading1;
    public TextMeshProUGUI heading2;
    public Button restart;

    private void Start()
    {
        restart.gameObject.SetActive(false);
        heading1.gameObject.SetActive(false);
        heading2.gameObject.SetActive(false);
    }
    public void TurnOnUI()
    {
        restart.gameObject.SetActive(true);
        heading1.gameObject.SetActive(true);
        heading2.gameObject.SetActive(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}