using Lean.Pool;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public Image iconReload;
    public float fillTime = 2f;
    [SerializeField] private GameObject Home;
    [SerializeField] private MainMenu mainMenu;

    [SerializeField]
    private GameObject instantiatedMap;

    // Start is called before the first frame update
    private void OnEnable()
    {
        StartCoroutine(FillOverTime());
    }

    private IEnumerator FillOverTime()
    {
        float elapsedTime = 0f;

        // Start the LoadMap coroutine and wait for it to finish
        yield return StartCoroutine(LoadMap());

        while (elapsedTime < fillTime)
        {
            elapsedTime += Time.deltaTime;

            // Lerp để tăng giá trị fill từ 0 đến 1
            float fillAmount = Mathf.Lerp(0f, 1f, elapsedTime / fillTime);

            // Gán giá trị fill cho Image
            iconReload.fillAmount = fillAmount;

            yield return null;
        }

        Home.SetActive(false);
        // Khi coroutine kết thúc, làm thêm gì đó nếu cần
        Debug.Log("Load Map!");
        instantiatedMap.SetActive(true);
        mainMenu.Close();
    }

    private IEnumerator LoadMap()
    {
        // Tạo một bản đồ mới
        GameObject map = Resources.Load("Level/1") as GameObject;
        instantiatedMap = LeanPool.Spawn(map) as GameObject;
        LevelManager.Instance.AddLevel(instantiatedMap);
        instantiatedMap.SetActive(false);
        // Chờ cho đến khi bản đồ được tải hoàn toàn
        yield return new WaitUntil(() => instantiatedMap != null);


        Debug.Log("Map loaded!");
    }
}