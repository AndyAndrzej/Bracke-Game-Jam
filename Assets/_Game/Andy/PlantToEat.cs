using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlantToEat : MonoBehaviour, IReaktable
{
    [SerializeField] private static float _anTime=1;
    [SerializeField] private static float _scaleMod = 0.01f, _secMoveMod = 0.1f;
    public void TakeAktion(GameObject source)
    {
        this.GetComponent<BoxCollider>().enabled = false;
        transform.DOMove(source.transform.position, _anTime).OnComplete(()=> { 
            GameManager.Instance.AddOneSizePoint();
            transform.DOMove(source.transform.position, _anTime* _secMoveMod);
            transform.DORotate(source.transform.eulerAngles, _anTime * _secMoveMod);
            transform.DOScale(_scaleMod* _secMoveMod, _anTime* _secMoveMod);
        });
        transform.DORotate(source.transform.eulerAngles, _anTime);
        transform.DOScale(_scaleMod, _anTime);
        
    }
}
