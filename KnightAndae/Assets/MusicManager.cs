using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static AudioClip musicForest, musicTown, musicMountain, musicSwamp, musicCastle, musicBoss, musicTitle;
    static AudioSource audiosource;
    string sceneName;
    static string musicName;

    // Start is called before the first frame update
    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        musicForest = Resources.Load<AudioClip>("forest");
        musicTown = Resources.Load<AudioClip>("town");
        musicMountain = Resources.Load<AudioClip>("mountain");
        musicSwamp = Resources.Load<AudioClip>("swamp");
        musicCastle = Resources.Load<AudioClip>("castle");
        musicBoss = Resources.Load<AudioClip>("boss");
        musicTitle = Resources.Load<AudioClip>("title");

        audiosource = GetComponent<AudioSource>();
        // this switch is mainly for debugging purpose, simply doing PlayMusic("title"); would work too
        switch (sceneName)
        {
            case "TitleScreen":
                musicName = "title";
                PlayMusic("title");
                break;
            case "PlainsScene":
                musicName = "forest";
                PlayMusic("forest");
                break;
            case "TownScene":
                musicName = "town";
                PlayMusic("town");
                break;
            case "MountainScene":
                musicName = "mountain";
                PlayMusic("mountain");
                break;
            case "SwampScene":
                musicName = "swamp";
                PlayMusic("swamp");
                break;
            case "Castle1":
                musicName = "castle";
                PlayMusic("castle");
                break;
            case "Castle2":
                musicName = "castle";
                PlayMusic("castle");
                break;
            case "Castle3":
                musicName = "castle";
                PlayMusic("castle");
                break;
            case "Castle4":
                musicName = "castle";
                PlayMusic("castle");
                break;
            case "Castle5":
                musicName = "castle";
                PlayMusic("castle");
                break;
        }
    }

    private void Update()
    {
        string curSceneName = SceneManager.GetActiveScene().name;
        if (sceneName != curSceneName)
        {
            switch (curSceneName)
            {
                case "TitleScreen":
                    SwitchMusic("title");
                    break;
                case "PlainsScene":
                    SwitchMusic("forest");
                    break;
                case "TownScene":
                    SwitchMusic("town");
                    break;
                case "MountainScene":
                    SwitchMusic("mountain");
                    break;
                case "SwampScene":
                    SwitchMusic("swamp");
                    break;
                case "Castle1":
                    SwitchMusic("castle");
                    break;
            }
            sceneName = curSceneName;
        }
    }
    public static void PlayMusic(string clip)
    {
        musicName = clip;
        switch (clip)
        {
            case "title":
                audiosource.PlayOneShot(musicTitle, 0.25f);
                break;
            case "forest":
                audiosource.PlayOneShot(musicForest, 0.1f);
                break;
            case "town":
                audiosource.PlayOneShot(musicTown, 0.2f);
                break;
            case "mountain":
                audiosource.PlayOneShot(musicMountain, 0.126f);
                break;
            case "swamp":
                audiosource.PlayOneShot(musicSwamp, 0.1f);
                break;
            case "castle":
                audiosource.PlayOneShot(musicCastle, 0.174f);
                break;
            case "boss":
                audiosource.PlayOneShot(musicBoss, 0.2f);
                break;
        }
    }
    public static IEnumerator FadeMusic(AudioSource nAudioSource, float FadeTime, string clip)
    {
        float startVolume = nAudioSource.volume;
        while (nAudioSource.volume > 0)
        {
            nAudioSource.volume -= startVolume * Time.deltaTime / FadeTime;
            yield return null;
        }
        nAudioSource.Stop();
        nAudioSource.volume = startVolume;
        PlayMusic(clip);
    }
    

    public void SwitchMusic(string clip)
    {
        StartCoroutine(FadeMusic(audiosource, 0.5f, clip));
    }

    public static string GetMusic()
    {
        return musicName;
    }
    public static void setVolume(float nVolume)
    {
        Debug.Log(nVolume);
        audiosource.volume = nVolume;
    }
}
