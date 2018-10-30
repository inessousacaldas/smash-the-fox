using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
 
public class VBStartGameHandler : MonoBehaviour, IVirtualButtonEventHandler
{

    private bool switchState = true;
    private GameObject vbButton;

    // Use this for initialization
    void Start()
    {
        // Register yourself as a handler for a button
        Object[] objects = FindSceneObjectsOfType(typeof(GameObject));
        foreach (Object vObject in objects)
        {
            GameObject gameObject = (GameObject)vObject;
            if (gameObject.name.Equals("VirtualButtonStartGame"))
            {
                vbButton = gameObject;
                Debug.Log("Found the VirtualButton GameObject!");
                VirtualButtonBehaviour vbuttonBehavior = (VirtualButtonBehaviour)gameObject.GetComponent(typeof(VirtualButtonBehaviour));
                vbuttonBehavior.RegisterEventHandler(this);
                Debug.Log("I'm registered!");
            }
            Debug.Log(gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void switchColor()
    {
        if (switchState)
        {
            switchState = false;
            gameObject.GetComponent<Light>().color = Color.blue;
        }
        else
        {
            switchState = true;
            gameObject.GetComponent<Light>().color = Color.green;
        }
    }

    #region IVirtualButtonEventHandler implementation
    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        Debug.Log("Button pressed!");
        switchColor();
        //throw new System.NotImplementedException ();
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {

        //throw new System.NotImplementedException ();
    }
    #endregion
}