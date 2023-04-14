using UnityEngine;

public class Swipe : MonoBehaviour
{
    public SpawnManager spawnManager;
    public DataManager dataManager;

    [SerializeField] private float swipeSpeed = 2f;

    private float? lastMousePoint = null;

    private void Update()
    {
        if (dataManager.isPlacing != true && spawnManager.placeFields == SpawnManager.PlaceFields.None) { Turn(); }
    }
    private void Turn()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePoint = Input.mousePosition.x;
            dataManager.isSwiping = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            lastMousePoint = null;
            dataManager.isSwiping = false;
        }
        if (lastMousePoint != null)
        {
            float difference = Input.mousePosition.x - lastMousePoint.Value;
            Vector3 target = new Vector3(transform.position.x + difference * Time.fixedDeltaTime, transform.position.y, transform.position.z);
            target.x = Mathf.Clamp(target.x,85,92);
            transform.position = Vector3.Lerp(transform.position, target, swipeSpeed * Time.fixedDeltaTime);
            lastMousePoint = Input.mousePosition.x;
        }
    }
}
