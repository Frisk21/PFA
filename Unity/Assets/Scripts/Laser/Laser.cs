using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    //GAMEOBJECT
    public GameObject laser;
    public GameObject StartPoint;

    //AUTRES
    [HideInInspector] public Vector3 Pos;
    [HideInInspector] public Vector3 PosFin;

    //VARIABLES
    public int distMax; // a definir dans unity
    [HideInInspector] public float angle;
    public bool start; //Est-ce le debut du laser? Oui: true / Non: false  A def dans unity
    [HideInInspector] public bool isActive;

    //AUTRE
    [HideInInspector] public LineRenderer line;
    public LayerMask layer; //Mettre les layers prix en compte
    [HideInInspector] public Cristal CristalScript;
    [HideInInspector] public Vector3 vectDir;


    // Start is called before the first frame update
    void Start()
    {
        //recupere le linerenderer
        line = laser.GetComponent<LineRenderer>();

        //active tjr le script si c un debut, sinon non
        if(start)
        {
            isActive = true;
        }
        else
        {
            isActive = false;
        }

    }


    // Update is called once per frame
    void Update()
    {
        //initialisation de la position de depart, de l'angle et calcule de la direction.
        Pos = StartPoint.transform.position;
        angle = (StartPoint.transform.eulerAngles.y * Mathf.PI) / 180;
        PosFin = TrigoCalculs(distMax);

        //initialisation du laser avec raycast
        line.SetPosition(0, Pos);
        

    }

    private void FixedUpdate()
    {
        if (isActive)
        {
            RaycastHit hit;

            // calcule d'un rayon regandant s'il collide avec les layers selectionner.
            // Set de la position de fin en fonction de s'il touche ou non.
            if (Physics.Raycast(Pos, PosFin - Pos, out hit, distMax, layer))
            {
                //Si collide avec un cristal
                if (hit.collider.tag == "Cristaux")
                {
                    //recupere le script
                    CristalScript = hit.collider.GetComponent<Cristal>();
                    //active le scripte
                    CristalScript.isTouching = true;
                    //calcule vecteur direction vecteur arrivant (arriver - depart)
                        //vectDir = hit.point - Pos;
                    //lui donne l'angle d'arriver
                        //CristalScript.Teta1 = CalculAngle(vectDir, hit.normal);
                }
                else if (CristalScript != null) 
                {

                    CristalScript.isTouching = false;
                }

                PosFin = TrigoCalculs(hit.distance);
                line.SetPosition(1, PosFin);
            }
            else
            {
                //desactive le script du dernier cristal toucher
                if (CristalScript != null)
                {
                    CristalScript.isTouching = false;
                    CristalScript = null;
                }
                line.SetPosition(1, PosFin);

            }
        }
        else
        {
            line.SetPosition(1, Pos);
            if (CristalScript != null)
            {
                CristalScript.isTouching = false;
            }
        }
    }

    public Vector3 TrigoCalculs(float dist)
    {
        float distanceX;
        float distanceZ;

        //Calcule trigonometrique  permetant de determiner la position de fin du laser.
        distanceX = dist * Mathf.Sin(angle);
        distanceZ = dist * Mathf.Cos(angle);
        PosFin = new Vector3(distanceX, 0, distanceZ) + Pos;
        return PosFin;
    }

    /*
    public float CalculAngle(Vector3 u, Vector3 v)
    {
        //Calcule de l'angle entre deux vecteurs
        //VARIABLES
        float uNorme;
        float vNorme;
        float produitScalaire; 
        float cosTeta;
        float Teta;

        //calcul norme des vecteurs
        uNorme = Mathf.Sqrt(Mathf.Pow(u.y, 2) + Mathf.Pow(u.x, 2) + Mathf.Pow(u.z, 2));
        vNorme = Mathf.Sqrt(Mathf.Pow(v.y, 2) + Mathf.Pow(v.x, 2) + Mathf.Pow(v.z, 2));

        //Calcul du produit scalaire
        produitScalaire = Vector3.Dot(u, v);

        //Calcul du cos teta
        cosTeta = (produitScalaire / (uNorme * vNorme));

        //Calcule Teta
        Teta = Mathf.Acos(cosTeta);
        return Teta;

    }
    */
}

