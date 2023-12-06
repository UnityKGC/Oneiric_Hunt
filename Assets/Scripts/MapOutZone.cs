using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapOutZone : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Boss") || collision.gameObject.CompareTag("Monster"))
        {
            collision.gameObject.GetComponent<Stat>().SetDamage(10000f);
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }
}
