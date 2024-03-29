﻿using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Feedback : MonoBehaviour
{
    public static Feedback Instance = null;

    [SerializeField]
    private GameObject simpleMessagePrefab;

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject playerAction;
    [SerializeField]
    private TextMeshPro playerActionText;

    [SerializeField]
    private GameObject gameOverEffect;

    [SerializeField]
    private GameObject victoryEffect;

    [SerializeField]
    private CinemachineVirtualCamera vcam;

    bool isGameOver = false;


    void Start()
    {
        Instance = this;
    }


    public void ShowSimpleMessage(Vector2 pos, string txt, Color col, int size = 48, float duration = 0.25f, Transform p = null)
    {
        GameObject msg = Instantiate(simpleMessagePrefab, new Vector3(pos.x, pos.y, -5f), new Quaternion());

        if(p != null)
        {
            msg.transform.parent = p;
        }

        TextMeshPro tmp = msg.GetComponent<TextMeshPro>();

        if(tmp != null)
        {
            //tmp.text = txt;
            tmp.color = col;
            tmp.fontSize = size;

            StartCoroutine(SimpleMessageAnim(tmp, txt, duration));
        }
    }


    private IEnumerator SimpleMessageAnim(TextMeshPro tmp, string txt, float duration)
    {
        for(int i = 0; i < txt.Length; ++i)
        {
            tmp.text += txt[i];
            yield return new WaitForSeconds(0.075f);
        }

        yield return new WaitForSeconds(duration);

        for(int j = 0; j < 20; ++j)
        {
            tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, 1f - (j * 0.10f));

            yield return new WaitForSeconds(0.025f);
        }

        Destroy(tmp.gameObject);
    }


    public void ShowPlayerAction(string txt)
    {
        playerAction.transform.position = player.transform.position + new Vector3(-3f, 3f, 0f);

        playerAction.SetActive(true);
        playerActionText.text = txt;
    }

    public void HidePlayerAction()
    {
        playerAction.SetActive(false);
        playerActionText.text = "";
    }

    public void GameOver(Transform tr = null)
    {
        Debug.LogWarning("GAME OVER");

        StartCoroutine(GameOverScreen());

        if (tr != null)
        {
            //vcam.Follow = tr;
            //vcam.LookAt = tr;
        }
    }

    public void Victory()
    {
        Debug.LogWarning("Victory");

        if (!isGameOver)
            StartCoroutine(VictoryScreen());
    }


    IEnumerator GameOverScreen()
    {
        isGameOver = true;

        yield return new WaitForSeconds(3f);

        SoundSystem.inst.PlayMusicGameOver();

        Image img = gameOverEffect.GetComponent<Image>();
        TextMeshProUGUI tmp1 = gameOverEffect.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI tmp2 = gameOverEffect.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        img.color = new Color(0f, 0f, 0f, 0f);

        gameOverEffect.SetActive(true);

        for(int i = 0; i < 20; ++i)
        {
            img.color = new Color(0f, 0f, 0f, i * 0.05f);
            tmp1.color = new Color(tmp1.color.r, tmp1.color.g, tmp1.color.b, i * 0.05f);
            tmp2.color = new Color(tmp2.color.r, tmp2.color.g, tmp2.color.b, i * 0.05f);

            yield return new WaitForSeconds(0.05f);
        }

        vcam.enabled = false;
        PlayerController.PlayerInstance.GetComponent<SpriteRenderer>().enabled = false;
        PlayerController.PlayerInstance.GetComponent<PlayerController>().enabled = false;
        PlayerController.PlayerInstance.transform.position = new Vector3(6.24f, -3.34f, 0f);
        Camera.main.transform.position = new Vector3(6.24f, -3.34f, -10f);

        yield return new WaitForSeconds(3f);

        for (int k = 0; k < 20; ++k)
        {
            img.color = new Color(img.color.r, img.color.g, img.color.b, 1f - k * 0.05f);
            tmp1.color = new Color(tmp1.color.r, tmp1.color.g, tmp1.color.b, 1f - k * 0.05f);
            tmp2.color = new Color(tmp2.color.r, tmp2.color.g, tmp2.color.b, 1f - k * 0.05f);

            yield return new WaitForSeconds(0.05f);
        }

        SceneManager.LoadScene(0);
    }

    IEnumerator VictoryScreen()
    {
        PlayerController.PlayerInstance.Say("Freeeedoooom");

        SoundSystem.inst.PlayWin();

        yield return new WaitForSeconds(1.5f);

        SoundSystem.inst.PlayMusicWin();

        Image img = victoryEffect.GetComponent<Image>();
        TextMeshProUGUI tmp1 = victoryEffect.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI tmp2 = victoryEffect.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        img.color = new Color(0f, 0f, 0f, 0f);

        victoryEffect.SetActive(true);

        for (int i = 0; i < 20; ++i)
        {
            img.color = new Color(0f, 0f, 0f, i * 0.05f);
            tmp1.color = new Color(tmp1.color.r, tmp1.color.g, tmp1.color.b, i * 0.05f);
            tmp2.color = new Color(tmp2.color.r, tmp2.color.g, tmp2.color.b, i * 0.05f);

            yield return new WaitForSeconds(0.05f);
        }

        //yield return new WaitForSeconds(3f);

        //SceneManager.LoadScene(0);
    }
}
