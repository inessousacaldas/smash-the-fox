using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitParticles : MonoBehaviour {

    private float timeCounter;
    public float radius = 5f;
    public float speed = 30f;
    // Use this for initialization

    void Start () {
        this.gameObject.SetActive(false);	
	}
	
	// Update is called once per frame
	void Update () {

        timeCounter += Time.deltaTime * speed;
        float x = Mathf.Cos(timeCounter);
        
        transform.localPosition = Quaternion.AngleAxis(timeCounter, Vector3.up) * new Vector3(radius, transform.localPosition.y);

    }
}
