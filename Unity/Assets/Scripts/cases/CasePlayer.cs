using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasePlayer : MonoBehaviour
{

    //GAMEOBJECT
    public GameObject dalleParent;
    public GameObject player;

    //AUTRE
    [HideInInspector] public casses caseScript;

    // Start is called before the first frame update
    void Start()
    {
        caseScript = dalleParent.GetComponent<casses>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            caseScript.isTouching = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            caseScript.isTouching = false;
        }
    }
}
