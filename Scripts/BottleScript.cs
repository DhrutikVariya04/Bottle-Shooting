using System;
using UnityEngine;

[RequireComponent(typeof(Explodable))]
public class BottleScript : MonoBehaviour
{
    private Explodable _explodable;

    private void Start()
    {
        _explodable = GetComponent<Explodable>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ball" && gameObject.transform.tag != "Box")
        {
            GetComponent<AudioSource>().Play();
        }

        if (collision.transform.tag == "BackGround" && gameObject.transform.tag != "Box")
        {            
            _explodable.explode();
        }

        if(collision.transform.tag == "Ball" && gameObject.transform.tag == "Box")
        {
            _explodable.explode();
        }
     
    }
}
