using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject creditsPanel;

    public GameObject instructionsPanel1;
    public GameObject instructionsPanel2;
    public GameObject instructionsPanel3;
    public GameObject instructionsPanel4;

    public GameObject backButton;

    private void Start()
    {
        creditsPanel.SetActive(false);
        instructionsPanel1.SetActive(false);
        instructionsPanel2.SetActive(false);
        instructionsPanel3.SetActive(false);
        backButton.SetActive(false);
    }

    public void SelectButton(int index)
    {
        switch(index)
        {
            case 0:
            {
                Time.timeScale = 0;
                ShowCredits();
                break;
            }
            case 1:
            {
                PlayGame();
                break;
            }
            case 2:
            {
                Time.timeScale = 0;
                ShowInstructions1();
                break;
            }
        }
    }

    public void BackButton()
    {
        creditsPanel.SetActive(false);
        instructionsPanel1.SetActive(false);
        backButton.SetActive(false);
        Time.timeScale = 1;
    }

    private void ShowCredits()
    {
        creditsPanel.SetActive(true);
        backButton.SetActive(true);
    }

    private void PlayGame()
    {
        SceneManager.LoadScene("FactoryScene");
    }

    public void ShowInstructions1()
    {
        instructionsPanel1.SetActive(true);
        instructionsPanel2.SetActive(false);
    }

    public void ShowInstructions2()
    {
        instructionsPanel2.SetActive(true);
        instructionsPanel1.SetActive(false);
        instructionsPanel3.SetActive(false);

    }

    public void ShowInstructions3()
    {
        instructionsPanel3.SetActive(true);
        instructionsPanel2.SetActive(false);
        instructionsPanel4.SetActive(false);
    }

    public void ShowInstructions4()
    {
        instructionsPanel4.SetActive(true);
        instructionsPanel3.SetActive(false);
    }
}
