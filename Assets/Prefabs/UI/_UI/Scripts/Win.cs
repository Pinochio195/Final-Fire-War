using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Win : UICanvas
{
    public Text _gold;
    public Text _countKill;

    public void NextGame()
    {
        UiManager.Instance.OpenUI<MainMenu>();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Close();
    }
}
