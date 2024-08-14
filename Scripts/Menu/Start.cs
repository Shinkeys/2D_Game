using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonScript : MonoBehaviour
{
    public void StartButtonClicked()
    {
        string sceneToLoad = "level 1";
        SceneManager.LoadScene(sceneToLoad);
    }

    public void ExitButtonClicked(){
        Application.Quit();
    }
}