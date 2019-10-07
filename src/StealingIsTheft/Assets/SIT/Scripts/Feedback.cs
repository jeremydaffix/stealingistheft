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

    public void GameOver()
    {
        Debug.LogWarning("GAME OVER");

        //StartCoroutine(GameOverScreen());
    }

    public void Victory()
    {
        Debug.LogWarning("Victory");

        if(!isGameOver)
            StartCoroutine(VictoryScreen());
    }


    IEnumerator GameOverScreen()
    {
        isGameOver = true;

        yield return new WaitForSeconds(3f);

        Image img = gameOverEffect.GetComponent<Image>();

        img.color = new Color(0f, 0f, 0f, 0f);

        gameOverEffect.SetActive(true);

        for(int i = 0; i < 20; ++i)
        {
            img.color = new Color(0f, 0f, 0f, i * 0.05f);
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene(0);
    }

    IEnumerator VictoryScreen()
    {
        yield return new WaitForSeconds(0.5f);

        Image img = victoryEffect.GetComponent<Image>();

        img.color = new Color(0f, 0f, 0f, 0f);

        victoryEffect.SetActive(true);

        for (int i = 0; i < 20; ++i)
        {
            img.color = new Color(0f, 0f, 0f, i * 0.05f);
            yield return new WaitForSeconds(0.05f);
        }

        //yield return new WaitForSeconds(3f);

        //SceneManager.LoadScene(0);
    }
}
