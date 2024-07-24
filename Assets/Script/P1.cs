using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class P1 : MonoBehaviour
{
    public AudioSource audio;
    public AudioSource Raudio;

    public static P1 instance;
    public P2 player;
    public GameManager manager;

    private Animator myAnim;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public float speed;
    bool isWalking = false;
    bool isRun = false;
    bool isBack = false;
    bool kickOne = false;
    bool isPunch = false;
    bool isAttack = false;
    bool isJump = false;
    bool isDie = false;
    bool isWin = false;

    bool react = false;

    float P1xPosition = 0;
    float P1yPosition = 0;
  

    float P2xPosition = 0;
    int beatCounter = 0;

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
            isWalking = Input.GetKey(KeyCode.D);
            if (isWalking)
            {
                if (P1xPosition < 1.85)
                {
                    transform.Translate(Vector3.forward * speed * Time.deltaTime);
                }
            }
            myAnim.SetBool("WALK", isWalking);

            isRun = Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift);
            if (isRun)
            {
                if (P1xPosition < 1.85)
                {
                    transform.Translate(Vector3.forward * speed * 2 * Time.deltaTime);
                }
                
            }
            myAnim.SetBool("RUN", isRun);

            isBack = Input.GetKey(KeyCode.A);
            if (isBack)
            {
                if (P1xPosition > -2.95)
                {
                    transform.Translate(Vector3.back * speed * Time.deltaTime);
                }
                
            }
            myAnim.SetBool("W_BACK", isBack);

            //atatck anim
            P1_attackAnim();

            isJump = Input.GetKey(KeyCode.W);
            myAnim.SetBool("JUMP", isJump);


            //p2 pos
            P2xPosition = P2.instance.getP2_x();

            //P1 pos
            P1xPosition = transform.position.x;
            P1yPosition = transform.position.y;


            setPosEachFrame(P1xPosition, P1yPosition);

            setRotatonEachFrame();

            checkP1_p2_pos();

            checkBeatScore();
            aniCheck = false;

        }
        

    }

    
    void checkP1_p2_pos()
    {
        float pos_diff = Mathf.Abs(P1xPosition - P2xPosition);
        

        if ((pos_diff < 1.35 )&& (react == false))
        {
            if (aniCheck==true) { 
                P1_attackAnim();
                P2.instance.P2React();
            }
        }

    }

    public void AnimationComplete()
    {
        aniCheck = true;
    }


    void checkBeatScore()
    {
        if (beatCounter > 9)
        {
            isInputEnabled = false;
            isDie = true;
            myAnim.SetBool("DIE", isDie);
            beatCounter = 0;
            P2.instance.win();
        }
    }

    public void win()
    {
        isWin = true;
        myAnim.SetBool("WIN", isWin);
        GameManager.instance.GameOver();
        
    }

    public int getBeatCount()
    {
        return beatCounter;
    }

    public void attackAudiio()
    {
        
         audio.Play();
    }

    public void ReactAudiio()
    {

        Raudio.Play();
    }


    void P1_attackAnim()
    {
        isPunch = Input.GetKey(KeyCode.J);
        myAnim.SetBool("Punch", isPunch);

        kickOne = Input.GetKey(KeyCode.H);
        myAnim.SetBool("C_KICK", kickOne);

        myAnim.SetBool("com_ATTACK", isAttack);
        isAttack = Input.GetKey(KeyCode.K);
    }

    void setPosEachFrame(float newX, float newY)
    {
        float newZ = (float)-6.15;

        transform.position = new Vector3(newX, newY, newZ);
    }

    void setRotatonEachFrame()
    {
        float newYRotation = 90.0f;

        // Create a new rotation with the desired y-axis rotation
        Quaternion newRotation = Quaternion.Euler(0f, newYRotation, 0f);

        // Apply the new rotation to the object
        transform.rotation = newRotation;
    }

    public void P1React()
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

    public float getP1_x()
    {
        return P1xPosition;
    }

    
}
