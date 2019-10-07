using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMessageOnWalk : MonoBehaviour
{
    [SerializeField]
    private string message;

    [SerializeField]
    private Vector2 position = new Vector2();

    [SerializeField]
    private Color color = Color.white;

    [SerializeField]
    private int size = 48;

    [SerializeField]
    private float duration = 0.25f;

    [SerializeField]
    bool autoDestroy = true;

    [SerializeField]
    SimpleMessageOnWalk nextMessage = null;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            Feedback.Instance.ShowSimpleMessage(position, message, color, size, duration);

            if (nextMessage != null)
            {
                Invoke("EnableNextMessage", 5f);
            }

            if (autoDestroy)
                Destroy(gameObject);
        }
    }

    private void EnableNextMessage() // bug: not called because object destroyed
    {
        nextMessage.gameObject.SetActive(true);
    }

}
