using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Android;

public class casses : MonoBehaviour
{

    //VARIABLES
    public bool Hub;
    public bool canWalkOn;
    [HideInInspector] public bool isTouching;
    [HideInInspector] public bool activated;

    [HideInInspector] public bool init;
    [HideInInspector] public float vitesseCasesFast;
    [HideInInspector] public float vitesseCasesSlow;
    [HideInInspector] float vitesseDefAct;
    [HideInInspector] public float vitesseDefDeact;
    [HideInInspector] public bool CoRoutAct;
    [HideInInspector] public float tick;
    [HideInInspector] public bool tpDone;


    //GAMEOBJECT
    public GameObject player;
    public GameObject CaseParent;
    public GameObject caseCurrent;
    public GameObject restartPoint;
    public GameObject gameManager;
    public GameObject porteChemin;

    //AUTRES
    [HideInInspector] public InitCases Init;
    [HideInInspector] public Player playerScript;
    [HideInInspector] public Material mat;
    [HideInInspector] public Color colorOui;
    [HideInInspector] public Color colorDeact;
    [HideInInspector] public Color colorNon;
    [HideInInspector] public Color colorCurrent;
    [HideInInspector] public Color colorAct;
    [HideInInspector] public GameManager managerScript;
    [HideInInspector] public porte porteScript;

    // Start is called before the first frame update
    void Start()
    {
        //recup le script
        
        playerScript = player.GetComponent<Player>();
        managerScript = gameManager.GetComponent<GameManager>();
        if (Hub)
        {
            porteScript = porteChemin.GetComponent<porte>();
        }
        else
        {
            Init = CaseParent.GetComponent<InitCases>();
        }
        

        //recup le material et color
        mat = caseCurrent.GetComponent<Renderer>().material;

        //initialisation des variables
        isTouching = false;
        colorDeact = new Color32(0xBF, 0xB4, 0x00, 0xFF); //BFB400
        colorOui = new Color32(0x00, 0xEB, 0xFF, 0xFF); //00EBFF
        colorNon = new Color32(0xFF, 0x00, 0x00, 0xFF); //FF0000

        tpDone = false;
        tick = 0;
        init = false;
        activated = false;
        CoRoutAct = false;
        vitesseCasesFast = 2f;
        vitesseCasesSlow = 1f;
        //met la bonne vitesse et couleur pour la case
        if (canWalkOn)
        {
            colorAct = colorOui;
            vitesseDefAct = vitesseCasesSlow;
            vitesseDefDeact = vitesseCasesFast;
        }
        else
        {
            colorAct = colorNon;
            vitesseDefDeact = vitesseCasesSlow;
            vitesseDefAct = vitesseCasesFast;
        }

        //initialisation du go
        mat.SetColor("_BaseColor", colorDeact);
    }

    // Update is called once per frame
    void Update()
    {
        if (managerScript.isGameActive)
        {
            //si le player touche
            if (isTouching && !activated)
            {
                activated = true;
                CoRoutAct = true;
                if (!canWalkOn)
                {
                    playerScript.playerStop = true;
                }
            }

            //illumine la case si active et pas dj de la bonne couleur
            if (activated && CoRoutAct)
            {
                tick += vitesseDefAct * Time.deltaTime;
                ChangeColor(tick, colorDeact, colorAct);
                if (tick > 1)
                {
                    CoRoutAct = false;
                }
            }

            //si bonne couleur
            if (!CoRoutAct && activated)
            {
                if (!canWalkOn && !tpDone) //peut pas marcher dessus
                {
                    //active reinitialisation chemin et tp perso
                    Init.Init = true;
                    playerScript.PlayerTP(restartPoint);
                    tpDone = true;
                }
            }


            if (init && !CoRoutAct)
            {
                //Si c dj la bonne couleur, ne fait rien, sinon, la change
                if (tick > 0)
                {
                    tick -= vitesseDefDeact * Time.deltaTime;
                    ChangeColor(tick, colorDeact, colorAct);
                }
                else
                {
                    init = false;
                    activated = false;
                    tpDone = false;
                    tick = 0;
                    playerScript.playerStop = false;
                }

            }

            if (Hub && activated)
            {
                porteScript.OuverturePorte();
            }
        }
        
    }

    //Change la colo
    public void ChangeColor(float tick, Color StartColor, Color EndColor)
    {
            colorCurrent = Color.Lerp(StartColor, EndColor, tick); // fait que c'est progressif
            mat.SetColor("_BaseColor", colorCurrent);
    }

  

}

