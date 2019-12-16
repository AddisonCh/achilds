using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Receiver : MonoBehaviour
{

    public UnityEvent thisEvent;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Projectile":
                Destroy(collision.gameObject.transform.parent.gameObject);
                thisEvent.Invoke();
                break;
        }

    }
}
