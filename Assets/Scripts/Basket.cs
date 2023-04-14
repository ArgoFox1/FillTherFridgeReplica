using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class Basket : MonoBehaviour
{
    public List<GameObject> foods;
    public bool isTweening;
    public int negativefoodCount;
    public int positivefoodCount;
    public DataManager dataManager;
    public SpawnManager spawnManager;
    public TweenManager tweenManager;
    public Vector3 spawnPos;
    private void Start()
    {
        spawnPos = transform.position;
    }
    private void Update()
    {
        if (tweenManager.tweeningBaskets.Count != 0 && dataManager.isChoosed == true)
        {
            foreach (Basket b in tweenManager.tweeningBaskets)
            {
                for (int i = 0; i < b.positivefoodCount; i++)
                {
                    if(b.foods.Count >= b.positivefoodCount)
                    {
                        dataManager.canPlace = true;
                        b.foods[i].SetActive(false);
                    }
                    else { dataManager.canPlace = false; dataManager.isChoosed = false; }
                }
            }          
        }
        if (tweenManager.tweeningBaskets.Count != 0 && dataManager.isChoosed == true)
        {
            if(dataManager.isRemoving == true)
            {
                foreach (Basket b in tweenManager.tweeningBaskets)
                {
                    for (int i = 0; i < b.negativefoodCount; i++)
                    {
                        b.foods[i].SetActive(true);
                    }
                }
            }
            else { tweenManager.tweeningBaskets[0].negativefoodCount = 0; }
        }
    }
    public void TweeningBasket(SpawnManager.PlaceFields fields)
    {
        if (tweenManager.tweeningBaskets.Count <= 0)
        {
            dataManager.isChoosed = true;
            dataManager.canPlace = false;
            spawnManager.placeFields = fields;
            tweenManager.tweeningBaskets.Add(this);
            isTweening = true;
            transform.DOLocalMoveY(-0.750f, tweenManager.basketDuration);
            transform.DOShakeRotation(tweenManager.basketDuration, tweenManager.strength, tweenManager.vibration, tweenManager.randomness).OnComplete(() =>
            {
                transform.DOLocalRotate(new Vector3(20, 0, 0), tweenManager.basketDuration);
                dataManager.canPlace = true;
            });
        }       
    }
    public void StopTweening()
    {
        transform.DOMoveY(spawnPos.y, tweenManager.basketDuration);
        transform.DORotate(new Vector3(0, 0, 0), tweenManager.basketDuration);
        isTweening = false;
        tweenManager.tweeningBaskets.Remove(this);
        spawnManager.placeFields = SpawnManager.PlaceFields.None;
        dataManager.boolCount = 0;
        dataManager.isChoosed = false;
    }
}
