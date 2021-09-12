using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{

    public AudioClip[] GAME_BGM;
    private int bgm_num;
    private GameObject gameObj;
    private AudioSource Sound_Player;

    private void Awake()
    {
        Sound_Player = this.GetComponent<AudioSource>();
    }
    void start()
    {
        bgm_num = 0;
        Sound_Player.PlayOneShot(GAME_BGM[bgm_num], 0.8f);
    }
    void OnTriggerEnter(Collider other)
    {
        gameObj = other.gameObject;
        if (gameObj.CompareTag("Ocean"))
        {
            Sound_Player.Stop();
            bgm_num = 1;
            Sound_Player.PlayOneShot(GAME_BGM[bgm_num], 0.8f);
            Debug.Log("1-사운드 발생!");
        }
        else if (gameObj.CompareTag("Field"))
        {
            Sound_Player.Stop();
            bgm_num = 2;
            Sound_Player.PlayOneShot(GAME_BGM[bgm_num], 0.8f);
            Debug.Log("2-사운드 발생!");
        }
        else if (gameObj.CompareTag("Nomal"))
        {
            Sound_Player.Stop();
            bgm_num = 0;
            Sound_Player.PlayOneShot(GAME_BGM[bgm_num], 0.8f);
            Debug.Log("0-사운드 발생!");
        }
    }
}
