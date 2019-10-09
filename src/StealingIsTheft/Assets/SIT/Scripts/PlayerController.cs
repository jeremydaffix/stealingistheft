using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    enum PlayerDirection
    {
        Left,
        TopLeft,
        Top,
        TopRight,
        Right,
        BottomRight,
        Bottom,
        BottomLeft,
        None
    }

    private PlayerDirection currentDirection = PlayerDirection.None;

    public static PlayerController PlayerInstance = null;

    [SerializeField]
    private float speed = 0.1f;

    [SerializeField]
    GameObject shoppingList;

    [SerializeField]
    GameObject drunkEffect;

    private float speedBoost = 1f;

    float lastCollisionSound = 0f;


    // character properties

    bool isStealing = false;

    bool isNaked = true;
    bool isDehydrated = true;
    bool isHungry = true;
    bool isDrunk = false;
    bool isWearingAntiTheft = true;

    bool hasShoppingCart = false;

    bool hasPants = false;
    bool hasShoes = false;
    bool hasUnderwear = false;
    bool hasTshirt = false;
    bool hasJacket = false;
    bool hasSunglasses = false;
    bool hasHat = false;

    bool hasPincers = false;

    bool isTalking = false;


    public bool IsStealing { get => isStealing; set => isStealing = value; }
    public bool IsNaked { get => isNaked; set => isNaked = value; }
    public bool IsDehydrated { get => isDehydrated; set => isDehydrated = value; }
    public bool IsHungry { get => isHungry; set => isHungry = value; }
    public bool HasPants { get => hasPants; set => hasPants = value; }
    public bool HasShoes { get => hasShoes; set => hasShoes = value; }
    public bool HasUnderwear { get => hasUnderwear; set => hasUnderwear = value; }
    public bool HasTshirt { get => hasTshirt; set => hasTshirt = value; }
    public bool HasJacket { get => hasJacket; set => hasJacket = value; }
    public bool HasSunglasses { get => hasSunglasses; set => hasSunglasses = value; }
    public bool HasHat { get => hasHat; set => hasHat = value; }
    public bool HasPincers { get => hasPincers; set => hasPincers = value; }
    public bool HasShoppingCart { get => hasShoppingCart; set => hasShoppingCart = value; }
    public bool IsDrunk { get => isDrunk; set => isDrunk = value; }
    public bool IsTalking { get => isTalking; set => isTalking = value; }
    public bool IsWearingAntiTheft { get => isWearingAntiTheft; set => isWearingAntiTheft = value; }
    public Animator Anim { get => anim; set => anim = value; }



    // components

    Rigidbody2D rb;
    Animator anim;
    CinemachineImpulseSource imp;

    void Awake()
    {
        PlayerInstance = this;
    }

    void Start()
    {
        //PlayerInstance = this;

        rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        imp = GetComponent<CinemachineImpulseSource>();
    }

    void Update()
    {
        CalcDirection();

        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }




    void CalcDirection()
    {
        if (Input.GetAxisRaw("Horizontal") > 0.1f) // right
        {
            if (Input.GetAxisRaw("Vertical") > 0.1f) // top
            {
                currentDirection = PlayerDirection.TopRight;
            }

            else if (Input.GetAxisRaw("Vertical") < -0.1f) // bottom
            {
                currentDirection = PlayerDirection.BottomRight;
            }

            else
            {
                currentDirection = PlayerDirection.Right;
            }
        }

        else if (Input.GetAxisRaw("Horizontal") < -0.1f) // left
        {
            if (Input.GetAxisRaw("Vertical") > 0.1f) // top
            {
                currentDirection = PlayerDirection.TopLeft;
            }

            else if (Input.GetAxisRaw("Vertical") < -0.1f) // bottom
            {
                currentDirection = PlayerDirection.BottomLeft;
            }

            else
            {
                currentDirection = PlayerDirection.Left;
            }
        }

        else
        {
            if (Input.GetAxisRaw("Vertical") > 0.1f) // top
            {
                currentDirection = PlayerDirection.Top;
            }

            else if (Input.GetAxisRaw("Vertical") < -0.1f) // bottom
            {
                currentDirection = PlayerDirection.Bottom;
            }

            else // idle
            {
                currentDirection = PlayerDirection.None;
            }
        }

        if(Input.GetButton("Fire3") && !isDrunk && !isDehydrated)
        {
            speedBoost = 2.0f;
        }

        else
        {
            speedBoost = 1f;
        }
    }


    void MovePlayer()
    {
        if(currentDirection != PlayerDirection.None && !IsTalking && !IsStealing)
        {
            Vector3 moving = new Vector3();

            if (currentDirection == PlayerDirection.Left || currentDirection == PlayerDirection.BottomLeft || currentDirection == PlayerDirection.TopLeft)
                moving.x = -1f;

            else if (currentDirection == PlayerDirection.Right || currentDirection == PlayerDirection.BottomRight || currentDirection == PlayerDirection.TopRight)
                moving.x = 1f;


            if (currentDirection == PlayerDirection.Top || currentDirection == PlayerDirection.TopLeft || currentDirection == PlayerDirection.TopRight)
                moving.y = 1f;

            else if (currentDirection == PlayerDirection.Bottom || currentDirection == PlayerDirection.BottomLeft || currentDirection == PlayerDirection.BottomRight)
                moving.y = -1f;


            if(isDrunk)
            {
                moving = moving * Random.Range(0f, 0.8f);
                //Camera.main.transform.Rotate(new Vector3(0f, 0f, 50f));
            }


            Vector3 newPos = transform.position + moving * speed * speedBoost;

            var dir = newPos - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            rb.SetRotation(Quaternion.AngleAxis(angle - 90f, Vector3.forward));

            //transform.LookAt(newPos);

            rb.MovePosition(newPos);

            Anim.SetBool("isWalking", true);
        }

        else
        {
            Anim.SetBool("isWalking", false);
        }
    }

    IEnumerator Steal()
    {
        IsStealing = true;
        yield return new WaitForSeconds(1f);
        IsStealing = false;
    }

    IEnumerator Talk()
    {
        IsTalking = true;
        yield return new WaitForSeconds(2f);
        IsTalking = false;
    }


    public bool Take(string nameObject)
    {
        bool objectTaken = true;

        switch(nameObject)
        {
            case "Water":
                IsDehydrated = false;
                Say("I feel better, I could even run...");
                SoundSystem.inst.PlayBoost();
                break;

            case "Beer":
                IsDehydrated = true;
                IsDrunk = true;
                Say("Shit, I did it again");
                drunkEffect.SetActive(true);
                SoundSystem.inst.PlayDrink();
                break;

            case "Crisps":
                IsHungry = false;
                Say("Yummy yummy");
                SoundSystem.inst.PlayEat();
                break;

            case "Raclette":
                IsHungry = false;
                Say("YUMMY YUMMY");
                SoundSystem.inst.PlayEat();
                break;

            case "Aspirin":
                IsDehydrated = false;
                isDrunk = false;
                Say("I feel better, I could even run...");
                drunkEffect.SetActive(false);
                SoundSystem.inst.PlayBoost();
                break;

            case "Cough Syrup":
                //IsDehydrated = true;
                IsDrunk = true;
                Say("I feel a little tired");
                drunkEffect.SetActive(true);
                SoundSystem.inst.PlaySleep();
                break;

            case "Metallica T-Shirt":
                hasTshirt = true;
                ShowShoppingListElement(4);
                Say("Great band");
                SoundSystem.inst.PlayTake();
                break;

            case "Sneakers":
                hasShoes = true;
                ShowShoppingListElement(1);
                SoundSystem.inst.PlayTake();
                break;

            case "Top Hat":
                hasHat = true;
                ShowShoppingListElement(6);
                Say("Looking like a gentleman");
                SoundSystem.inst.PlayTake();
                break;

            case "Underpants":
                hasUnderwear = true;
                ShowShoppingListElement(2);
                //Say("Bye bye freedom");
                SoundSystem.inst.PlayTake();
                break;

            case "Jeans":
                HasPants = true;
                ShowShoppingListElement(3);
                SoundSystem.inst.PlayTake();
                break;

            case "Yellow Jacket":
                hasJacket = true;
                ShowShoppingListElement(5);
                SoundSystem.inst.PlayTake();
                break;


            case "Sunglasses":

                HasSunglasses = true;

                if(hasSunglasses && hasPincers)
                    ShowShoppingListElement(7);

                Say("No more red eyes");
                SoundSystem.inst.PlayTake();

                break;

            case "Pincers":

                hasPincers = true;

                if (hasSunglasses && hasPincers)
                    ShowShoppingListElement(7);

                Say("Hasta la vista anti-theft devices");
                SoundSystem.inst.PlayTake();

                break;


            default:
                objectTaken = false;
                break;
        }


        if(objectTaken)
        {
            //SoundSystem.inst.PlayTake();
        }


        return objectTaken;
    }


    void ShowShoppingListElement(int index)
    {
        TextMeshProUGUI tmp = shoppingList.transform.GetChild(index).GetComponent<TextMeshProUGUI>();

        if(tmp != null)
        {
            tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, 1f);
        }
    }

    public void Say(string msg)
    {
        Feedback.Instance.ShowSimpleMessage(new Vector2(transform.position.x, transform.position.y + 2f), msg, new Color(1f, 1f, 1f), 22, 0.75f/*, transform*/);
        StartCoroutine(Talk());
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if((Time.time - lastCollisionSound) > 0.5f)
        {
            SoundSystem.inst.PlayCollision();
            lastCollisionSound = Time.time;
        }
    }
}
