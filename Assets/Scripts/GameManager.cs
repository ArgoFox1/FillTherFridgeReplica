using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
public class GameManager : MonoBehaviour 
{
    private int sceneCount;
    public ADManager adManager;
    public Player player;
    public AudioSource soundFolder;
    public TweenManager tweenManager;
    public DataManager dataManager;
    public Text coinText;
    public Image bar;
    public Image wonImage;
    public Image lostImage;
    public Image pauseImage;

    #region Singleton
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    #endregion   

    private void Start()
    {
        soundFolder.PlayOneShot(soundFolder.clip);
    }
    private void Update()
    {
        if (adManager.interstitialAd.CanShowAd() != true) { Time.timeScale = 0; }
        if(pauseImage.gameObject.activeInHierarchy == true) { Time.timeScale = 0; }
        #region UIs staff
        sceneCount = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(sceneCount);  
        #region Coin      
        coinText.DOText($"{dataManager.spaceCount / 96 * 20}", 0.05f);
        #endregion

        #region Bar
        if (sceneCount == 1) { bar.fillAmount = (float)dataManager.spaceCount / 95; }
        else if (sceneCount == 2) { bar.fillAmount = (float)dataManager.spaceCount / 300; }
        #endregion

        #region SoundSettings
        if (Time.timeScale == 0) { soundFolder.Pause(); }
        else { soundFolder.UnPause(); }
        #endregion

        #region Won
        if (sceneCount == 1 && dataManager.spaceCount >= 95 && player.isDoorOpen == true) { StartCoroutine(nameof(Cooldown4Win)); }
        else if (sceneCount == 2 && dataManager.spaceCount >= 180 && player.isDoorOpen == true) { StartCoroutine(nameof(Cooldown4Win)); }
        #endregion

        #region Lost
        if (dataManager.spaceCount < 0) { lostImage.gameObject.SetActive(true); }
        #endregion

        #endregion
    }
    IEnumerator Cooldown4Win()
    {
        player.isDoorOpen = false;
        player.CloseDoor();
        yield return new WaitForSeconds(tweenManager.fridgeDuraiton);
        wonImage.gameObject.SetActive(true);
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(2);
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene(sceneCount);
    }
    public void GoMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Resume()
    {
        Time.timeScale = 1;
        pauseImage.gameObject.SetActive(false);
    }
    public void Settings()
    {
        adManager.interstitialAd.Show();
        Time.timeScale = 0;
        pauseImage.gameObject.SetActive(true);
    }
    public void SoundOff()
    {
        soundFolder.mute = !soundFolder.mute;
    }
    public void RemoveButton()
    {
        if(tweenManager.tweeningBaskets.Count != 0 )
        {
            dataManager.isRemoving = true;
            dataManager.canPlace = false;
        }
    }
    public void VerifyButton()
    {
        if (tweenManager.tweeningBaskets.Count != 0)
        {
            dataManager.isRemoving = false;
            dataManager.canPlace = true;
        }
    }
}
