using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Import Unity's UI namespace

public class StartGame : MonoBehaviour
{
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(StartGameScene);
    }

    void StartGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
