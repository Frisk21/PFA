using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class energieManager : MonoBehaviour
{
    //GAMEOBJECT
    public GameObject energie1;
    public GameObject energie2;
    public GameObject energie3;
    public GameObject energieact;

    //AUTRES
    [HideInInspector] public Energie energiescript1;
    [HideInInspector] public Energie energiescript2;
    [HideInInspector] public Energie energiescript3;
    [HideInInspector] public Energie energieactScript;

    //VARIABLES
    [HideInInspector] public bool isFinish;

    // Start is called before the first frame update
    void Start()
    {
        //recup scripts
        energiescript1 = energie1.GetComponent<Energie>();
        energiescript2 = energie2.GetComponent<Energie>();
        energiescript3 = energie3.GetComponent<Energie>();
        energieactScript = energieact.GetComponent<Energie>();

        //init variables
        isFinish = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (energiescript1.Active &&  energiescript2.Active && energiescript3.Active)
        {
            isFinish = true;
        }

        if (isFinish)
        {
            energieactScript.Active = true;
            //ouvre la porte
        }
    }
}
