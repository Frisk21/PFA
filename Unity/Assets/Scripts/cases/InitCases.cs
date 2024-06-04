using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UIElements;

public class InitCases : MonoBehaviour
{

    //VARIABLES
    [HideInInspector] public bool Init;

    //GAMEOBJECT
    public GameObject CasesOui;
    public GameObject CasesNon;

    // Start is called before the first frame update
    void Start()
    {
        //initialisation des variables
        Init = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Init)
        {
            //initialise toute les cases a 0
            foreach (Transform child in CasesOui.transform)
            {
                child.transform.gameObject.GetComponent<casses>().init = true;
            }
            foreach (Transform child in CasesNon.transform)
            {
                child.transform.gameObject.GetComponent<casses>().init = true;
            }
            Init = false;
        }
    }

}
