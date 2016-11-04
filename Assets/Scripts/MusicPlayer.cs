using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {

    public AudioClip startMusic;
    public AudioClip fightMusic;
    public AudioClip loseMusic;

    public static MusicPlayer instance;

    AudioSource musicPlayer;

    int lastLevel;

    
    // Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
        if ((instance != null) && (instance != this))
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        musicPlayer = GetComponent<AudioSource>();
        lastLevel = 0;
	}
	
	void OnLevelWasLoaded(int level)
    {
        if ((level == 0) && (lastLevel == 3))
        {
            musicPlayer.Stop();
            musicPlayer.clip = startMusic;
            musicPlayer.Play();
            lastLevel = 0;
        }
        else if (level == 2)
        {
            musicPlayer.Stop();
            musicPlayer.clip = fightMusic;
            musicPlayer.Play();
            lastLevel = 2;
        }
        else if (level == 3)
        {
            musicPlayer.Stop();
            musicPlayer.clip = loseMusic;
            musicPlayer.Play();
            lastLevel = 3;
        }
        
    }
}
