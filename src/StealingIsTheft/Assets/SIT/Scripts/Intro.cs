using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GoIntro());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GoIntro()
    {
        TextMeshProUGUI tmp = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI tmp2 = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        Image bgd = GetComponent<Image>();

        string txt = "[ stealing is theft ]";
        string txt2 = "You wake up in a supermarket fitting room.\nThe worst hangover of your life.\nNo memories. No self respect.\nNo CLOTHES.\nWhat could possibly go wrong?\n";

        tmp.text = txt;

        for (int j = 0; j < 20; ++j)
        {
            tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, j * 0.05f);

            yield return new WaitForSeconds(0.05f);
        }

        //yield return new WaitForSeconds(1f);

        for (int i = 0; i < txt2.Length; ++i)
        {
            tmp2.text += txt2[i];
            yield return new WaitForSeconds(0.035f);
        }

        yield return new WaitForSeconds(0.5f);

        for (int k = 0; k < 20; ++k)
        {
            bgd.color = new Color(bgd.color.r, bgd.color.g, bgd.color.b, 1f - k * 0.05f);
            tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, 1f - k * 0.05f);
            tmp2.color = new Color(tmp2.color.r, tmp2.color.g, tmp2.color.b, 1f - k * 0.05f);

            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(0.5f);


        gameObject.SetActive(false);
    }
}
