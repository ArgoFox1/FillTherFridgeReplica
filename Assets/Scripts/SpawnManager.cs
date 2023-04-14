using DG.Tweening;
using System.Collections;
using UnityEngine;

public  class SpawnManager : MonoBehaviour
{
    public AudioSource soundFolder;
    public AudioClip placeClip;
    public AudioClip removeClip;
    public TweenManager tweenManager;
    public DataManager dataManager;
    public enum PlaceFields
    {
        None,
        MilkPlace,
        CokePlace,
        PringlesPlace
    }
    public PlaceFields placeFields;
    public void RemoveObjects(RaycastHit h , int negativeXpCount )
    {
        if (tweenManager.tweeningCaps[0].isTweening != true && dataManager.isRemoving == true)
        {
            dataManager.spaceCount -= negativeXpCount;
            tweenManager.tweeningBaskets[0].negativefoodCount++;
            tweenManager.tweeningBaskets[0].positivefoodCount--;
            dataManager.canPlace = false;
            dataManager.isPlacing = false;
            soundFolder.PlayOneShot(removeClip);
            Destroy(h.collider.gameObject);
        }
    }
    public void SpawnObject(RaycastHit h, GameObject g, float valueY , int xpCount)
    {
        if(dataManager.isRemoving != true && dataManager.canPlace == true && tweenManager.tweeningCaps[0].isTweening != true && dataManager.isChoosed == true)
        {
            dataManager.spaceCount += xpCount;
            tweenManager.tweeningBaskets[0].positivefoodCount++;
            soundFolder.PlayOneShot(placeClip);
            dataManager.canPlace = false;
            dataManager.isPlacing = true;
            GameObject newObject = Instantiate(g, g.transform.position, Quaternion.Euler(60, 0, 0));
            newObject.transform.parent = h.collider.transform;
            newObject.transform.localPosition = new Vector3(0, valueY, 0);
            newObject.transform.DOScale(1.25f, tweenManager.foodTweenDuration).From().SetLoops(1, LoopType.Yoyo);
            StartCoroutine(nameof(Cooldown4Spawn));
        }      
    }
    IEnumerator Cooldown4Spawn()
    {
        yield return new WaitForSeconds(0.1f);
        dataManager.canPlace = true;
    }   
}
