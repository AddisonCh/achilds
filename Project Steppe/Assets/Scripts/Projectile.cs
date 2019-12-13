using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public string projectileName;
    public int projectileSpeed;
    public enum ProjectileType {Ball, Bolt}
    public ProjectileType thisType;
    private Vector3 shootDir;
    public int damage;

    void Start()
    {
        GameObject player = FindObjectOfType<PlayerController>().gameObject;
        //shootDir = new Vector3(Camera.main.transform.eulerAngles.x, 0, 0);
        shootDir = player.transform.forward;
    }


    void Update()
    {
        switch (thisType)
        {
            case ProjectileType.Ball:
                transform.Translate(shootDir * projectileSpeed * Time.deltaTime);
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "NonDestructable":
                Destroy(gameObject);
                break;
        }
    }
}
