using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bouton : MonoBehaviour
{
    //VARIABLES
    public int plaqueNumber;
    public bool isEnd;

    //GAMEOBJECT
    public GameObject manager;
    public GameObject bloque;

    //AUTRES
    [HideInInspector] public PushObject ScriptEnter;
    [HideInInspector] public PushObject ScriptExit;
    [HideInInspector] public cubeManager cubeManagerScript;
    [HideInInspector] public bloques bloqueScript;

    // Start is called before the first frame update
    void Start()
    {
        //recup le script
        if (isEnd)
        {
            bloqueScript = bloque.GetComponent<bloques>();
        }
        else
        {
            cubeManagerScript = manager.GetComponent<cubeManager>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {

        ScriptEnter = other.gameObject.transform.GetChild(1).gameObject.GetComponent<PushObject>();
        if (ScriptEnter != null )
        {
            if(plaqueNumber == ScriptEnter.cubeNumber)
            {
                if (!isEnd)
                {
                    cubeManagerScript.etatCubes[plaqueNumber] = true;
                }
                else
                {
                    bloqueScript.isActive = true;
                }
                
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        
        ScriptExit = other.gameObject.transform.GetChild(1).gameObject.GetComponent<PushObject>();
        if (ScriptExit != null)
        {
            if (plaqueNumber == ScriptExit.cubeNumber)
            {
                if (!isEnd)
                {
                    cubeManagerScript.etatCubes[plaqueNumber] = false;
                }
                else
                {
                    bloqueScript.isActive = false;
                }
            }
        }
    }
}
