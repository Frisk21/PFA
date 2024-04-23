using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steles : MonoBehaviour
{

    //MATERAIL
    public Material MatStele;
    public Color SteleColor;

    //GAMEOBJECT
    public GameObject Player;
    public GameObject stele;
    public GameObject enCollision;

    //VARIABLES
    public int NewEmission;
    public float Emission;
    public bool hasTouched;

    // Start is called before the first frame update
    void Start()
    {
        //Recupere le material et sa couleur d'emission
        MatStele = stele.GetComponent<Renderer>().material;
        SteleColor = MatStele.GetColor("_EmissionColor");

        //initialise les emissions et variables
        Emission = 1;
        NewEmission = 5;
        hasTouched = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasTouched && Emission < NewEmission)
        {
            Emission += Time.deltaTime *4;
            MatStele.SetColor("_EmissionColor", SteleColor * Emission);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        //Recupere l'objet toucher
        enCollision = other.gameObject;

        //Quand le player entre en colision avec la stele, change l'emissio, pour l'activer
        if (enCollision == Player)
        {
            hasTouched = true;
        }
    }
}
