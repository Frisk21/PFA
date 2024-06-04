using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveTest : MonoBehaviour
{
    //GAMEOBJECT
    public GameObject movableGO;
    public GameObject ObjectParent;
    public GameObject Collider;
    public GameObject Player;
    public GameObject Sol;

    [HideInInspector] public GameObject Other;

    //AUTRES
    [HideInInspector] public PushObject pushScript;
    [HideInInspector] public Rigidbody ObjectRB;
    [HideInInspector] public Vector3 dep;

    //VARIABLES
    [HideInInspector] public bool forward; //donner dans le start de pushObject

    // Start is called before the first frame update
    void Start()
    {
        //recup le script et rb
        pushScript = Collider.GetComponent<PushObject>();
        ObjectRB = movableGO.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        ObjectRB.transform.position = dep;

        if (forward)
        {
            pushScript.hasCollided = false;
        }
        else
        {
            pushScript.hasCollidedBack = false;
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        Other = other.gameObject;
        ObjectRB.velocity += Vector3.zero;
        
        if (Other != Player && Other != Sol)
        {
            if (forward)
            {
                pushScript.hasCollided = true;
            }
            else
            {
                pushScript.hasCollidedBack = true;
            }
            movableGO.transform.position = ObjectParent.transform.position;
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject != Player && collision.gameObject != Sol)
        {
            if (forward)
            {
                pushScript.hasCollided = false;
            }
            else
            {
                pushScript.hasCollidedBack = false;
            }
            
        }
        
    }

}
