using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip playerHit, playerDeath, playerSwing, playerBow, playerHealth, confirm, deconfirm, pause, hover;
    static AudioSource audiosource;

    // Start is called before the first frame update
    void Start()
    {
        playerHit = Resources.Load<AudioClip>("player_hit");
        playerDeath = Resources.Load<AudioClip>("player_death");
        playerSwing = Resources.Load<AudioClip>("player_swing");
        playerBow = Resources.Load<AudioClip>("player_bow");
        playerHealth = Resources.Load<AudioClip>("healthpickup");
        hover = Resources.Load<AudioClip>("hover");
        pause = Resources.Load<AudioClip>("pause");
        confirm = Resources.Load<AudioClip>("confirm");
        deconfirm = Resources.Load<AudioClip>("deconfirm");

        audiosource = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "hit":
                audiosource.PlayOneShot(playerHit, 0.5f);
                break;
            case "bow":
                audiosource.PlayOneShot(playerBow, 0.5f);
                break;
            case "swing":
                audiosource.PlayOneShot(playerSwing, 0.2f);
                break;
            case "death":
                audiosource.PlayOneShot(playerDeath, 0.7f);
                break;
            case "health":
                audiosource.PlayOneShot(playerHealth, 0.1f);
                break;
            case "hover":
                audiosource.PlayOneShot(hover, 0.2f);
                break;
            case "confirm":
                audiosource.PlayOneShot(confirm, 0.5f);
                break;
            case "deconfirm":
                audiosource.PlayOneShot(deconfirm, 0.5f);
                break;
            case "pause":
                audiosource.PlayOneShot(pause, 0.5f);
                break;
        }
    }
}
