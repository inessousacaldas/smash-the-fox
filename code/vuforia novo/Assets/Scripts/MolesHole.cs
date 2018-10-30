using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MolesHole : MonoBehaviour {

    [SerializeField]
    string id;
    [SerializeField]
    Mole moleGood; //Gives points
    [SerializeField]
    Mole moleBad; //Takes points
    Mole[] mole;
    bool respawn;
    bool active;

    //Mole data
    /************************/
    private float timeAlive;
    private float timeStamp;
    private bool alive; //TODO: remove
    private bool hide;

    private Vector3 direction;

    private Transform start;
    private Transform end;
    [SerializeField]
    private Transform upPosition;
    [SerializeField]
    private Transform downPosition;
    [SerializeField]
    private Transform jumpPosition;
    private float speed = 20f;
    public float smooth = 15f;
    private float startTime;
    private float journeyLength;
    private bool available;
    private int currMole;
    
    public float TimeAlive
    {
        get
        {
            return timeAlive;
        }

        set
        {
            timeAlive = value;
        }
    }

    public float Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
        }
    }

    void Awake()
    {
        start = downPosition;
        end = downPosition;
    }

    // Use this for initialization
    void Start () {

        currMole = 0;

        mole = new Mole[2];

        mole[0] = Instantiate<Mole>(moleGood); ;
        mole[0].gameObject.SetActive(false);
        mole[0].transform.position = downPosition.position;

        mole[1] = Instantiate<Mole>(moleBad);
        mole[1].gameObject.SetActive(false);
        mole[1].transform.position = downPosition.position;

        timeStamp = Time.time;
        respawn = false;
        active = false;
        alive = false;
        hide = false;
        TimeAlive = 3f;

        startTime = Time.time;
        journeyLength = Vector3.Distance(upPosition.position, downPosition.position);
        available = true;
    }
	
	// Update is called once per frame
	void Update () {
        mole[currMole].smooth = smooth;
        move();
        controlMove();

        checkStatus();

        if (respawn)
        {
            giveLife();
        }

        if (hide)
        {
            hideMovement();
        }

	}

    public bool respawnMole(int type)
    {

        if (isActive() && available && !mole[currMole].Alive)
        {
            currMole = type;
            respawn = true;
            mole[currMole].gameObject.SetActive(true);
            mole[currMole].TiltAngle = 0f;
            return true;
        }
        //else if (!isActive())
        //{
        //    mole[0].gameObject.SetActive(false);
        //    mole[1].gameObject.SetActive(false);
        //}
        return false;
    }

    private bool isActive()
    {
        DefaultTrackableEventHandler handler = (DefaultTrackableEventHandler)GetComponentInParent(typeof(DefaultTrackableEventHandler));
        return handler.Active;
    }


    //Mole functions
    /********************************/
    void checkStatus()
    {
        //((mole[currMole].Alive && !mole[currMole].Kill && timeStamp <= Time.time) || mole[currMole].Hide))
        if (available && timeStamp <= Time.time && mole[currMole].State == 1)
        {
            hide = true;
            mole[currMole].Hide = false; //update
        }else if(mole[currMole].State == 4)
        {
            hide = true;
            mole[currMole].Hide = false; //update
        }
    }

    public void giveLife()
    {
        mole[currMole].Alive = true;
        mole[currMole].animatorSetState(1);
        mole[currMole].State = 1;

        print("Dei vida state " + mole[currMole].State);

        respawn = false;
        //timeStamp = Time.time + 20f;

        //Movement
        startTime = Time.time;
        start = downPosition;
        end = upPosition;
        available = false;

    }

    private void hideMovement()
    {

        mole[currMole].Alive = false; //The hammer cannot hit the mole
        mole[currMole].hideMovement();
        hide = false;
        available = false;
       
        //Movement
        start = upPosition;
        end = downPosition;

        startTime = Time.time;
    }


    void move()
    {
        if (!mole[currMole].canMove())
            return;
        
        if(mole[currMole].jump == true && mole[currMole].State == 1)
        {
            mole[currMole].jump = false; //Start moving down
            startTime = Time.time;
        }

        if (start != end)
        {
            float distCovered = (Time.time - startTime) * Speed;
            float fracJourney = distCovered / journeyLength;
            mole[currMole].transform.position = Vector3.Lerp(start.position, end.position, fracJourney);
        }
        else
        {
            available = true;
        }  
    }

    void controlMove()
    {

        if (mole[currMole].Alive && !available)
        {
            if(mole[currMole].transform.position == upPosition.position)
            {
                print("Cheguei ao topo com estado " + mole[currMole].State);

                if (mole[currMole].State == 1)
                {
                    timeStamp = Time.time + this.TimeAlive;
                    available = true;
                }   
            }
                
        }
        else if (!mole[currMole].Alive && !available)
        {
            
            if (mole[currMole].transform.position == downPosition.position)
            {
                print("Cheguei ao fundo " + mole[currMole].State);
                if(mole[currMole].State == -1)
                    available = true;

            }
        }
    }
}
