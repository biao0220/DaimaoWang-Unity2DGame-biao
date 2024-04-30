using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static AudioClip pickCoin;
    public static AudioClip throwCoin;
    public static AudioSource audioSrc;
    public static AudioClip attack;
    public static AudioClip hit;
    public static AudioClip jump;
    public static AudioClip climb;

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        pickCoin = Resources.Load<AudioClip>("PickCoin");
        throwCoin = Resources.Load<AudioClip>("ThrowCoin");
        attack = Resources.Load<AudioClip>("Attack");
        hit = Resources.Load<AudioClip>("Hit");
        jump = Resources.Load<AudioClip>("Jump");
        climb = Resources.Load<AudioClip>("Climb");

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void PlayPickCoinClip()
    {
        audioSrc.PlayOneShot(pickCoin);
    }

    public static void PlayThrowCoinClip()
    {
        audioSrc.PlayOneShot(throwCoin);
    }

    public static void PlayAttackClip()
    {
        audioSrc.PlayOneShot(attack);
    }

    public static void PlayHitClip()
    {
        audioSrc.PlayOneShot(hit);
    }

    public static void PlayJumpClip()
    {
        audioSrc.PlayOneShot(jump);
    }

    public static void PlayClimbClip()
    {
        audioSrc.PlayOneShot(climb);
    }
}
