using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialCanvas;
    
    private void Start()
    {
        ShowTutorial();
    }

    public void ShowTutorial()
    {
        tutorialCanvas.SetActive(true);
        Time.timeScale = 0f;
        StartCoroutine(WaitForPlayerInput());
    }

    private IEnumerator WaitForPlayerInput()
    {
        // Wait for player input
        while (true)
        {
            if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
            {
                break; // Exit loop if player input detected
            }
            yield return null; // Wait for next frame
        }

        // Player input detected, hide tutorial immediately
        Time.timeScale = 1f;
        tutorialCanvas.SetActive(false);
    }
}
