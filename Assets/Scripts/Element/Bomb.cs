using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] ParticleSystem _bomb;
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] float _radius = 5.0f; // bán kính của hình cầu
    private void OnEnable()
    {
        //_rigidbody.position = transform.position + Vector3.up * 15 ;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(Settings.Tag_Ground))
        {
            MusicManager.Instance.PlayAudio(Ring.MusicController.TypeMusic.Bomb);
            LeanPool.Spawn(_bomb, transform.position, Quaternion.identity);
            //_rigidbody.AddExplosionForce(20, transform.position, 5);
            LeanPool.Despawn(gameObject);
            CheckZombies();
        }
        if (other.gameObject.CompareTag(Settings.Tag_Zombie))
        {
        }
    }
    private void CheckZombies()
    {
        // Tạo ra một hình cầu tại vị trí bom
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag(Settings.Tag_Zombie)) // Thêm điều kiện kiểm tra tag
            {
                BotController botController = hitCollider.GetComponent<BotController>();
                botController._botController._touchBullet.GetDamage(.32f);//lấy ra vụ khí của player
                Debug.Log(hitCollider.name);
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
