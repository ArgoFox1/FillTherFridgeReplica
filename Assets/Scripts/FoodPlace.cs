using UnityEngine;

public class FoodPlace : MonoBehaviour
{
    private bool isCollision;
    private string foodTag;
    public DataManager dataManager;
    public BoxCollider foodPlaceCollider;
    public SpawnManager spawnManager;
    private void Update()
    {
        foodTag = this.gameObject.tag;
        if (foodTag == "CokePlace")
        {
            if (spawnManager.placeFields == SpawnManager.PlaceFields.CokePlace) { foodPlaceCollider.enabled = true; }
            else { foodPlaceCollider.enabled = false; }
        }
        if (foodTag == "MilkPlace")
        {
            if (spawnManager.placeFields == SpawnManager.PlaceFields.MilkPlace) { foodPlaceCollider.enabled = true; }
            else { foodPlaceCollider.enabled = false; }
        }
        if (foodTag == "PringlesPlace")
        {
            if (spawnManager.placeFields == SpawnManager.PlaceFields.PringlesPlace) { foodPlaceCollider.enabled = true; }
            else { foodPlaceCollider.enabled = false; }
        }
    }
}
