using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour {

    private int points;
    private int hitPoints = 10;

    public int Points
    {
        get
        {
            return points;
        }

        set
        {
            points = value;
        }
    }

    // Use this for initialization
    void Start () {

        Points = 0;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        
        print(" martelo Collide with " + other.tag);

        if (other.tag == "Enemy")
        {
            Mole mole = other.gameObject.GetComponent<Mole>();
            print("ALIVE " + mole.Alive);
            if (mole.givePoints())
            {
                Points += hitPoints;
            }
                
        }

        if (other.tag == "Friend")
        {
            Mole mole = other.gameObject.GetComponent<Mole>();
            print("ALIVE " + mole.Alive);
            if (mole.Alive)
            {
                Points -= hitPoints;
            }

        }
    }


}
