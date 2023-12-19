using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public Image iconReload;
    public float fillTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FillOverTime());
    }

    IEnumerator FillOverTime()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fillTime)
        {
            elapsedTime += Time.deltaTime;

            // Lerp để tăng giá trị fill từ 0 đến 1
            float fillAmount = Mathf.Lerp(0f, 1f, elapsedTime / fillTime);

            // Gán giá trị fill cho Image
            iconReload.fillAmount = fillAmount;

            yield return null;
        }

        // Khi coroutine kết thúc, làm thêm gì đó nếu cần
        Debug.Log("Fill completed!");
    }
}
