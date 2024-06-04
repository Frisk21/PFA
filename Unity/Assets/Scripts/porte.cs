using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class porte : MonoBehaviour
{
    //GAMEOBJECT
    public GameObject Porte;

    //VARIABLES
    [HideInInspector] public bool isActive;
    [HideInInspector] public float vitesse;
    [HideInInspector] public int hauteurMax;

    // Start is called before the first frame update
    void Start()
    {
        //init les variables
        vitesse = 1f;
        hauteurMax = 8;
    }

    // Update is called once per frame
    void Update()
    {
        //si on a activer la porte, la leve
        if (isActive && Porte.transform.position.y < hauteurMax)
        {
            Porte.transform.position += new Vector3(0, vitesse * Time.deltaTime, 0);
        }
    }

    public void OuverturePorte()
    {
        isActive = true;
    }
}
