using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bloques : MonoBehaviour
{
    //GAMEOBJECT
    public GameObject bloque;

    //VARAIABLES
    [HideInInspector] public bool isActive;
    public float HauteurMin;
    public int HauteurMax;
    [HideInInspector] public float vitesse;

    // Start is called before the first frame update
    void Start()
    {
        //iinit varaibels
        isActive = false;
        vitesse = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive && bloque.transform.position.y > HauteurMin)
        {
            bloque.transform.position -= new Vector3(0, vitesse * Time.deltaTime, 0);
        }
        if (!isActive && bloque.transform.position.y < HauteurMax)
        {
            bloque.transform.position += new Vector3(0, vitesse * Time.deltaTime, 0);
        }
    }
}
