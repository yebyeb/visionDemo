using UnityEngine;
using UnityEngine.SceneManagement;  
using UnityEngine.UI;
public class MenuControl : MonoBehaviour
{
    private SoundControl SD;
    [SerializeField] private int heartsCount;
    [SerializeField] private int OptionsBool;
    [SerializeField] private GameObject[] hearts;
    [SerializeField] private GameObject rewardedScreen;
    [SerializeField] public GameObject SoundButton;
    [SerializeField] private int SoundBool;
    [SerializeField] private Image SoundImg;
	[SerializeField] private Sprite SoundOff;
	[SerializeField] private Sprite SoundOn;

     private void Awake()
    {
        SD = FindObjectOfType<SoundControl>(); 
    }

    private void Start()
    {   
        //Kalan kalp haklarını hesaplar.
        heartsCount = PlayerPrefs.GetInt("heartsCountSave");
         for (int i = 0; i < heartsCount; i++)
         {
             hearts[i].SetActive(true);
         }

         // Ses dosyalarının önceki verilen karara göre sessiz veya sesli olması için.
		SoundBool = PlayerPrefs.GetInt("SoundBoolSave");
		if (SoundBool == 0)
		{
			SoundImg.GetComponent<Image>().sprite = SoundOn;
			AudioListener.volume = 1;
		}
		else
		{
			SoundImg.GetComponent<Image>().sprite = SoundOff;
			AudioListener.volume = 0;
		}
    }

    public void GameScene() 
    { 
        //Kalp hakkı var ise oyuna başlar ve1 kalp hakkını harcar yok ise reward menüsünü açar.
     if(heartsCount == 0)
        {
          rewardedScreen.SetActive(true);
        }
        else
        {
         heartsCount--;
         PlayerPrefs.SetInt("heartsCountSave", heartsCount);
         for (int i = 2; i >= heartsCount; i--)
         {
             hearts[i].SetActive(false);
         }

         SceneManager.LoadScene("GameScene");  
        }      
    } 
    // Reward videoyu izler ise 3 kalp hakkı kazanır.
    public void GetReward () 
    {
	     heartsCount = 3;        
         PlayerPrefs.SetInt("heartsCountSave", heartsCount);
         for (int i = 0; i < heartsCount; i++)
         {
             hearts[i].SetActive(true);
         }
	}
    // Options butonunun içindekileri açar ve kapatır.
    public void ToggleOptions () {
		if (OptionsBool == 0) {
			SoundButton.SetActive(true);
			OptionsBool = 1;
		} else
		{
			SoundButton.SetActive(false);
			OptionsBool = 0;
		}
	}

// Sound butonu ile sesin açılması veya kapatılması için.
    public void ToggleSound () {
		if (SoundBool == 0) {
			SoundImg.GetComponent<Image>().sprite = SoundOff;
			AudioListener.volume = 0;
			SoundBool = 1;
			PlayerPrefs.SetInt("SoundBoolSave", SoundBool);
		} else
		{
			SoundImg.GetComponent<Image>().sprite = SoundOn;
			AudioListener.volume = 1 ;
			SoundBool = 0;
			PlayerPrefs.SetInt("SoundBoolSave", SoundBool);
		}
	}
}
