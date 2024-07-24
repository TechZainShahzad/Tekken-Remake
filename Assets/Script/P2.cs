using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class P2 : MonoBehaviour
{

    public AudioSource audio;
    public AudioSource Raudio;

    public static P2 instance;
    public GameManager manager;
    public P1 player;

    float P2xPosition = 0;
    float P2yPosition = 0;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private Animator myAnim;

    
    public float speed;
    bool isRun = false;
    bool kickOne = false;
    bool isPunch = false;
    bool isAttack = false;
    bool isWalking = false;
    bool isBack = false;
    bool isJump = false;
    bool isWin = false;

    bool react = false;

    float P1xPosition = 0;

    int beatCounter = 0;
    private bool isDie;

    private bool aniCheck = false;

    private bool isInputEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInputEnabled)
        {
            
            isWalking = Input.GetKey(KeyCode.LeftArrow);
            if (isWalking)
            {
                if (P2xPosition > -2.95)
                {
                    transform.Translate(Vector3.forward * speed * Time.deltaTime);
                }
                
            }
            myAnim.SetBool("WALK", isWalking);

            isRun = Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightShift);
            if (isRun)
            {
                if (P2xPosition > -2.95)
                {
                    transform.Translate(Vector3.forward * speed * 2 * Time.deltaTime);
                }
                
            }
            myAnim.SetBool("RUN", isRun);

            isBack = Input.GetKey(KeyCode.RightArrow);
            if (isBack)
            {
                if (P2xPosition < 1.85)
                {
                    transform.Translate(Vector3.back * speed * Time.deltaTime);
                }
                
            }
            myAnim.SetBool("W_BACK", isBack);

            P2_attackAnim();

            isJump = Input.GetKey(KeyCode.UpArrow);
            myAnim.SetBool("JUMP", isJump);



            //p1 pos
            P1xPosition = P1.instance.getP1_x();

            //P2 pos
            P2xPosition = transform.position.x;
            P2yPosition = transform.position.y;

            setPosEachFrame(P2xPosition, P2yPosition);

            setRotatonEachFrame();

            checkP1_p2_pos();

            checkBeatScore();

            aniCheck = false;
        }
    }

    void checkP1_p2_pos()
    {
        float pos_diff = Mathf.Abs(P1xPosition - P2xPosition);


        if ((pos_diff < 1.35) && (react == false))
        {
            if (aniCheck == true)
            {
                P2_attackAnim();
                P1.instance.P1React();
            }
        }

    }

    public void AnimationComplete()
    {
        aniCheck = true;
    }

    public void attackAudiio()
    {
        audio.Play();
    }

    public void ReactAudiio()
    {

        Raudio.Play();
    }

    void checkBeatScore()
    {
        if (beatCounter > 9)
        {
            isInputEnabled = false;
            isDie = true;
            myAnim.SetBool("DIE", isDie);
            P1.instance.win();
        }
    }

    public int getBeatCount()
    {
        return beatCounter;
    }

    public void win()
    {
        isWin = true;
        myAnim.SetBool("WIN", isWin);
        GameManager.instance.GameOver();
    }
   

    public void P2React()
    {
        if (!react)
        {
            react = true;
            myAnim.SetBool("React", react);
            beatCounter++;
            StartCoroutine(ReactCoroutine());
        }
    }

    IEnumerator ReactCoroutine()
    {
        yield return new WaitForSeconds(0.1f);

        react = false;
        myAnim.SetBool("React", react);
    }





    void P2_attackAnim()
    {
        kickOne = Input.GetKey(KeyCode.Keypad1);
        myAnim.SetBool("S_KICK", kickOne);


        isPunch = Input.GetKey(KeyCode.Keypad2);
        myAnim.SetBool("Punch", isPunch);

        isAttack = Input.GetKey(KeyCode.Keypad3);
        myAnim.SetBool("MMAKICK", isAttack);
    }


    

    public float getP2_x()
    {
        return P2xPosition;
    }

    void setPosEachFrame(float newX, float newY)
    {
        float newZ = (float)-6.15;

        transform.position = new Vector3(newX, newY, newZ);
    }

    void setRotatonEachFrame()
    {
        float newYRotation = -90.0f;

        // Create a new rotation with the desired y-axis rotation
        Quaternion newRotation = Quaternion.Euler(0f, newYRotation, 0f);

        // Apply the new rotation to the object
        transform.rotation = newRotation;
    }
}
