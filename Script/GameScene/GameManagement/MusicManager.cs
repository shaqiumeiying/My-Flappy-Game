using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    public AudioClip menuMusic;
    public AudioClip gameMusic;

    private AudioSource audioSource;
    private float menuVolume = 1f;
    private float gameVolume = 0.5f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        PlayGameMusic();
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void PlayGameMusic()
    {
        audioSource.clip = gameMusic;
        audioSource.volume = gameVolume;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void SetGameVolume()
    {
        audioSource.volume = gameVolume;
    }

    public void SetMenuVolume()
    {
        audioSource.volume = menuVolume;
    }
}
