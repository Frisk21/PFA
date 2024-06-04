using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energie : MonoBehaviour
{
    //VARIABLES
    [HideInInspector] public bool Active;
    [HideInInspector] public float vitesse;
    [HideInInspector] public float tick;
    [HideInInspector] public bool changeDone;
    public bool isFinish;

    //GAMEOBJECT
    public GameObject gameManager;
    public GameObject porteFinal;
    [HideInInspector] public GameObject child1;
    [HideInInspector] public GameObject child2;

    //AUTRES
    [HideInInspector] public Color colorCurrent;
    [HideInInspector] public Color ColorDeAct;
    [HideInInspector] public Color ColorAct;
    [HideInInspector] public Material mat1;
    [HideInInspector] public Material mat2;
    [HideInInspector] public GameManager managerScript;
    [HideInInspector] public porte porteScript;
    

    // Start is called before the first frame update
    void Start()
    {
        //recup info
        child1 = this.gameObject.transform.GetChild(0).gameObject;
        child2 = this.gameObject.transform.GetChild(1).gameObject;
        mat1 = child1.GetComponent<Renderer>().material;
        mat2 = child2.GetComponent<Renderer>().material;
        managerScript = gameManager.GetComponent<GameManager>();
        if (isFinish)
        {
            porteScript = porteFinal.GetComponent<porte>();
        }

        //initialisation des variables
        Active = false;
        ColorDeAct = new Color32(0x80, 0x80, 0x80, 0xFF); //808080
        ColorAct = new Color32(0x00, 0xFF, 0xE4, 0xFF); //00FFE4
        vitesse = 0.005f;
        tick = 0f;
        changeDone = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (managerScript.isGameActive)
        {
            if (Active && !changeDone)
            {
                tick += vitesse;
                ChangeColor(tick, ColorDeAct, ColorAct);
                if (colorCurrent == ColorAct)
                {
                    changeDone = true;
                }
            }

            if (changeDone && isFinish)
            {
                porteScript.OuverturePorte();
            }
        }
    }

    public void ChangeColor(float tick, Color StartColor, Color EndColor)
    {
        colorCurrent = Color.Lerp(StartColor, EndColor, tick); // fait que c'est progressif
        mat1.SetColor("_BaseColor", colorCurrent);
        mat2.SetColor("_BaseColor", colorCurrent);
    }

}
