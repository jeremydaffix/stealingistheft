using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// https://opengameart.org/content/cc0-retro-music
// https://opengameart.org/content/80-cc0-creature-sfx
// https://opengameart.org/content/512-sound-effects-8-bit-style
// https://opengameart.org/content/rewind



public class SoundSystem : MonoBehaviour
{

    public AudioSource sourceSound; // sounds
    public AudioSource sourceMusic; // music

    public AudioClip begin, take, keeper, eat, sleep, win, drink, boost, dress, alarm, collision;
    public AudioClip musicBegin, musicGame, musicGameOver, musicWin;

    //AudioClip[] allMusic;
    //int currentMusic = -1;
    AudioClip music;

    public static SoundSystem inst;



    void Start()
    {
        inst = this;

        //allMusic = Resources.LoadAll<AudioClip>("Music");

        //Debug.Log(allMusic.Length + " musics");
    }


    void FixedUpdate()
    {
        /*if ((music == null || currentMusic == -1 || !sourceMusic.isPlaying) && sourceMusic.isActiveAndEnabled) { // si pas de musique en train de jouer

            int newMusic = -1;

            do
            {

                newMusic = GameController.RandomMakoto(0, allMusic.Length - 1);

            } while (newMusic == currentMusic); // différente de la précédente

            currentMusic = newMusic;
            music = allMusic[currentMusic];

            sourceMusic.PlayOneShot(music);

            Debug.Log("Playing " + music.name);
        }*/
    }


    /*public void RestartMusic()
    {

        music = null;
        sourceMusic.Stop();
    }*/


    public void PlayBegin()
    {
       sourceSound.PlayOneShot(begin);
    }

    public void PlayTake()
    {
        sourceSound.PlayOneShot(take);
    }

    public void PlayKeeper()
    {
        sourceSound.PlayOneShot(keeper);
    }

    public void PlayEat()
    {
        sourceSound.PlayOneShot(eat);
    }

    public void PlaySleep()
    {
        sourceSound.PlayOneShot(sleep);
    }

    public void PlayWin()
    {
        sourceSound.PlayOneShot(win);
    }

    public void PlayDrink()
    {
        sourceSound.PlayOneShot(drink);
    }

    public void PlayBoost()
    {
        sourceSound.PlayOneShot(boost);
    }

    public void PlayDress()
    {
        sourceSound.PlayOneShot(dress);
    }

    public void PlayAlarm()
    {
        sourceSound.PlayOneShot(alarm);
    }

    public void PlayCollision()
    {
        sourceSound.PlayOneShot(collision, 0.20f);
    }


    public void PlayMusicBegin()
    {
        sourceMusic.Stop();
        sourceMusic.clip = musicBegin;
        sourceMusic.Play();
    }

    public void PlayMusicGame()
    {
        sourceMusic.Stop();
        sourceMusic.clip = musicGame;
        sourceMusic.Play();
    }

    public void PlayMusicGameOver()
    {
        sourceMusic.Stop();
        sourceMusic.clip = musicGameOver;
        sourceMusic.Play();
    }

    public void PlayMusicWin()
    {
        sourceMusic.Stop();
        sourceMusic.clip = musicWin;
        sourceMusic.Play();
    }

    public void StopMusic()
    {
        sourceMusic.clip = null;
        sourceMusic.Stop();
    }
}

