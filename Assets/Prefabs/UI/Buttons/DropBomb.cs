using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBomb : BaseClickButton
{
    [SerializeField] GameObject _bombPrefabs;
    protected override void OnButtonClick()
    {
        myButton.interactable = false;
        GameObject bom = LeanPool.Spawn(_bombPrefabs, PlayerManager.Instance.transform.position + Vector3.up*2,Quaternion.identity);
        StartCoroutine(DelayBom());
    }
    IEnumerator DelayBom()
    {
        yield return new WaitForSeconds(1f);
        myButton.interactable = true;
    }
  
}
