using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeIcon : MonoBehaviour
{
   public void backToMenu()
{
    SceneManager.LoadScene("Menu");
    Time.timeScale = 1f;
}

}
