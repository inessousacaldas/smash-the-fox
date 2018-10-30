using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour {

    [SerializeField]
    private ParticleSystem hitParticles;
    private bool hide;
    private bool alive;
    private bool kill;
    private Animator animator;
    private int animationState;
    private bool move;
    private float tiltAngle = 0;
    private float timeStamp;
    public bool Alive
    {
        get
        {
            return alive;
        }

        set
        {
            alive = value;
        }
    }

    public bool Kill
    {
        get
        {
            return kill;
        }

        set
        {
            kill = value;
        }
    }

    public bool Hide
    {
        get
        {
            return hide;
        }

        set
        {
            hide = value;
        }
    }

    public float TiltAngle
    {
        get
        {
            return tiltAngle;
        }

        set
        {
            tiltAngle = value;
        }
    }

    public float TimeStamp
    {
        get
        {
            return timeStamp;
        }

        set
        {
            timeStamp = value;
        }
    }

    public int State
    {
        get
        {
            return state;
        }

        set
        {
            state = value;
        }
    }

    public float smooth = 15f;

    public bool jump;

    private int state; //defines behaviour

    private bool points; //if hit, gives points

    // Use this for initialization
    void Start () {

        points = false;
        jump = false;
        move = true;
        //animationState = 0;
        animator = GetComponent<Animator>();
        TiltAngle = 0f;
        
    }
	
	// Update is called once per frame
	void Update () {

        if (animationState == 0)
        {
            transform.rotation = Quaternion.identity;
        }

       /* if(state == 4)
        {
            hideMovement();
        } */
         
        animator.SetInteger("state", animationState);
        Quaternion target = Quaternion.Euler(-TiltAngle, 0, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);

    }

    void OnTriggerEnter(Collider other)
    {
        print("Collide with " + other.tag);
        if(other.tag == "Hammer" && alive && state == 1)
        {
            state = 3;
            kill = true;
            hide = false;
            move = false;
            animationState = 2;
            points = true;
        }
    }

   public void animatorSetState(int _animationState)
    {
        if (_animationState != 0)
            move = false;
        else
        {
            transform.rotation = Quaternion.identity;
            tiltAngle = 0;
        }
        animationState = _animationState;

    }

    public void hideMovement()
    {
        print("Hide event called");
        animationState = 3;
        move = false;
        kill = false;
        state = -1;
    }

    public void tiltMole(float ang)
    {
        tiltAngle = ang;
        if (ang == 100)
        {
            tiltAngle = 160;
            jump = true;
            timeStamp = Time.time;
        }
            

    }

    public void allowMovement()
    {
        move = true;
        
    }

    public bool canMove()
    {
        if (animationState == 0)
            return true;

        return move;
    }

    public void resetPosture()
    {
        transform.rotation = Quaternion.identity;
        tiltAngle = 0;
    }

    public void setState(int _state)
    {
        State = _state;
    }

    public void playParticles()
    {
        hitParticles.gameObject.SetActive(true);
    }

    public void stopParticles()
    {
        hitParticles.gameObject.SetActive(false);
    }

    public bool givePoints()
    {
        if (points)
        {
            points = false;
            return true;
        }

        return false;
    }

}
