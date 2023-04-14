using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public bool? isDoorOpen;
    public GameObject fridgeDoor;
    public AudioSource soundFolder;
    public AudioClip closeClip;
    public GameObject secondCoke;
    public TweenManager tManager;
    public GameObject milk;
    public GameObject pringles;
    public GameObject coke;
    private List<bool> bools;   
    public DataManager dataManager;
    public SpawnManager spawnManager;
    private string targetTag = null;
    private void Start()
    {
        isDoorOpen = false;
    }
    private void Update()
    {       
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.currentSelectedGameObject != null) return;
            if (EventSystem.current.IsPointerOverGameObject()) return;
            Ray();
            SetBool();
        }
    }
    private void Ray()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;     
        if (Physics.Raycast(ray, out hit,15f))
        {            
            targetTag = hit.collider.tag;
            switch (targetTag)
            {
                case "CokeBasket":
                    if (hit.collider.GetComponent<Basket>().isTweening == false) { hit.collider.GetComponent<Basket>().TweeningBasket(SpawnManager.PlaceFields.CokePlace); }
                    else { hit.collider.GetComponent<Basket>().StopTweening(); }                
                    break;
                case "MilkBasket":
                    if (hit.collider.GetComponent<Basket>().isTweening == false) { hit.collider.GetComponent<Basket>().TweeningBasket(SpawnManager.PlaceFields.MilkPlace); }
                    else { hit.collider.GetComponent<Basket>().StopTweening(); }                  
                    break;
                case "PringlesBasket":
                    if (hit.collider.GetComponent<Basket>().isTweening == false) { hit.collider.GetComponent<Basket>().TweeningBasket(SpawnManager.PlaceFields.PringlesPlace); }
                    else { hit.collider.GetComponent<Basket>().StopTweening(); }                  
                    break;
                case "Cap":
                    if (tManager.tweeningCaps.Count == 0)
                    {
                        hit.collider.GetComponentInParent<Cap>().canTween = true;
                    }
                    break;
                case "CokePlace":
                    spawnManager.SpawnObject(hit, coke, 0.6f,1);
                    dataManager.boolCount = 1;
                    break;
                case "MilkPlace":
                    dataManager.boolCount = 2;
                    spawnManager.SpawnObject(hit, milk, 0.6f,1);
                    break;
                case "PringlesPlace":
                    dataManager.boolCount = 3;
                    spawnManager.SpawnObject(hit, pringles, 0.6f,1);
                    break;
                case "Food":
                    if (dataManager.isRemoving == true && dataManager.isChoosed == true) { spawnManager.RemoveObjects(hit,1); }
                    dataManager.boolCount = 0;
                    break;
                case "Coke":
                    if(spawnManager.placeFields == SpawnManager.PlaceFields.CokePlace)
                    {
                        hit.collider.tag = "Food";
                        spawnManager.SpawnObject(hit, secondCoke, 0.15f,1);
                    }
                    if(dataManager.isRemoving == true && dataManager.isChoosed == true) { spawnManager.RemoveObjects(hit,1); }
                    break;
                case "FridgeDoor":
                    if(isDoorOpen != true) { OpenDoor(); }
                    else { CloseDoor(); }    
                    break;             
            }
        }
    }
    private void SetBool()
    {
        List<bool> disableBools = new List<bool>();
        dataManager.foodBools[dataManager.boolCount] = true;
        for (int i = 0; i < disableBools.Count; i++)
        {
            disableBools[i] = false;
            if (spawnManager.placeFields == SpawnManager.PlaceFields.None)
            {
                disableBools = new List<bool> { dataManager.canCoke, dataManager.canMilk, dataManager.canPringles };
            }
            if (spawnManager.placeFields == SpawnManager.PlaceFields.MilkPlace)
            {
                disableBools = new List<bool> { dataManager.canCoke, dataManager.canPringles };
            }
            if (spawnManager.placeFields == SpawnManager.PlaceFields.CokePlace)
            {
                disableBools = new List<bool> { dataManager.canMilk, dataManager.canPringles };
            }
            if (spawnManager.placeFields == SpawnManager.PlaceFields.PringlesPlace)
            {
                disableBools = new List<bool> { dataManager.canCoke, dataManager.canMilk };
            }
        } 
    }
    private void OpenDoor()
    {
        soundFolder.PlayOneShot(soundFolder.clip);
        fridgeDoor.transform.DORotate(new Vector3(0, -170, 0), 1).OnComplete(() => { isDoorOpen = true; });
    }
    public void CloseDoor()
    {
        if (tManager.tweeningCaps.Count == 0)
        {
            fridgeDoor.transform.DORotate(new Vector3(0, 0, 0), tManager.fridgeDuraiton).OnComplete(() => { isDoorOpen = false; soundFolder.PlayOneShot(closeClip); });
        }
        else
        {
            tManager.tweeningCaps[0].DoTweenBackCap();
            fridgeDoor.transform.DORotate(new Vector3(0, 0, 0), tManager.fridgeDuraiton).OnComplete(() => { isDoorOpen = false; soundFolder.PlayOneShot(closeClip); });
        }
    }
}
