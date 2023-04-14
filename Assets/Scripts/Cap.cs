using DG.Tweening;
using UnityEngine;
public class Cap : MonoBehaviour 
{
    public AudioSource soundFolder;
    public AudioClip tabClip;
    public GameObject tabsCap;
    public bool isTweening;
    public bool canTween;
    private Vector3 camTweenPos;
    public Vector3 spawnPos;
    public SpawnManager spawnManager;   
    public DataManager dataManager;
    public TweenManager tweenManager;    
    private void Start()
    {
        spawnPos = transform.position;
        camTweenPos = new Vector3(0.087f, -0.136f, 0.6f);
    }
    private void Update()
    {
        if(tweenManager.tweeningCaps.Count <= 0)
        {
            if (canTween == true && dataManager.isSwiping != true) { DoTweenCap(); }
        }
    }
    public void DoTweenCap()
    {
        isTweening = true;
        tweenManager.tweeningCaps.Add(this);
        transform.DOLocalMove(camTweenPos, tweenManager.capDuration);
        transform.DORotate(new Vector3(60, 0, 0), tweenManager.capDuration).OnComplete(() => 
        {
            tabsCap.transform.DOLocalMoveX(-0.4f, tweenManager.capDuration); canTween = false; 
            isTweening = false;
            dataManager.canPlace = true;
            soundFolder.PlayOneShot(tabClip);
        });
    }
    public void DoTweenBackCap()
    {
        foreach (Cap c in tweenManager.tweeningCaps)
        {
            if(c.isTweening == false)
            {
                soundFolder.PlayOneShot(tabClip);
                c.GetComponent<Cap>().tabsCap.gameObject.transform.DOLocalMoveX(-0.09f, tweenManager.capDuration).OnComplete(() =>
                {
                    c.transform.DOMove(c.spawnPos, tweenManager.capDuration);
                    c.transform.DORotate(new Vector3(0, 0, 0), tweenManager.capDuration).OnComplete(() => { tweenManager.tweeningCaps.Remove(c); dataManager.isPlacing = false; dataManager.canPlace = false; });
                });
            }          
        }
    }
}
