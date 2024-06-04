using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// FAIRE UN SYTEME DE TIRER LES OBJETS 
// SMOOTH DEPLACEMENT

public class PushObject : MonoBehaviour
{
    //KEYCODE
    [HideInInspector] public KeyCode F;
    [HideInInspector] public KeyCode AManette;

    //GAMEOBJECT
    public GameObject Player;
    [HideInInspector] public GameObject Collision;
    [HideInInspector] public GameObject CollisionExit;
    public GameObject movableObject;
    public GameObject TestDeplacement;
    public GameObject TestDeplacementTirer;
    public GameObject TestParent;
    public GameObject Camera;
    public GameObject GameManager;

    //VARIABLES
    [HideInInspector] public float vitesseMove; //movement du cube (%)
    [HideInInspector] public bool isTouching;
    [HideInInspector] public bool hasCollided;
    [HideInInspector] public bool hasCollidedBack;
    [HideInInspector] public float VertMove;
    [HideInInspector] public float HorMove;
    [HideInInspector] public float GridScale;
    [HideInInspector] public bool isActive;
    [HideInInspector] public bool sensX; //true si x, false si z;
    [HideInInspector] public bool isFront; //true si bas ou gauche, false si haut ou droite (par rapport a 0,0)
    [HideInInspector] public float camRotaY;
    [HideInInspector] public int isPushing; //1 si push/ 0 si pull, 2 si rien
    [HideInInspector] public int positionP; // 1: bas / 2: droite / 3: haut / 4: gauche (par rapport a 0,0) - PLAYER
    [HideInInspector] public int positionC; // 1: 0-90 / 2: 90-18 / 3: 180-(-90) / 4: (-90)-0 (rota cam Y) - CAMERA
    [HideInInspector] public float speedP;
    [HideInInspector] public float HoriInp;
    [HideInInspector] public float VertInp;
    public int cubeNumber;

    //AUTRES
    [HideInInspector] public Vector3 newPos;
    [HideInInspector] public Vector3 newPosTirer;
    [HideInInspector] public Vector3 vectDir;
    [HideInInspector] public Vector3 vectDep;
    [HideInInspector] public moveTest testScript;
    [HideInInspector] public moveTest testScriptTirer;
    [HideInInspector] public Player playerScript;
    [HideInInspector] public GameManager managerScript;
    [HideInInspector] public MeshRenderer Renderer;

    // Start is called before the first frame update
    void Start()
    {
        //recup les scripts et indique lequel est avant/arriere
        testScript = TestDeplacement.GetComponent<moveTest>();
        testScript.forward = true;
        testScriptTirer = TestDeplacementTirer.GetComponent<moveTest>();
        testScriptTirer.forward = false;
        playerScript = Player.GetComponent<Player>();
        managerScript = GameManager.GetComponent<GameManager>();
        Renderer = movableObject.GetComponent<MeshRenderer>();

        //Initialisation des varibles
        F = KeyCode.E;
        AManette = KeyCode.JoystickButton0;
        isTouching = false;
        vitesseMove = 0.4f; //vitesse deplacements
        hasCollided = false;
        hasCollidedBack = false;
        newPos = TestParent.transform.position;
        GridScale = Renderer.bounds.size.z/2 + 0.05f ; //Taille 1/2objet + decalage leger (0.05?)
        isActive = false;
        camRotaY = 0;
        isPushing = 2;
        positionP = 0;
        positionC = 0;
        HoriInp = 0;
        VertInp = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (managerScript.isGameActive)
        {
           

            if (isTouching)
            {
                //si c'est pas bloquer bloque et move si besoin, sinon debloque.
                if (!isActive && (Input.GetKeyDown(F) || Input.GetKeyDown(AManette)) && !playerScript.playerStop)
                {
                    //stop le mouvement du player
                    isActive = true;
                    playerScript.playerStop = true;
                }
                else if (isActive && (Input.GetKeyDown(F) || Input.GetKeyDown(AManette)))
                {
                    isActive = false;
                    playerScript.playerStop = false;
                }

                //Calcul direction de deplacement pour les colliders
                vectDep = TestParent.transform.position; //new Vector3(TestParent.transform.position.x, testScript.transform.position.z, TestParent.transform.position.y);
                vectDir = TestParent.transform.position - Player.transform.position;
                vectDir = DeplacementObjet(vectDir); //vectDir devien deplacement des collider test
                newPos = vectDep + vectDir;
                newPosTirer = vectDep - vectDir;
                testScript.dep = newPos;
                testScriptTirer.dep = newPosTirer;

                //Deplacement cube + player quand activer
                if (isActive)
                {
                    //recup emplacement cam
                    camRotaY = Camera.transform.eulerAngles.y;
                    positionP = calculPosPlayer();
                    positionC = calculPosCam(camRotaY);

                    if (positionC > 0 && positionP > 0 && positionC < 5 && positionP < 5)
                    {
                        HoriInp = playerScript.horizontalInput;
                        VertInp = playerScript.verticalInput;

                        isPushing = calculPullPush(positionP, positionC, HoriInp, VertInp);
                        if (isPushing == 1 && !hasCollided)
                        {
                            //pousse 
                            movableObject.transform.position += vectDir / vitesseMove * Time.deltaTime;
                            playerScript.playerRB.position += vectDir / vitesseMove * Time.deltaTime;
                        }
                        else if (isPushing == 0 && !hasCollidedBack)
                        {
                            //tire
                            movableObject.transform.position -= vectDir / vitesseMove * Time.deltaTime;
                            playerScript.playerRB.position -= vectDir / vitesseMove * Time.deltaTime;
                        }

                    }

                }


            }
            else
            {
                testScript.dep = TestParent.transform.position;
                testScriptTirer.dep = TestParent.transform.position;
            }
        }
        

    }

    public void OnTriggerEnter(Collider other)
    {
        //recupere l'objet toucher
        Collision = other.gameObject;

        //Si le player touche, active is touching
        if (Collision == Player)
        {
            isTouching = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        //recupere l'objet toucher
        CollisionExit = other.gameObject;

        //Si le player touche, desactive is touching
        if (CollisionExit == Player)
        {
            isTouching = false;
        }
    }

    public Vector3 DeplacementObjet(Vector3 dir)
    {
        Vector3 Result;
        float xAbs;
        float zAbs;

        //recup val absolu de x et z
        xAbs = Mathf.Abs(dir.x);
        zAbs = Mathf.Abs(dir.z);

        //regarde lequel est plus grand (si player est plus sur x ou z)
        if (xAbs > zAbs)
        {
            //sur x
            if (dir.x > 0) 
            {
                Result = new Vector3(GridScale, 0, 0);
                isFront = true;
            }
            else
            {
                Result = new Vector3(-GridScale, 0, 0);
                isFront = false;
            }

            //tourne collider dans le bon sens
            sensX = true;
            //TestParent.transform.rotation = Quaternion.Euler(0, 90, 0); //Rota sur Y!
            TestParent.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else
        {
            // sur z
            if (dir.z > 0)
            {
                Result = new Vector3(0, 0, GridScale);
                isFront = true;
            }
            else
            {
                Result = new Vector3(0, 0, -GridScale);
                isFront = false;
            }

            // tourne collider
            sensX = false;
            //TestParent.transform.rotation =  Quaternion.Euler(0, 0, 0); //pcq Rota inconnu sur X???
            TestParent.transform.rotation =  Quaternion.Euler(90, 0, 0);

        }
        
        

        return Result;
    }

    //return 1 si push, 0 si pull, 2 si rien, 3 si erreur
    public int calculPullPush(int posP , int posC, float hori, float verti)
    {
        posP -= 1;
        posC -= 1;

        if (posP == posC)
        {
            if (hori == 0 && verti == 0)
            {
                return 2;
            }
            else if (hori >= 0 && verti >= 0)
            {
                return 1;
            }
            else if (hori <= 0 && verti <= 0)
            {
                return 0;
            }
            
        }
        if ( posC == modulo(posP + 1, 4))
        {
            if (hori == 0 && verti == 0)
            {
                return 2;
            }
            else if (hori >= 0 && verti <= 0)
            {
                return 1;
            }
            else if (hori <= 0 && verti >= 0)
            {
                return 0;
            }
        }
        if (posC == modulo(posP + 2, 4))
        {
            if (hori == 0 && verti == 0)
            {
                return 2;
            }
            else if (hori <= 0 && verti <= 0)
            {
                return 1;
            }
            else if (hori >= 0 && verti >= 0)
            {
                return 0;
            }
        }
        if (posC == modulo(posP + 3, 4))
        {
            if (hori == 0 && verti == 0)
            {
                return 2;
            }
            else if (hori <= 0 && verti >= 0)
            {
                return 1;
            }
            else if (hori >= 0 && verti <= 0)
            {  
                return 0;
            }
        }
        return 3;
    }

    public int modulo (int n, int m)
    {
        return ((n % m + m) % m);
    }

    //defini la position du player
    public int calculPosPlayer()
    {
        if (!sensX && isFront)
        {
            return 1;
        }
        if  (!sensX && !isFront) 
        {
            return 3;
        }
        if (sensX && isFront)
        {
            return 4;
        }
        if (sensX && !isFront)
        {
            return 2;
        }
        return 0;
    }

    //defini la position de la camera
    public int calculPosCam(float Y)
    {
        if (Y >= 0 && Y < 90)
        {
            return 4;
        }
        if (Y >= 90 && Y < 180)
        {
            return 3;
        }
        if (Y >= 180 && Y < 270)
        {
            return 2;
        }
        if (Y >= 270 && Y < 360)
        {
            return 1;
        }
        return 0;
    }
}
