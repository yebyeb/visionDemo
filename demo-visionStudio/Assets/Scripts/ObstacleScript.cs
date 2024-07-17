using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    private GameControl GM;
    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        GM = FindObjectOfType<GameControl>(); 
    }
    
    private void Update()
    {
        rb.velocity = new Vector3(0, 0, GM.ObstacleSpeed);
        // Ekrandan çıkan engelleri belirli bir mesefa sonra yok etmek için.
        if (transform.localPosition.z < -5f)
        {
            Destroy(gameObject);
        }
    }

     // Karakterin engellerin üstünden zıplaması halinde puan kazanmasını sağlar.
     private void OnTriggerEnter(Collider other)
    {
       if (other.gameObject.tag == "Character")
        {
           GM.GetPoint();
           Destroy(gameObject,0.1f);
        }
    }
}
