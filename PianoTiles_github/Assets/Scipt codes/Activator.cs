using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    SpriteRenderer sr;
    public KeyCode key;
    GameObject note;
    Color old;
    public bool createMode;
    public GameObject n;

    // Start is called before the first frame update
    void Awake ()
    {
        sr=GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Start()
    {
        old=sr.color;
    }

    void Update (){
        if(createMode){
            if(Input.GetKeyDown(key))
                Instantiate(n,transform.position,Quaternion.identity);

        }else{

        }
    }


    
}
