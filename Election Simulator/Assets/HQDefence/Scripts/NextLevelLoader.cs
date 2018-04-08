using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelLoader : MonoBehaviour
{

    public string NextLevelName;
    public KeyCode KeyActivate;

    void Update()
    {
        if (Input.GetKeyDown(KeyActivate))
            LoadNextLevel();
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(NextLevelName);
    }
}
