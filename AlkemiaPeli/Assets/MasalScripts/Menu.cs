using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void SwitchToMasalOriginalScene()
    {
        SceneManager.LoadScene("MasalOriginalScene");
    }
}
