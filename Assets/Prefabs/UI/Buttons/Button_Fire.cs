using Lean.Pool;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Button_Fire : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isCheckVibrate;
    [SerializeField] private Text _countFire1;
    [SerializeField] private Text _countFire2;
    [SerializeField] private Image _iconReload;
    private float fireRate = 2f; // Tần số bắn
    private float nextFire; // Thời điểm có thể bắn tiếp theo

    public void OnPointerDown(PointerEventData eventData)
    {
        isCheckVibrate = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isCheckVibrate = false;
        PlayerManager.Instance._playerController._animator.SetBool(Settings.AnimationFire, false);
    }

    private void Update()
    {
        if (Int32.Parse(_countFire1.text) > 0)
        {
            ClickFIre();
        }
    }

    private void ClickFIre()
    {
        if (isCheckVibrate)
        {
            Handheld.Vibrate();
            if (!PlayerManager.Instance._playerController._animator.GetBool(Settings.AnimationFire))
            {
                PlayerManager.Instance._playerController._animator.SetBool(Settings.AnimationFire, true);
            }
            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate * Time.deltaTime;
                CountFire();
            }
        }
    }

    private void CountFire()
    {
        int count1 = Int32.Parse(_countFire1.text);
        int count2 = Int32.Parse(_countFire2.text);
        if (count2 > 0 || (count1 > 0 && count2 <= 0))
        {
            if (count1 <= 0)
            {
                StartCoroutine(ResetBullet());
            }
            else
            {
                _countFire1.text = (--count1).ToString();
                GameObject bullet = LeanPool.Spawn(PlayerManager.Instance._playerController._prefabBullet, PlayerManager.Instance._playerController._firePosition.position, Quaternion.identity);
                LeanPool.Despawn(bullet, 5);
                //trừ đạn theo từng viên
                PlayerManager.Instance._playerController._effectFire.Play();
                if (Input.GetMouseButton(0))
                {
                    MusicManager.Instance.PlayAudio(Ring.MusicController.TypeMusic.Fire);
                }
                Debug.Log(_countFire1.text);
            }
        }
    }

    public IEnumerator ResetBullet()
    {
        float elapsedTime = 0f;
        int count2 = Int32.Parse(_countFire2.text);
        int count1 = Int32.Parse(_countFire1.text);
        PlayerManager.Instance._playerController._animator.SetBool(Settings.AnimationReload, true);
        if (count2 > 0 && count1 < 40)//TODO: dưới mức dạn của vũ khí
        {
            while (elapsedTime < 2)
            {
                elapsedTime += Time.deltaTime;
                // Lerp để tăng giá trị fill từ 0 đến 1
                float fillAmount = Mathf.Lerp(0f, 1f, elapsedTime / 2);
                // Gán giá trị fill cho Image
                _iconReload.fillAmount = fillAmount;
                yield return null;
            }

            // Khi coroutine kết thúc, làm thêm gì đó nếu cần
            Debug.Log("Fill completed!");
            count2 -= 40;
            _countFire2.text = count2.ToString();
            _countFire1.text = 40.ToString();
        }
    }
}