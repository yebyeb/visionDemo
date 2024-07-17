using UnityEngine;


public class SoundControl : MonoBehaviour {

	public static SoundControl instance = null;
	[SerializeField] private AudioSource sourceMain;

    // Ses dosyalarını tüm sahnelerde açık tutmak için.
    private void Awake () {
		
		if (instance != null) {
			Destroy (gameObject);
		} else
		{
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
		}
	}
}