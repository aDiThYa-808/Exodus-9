
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("target"))
        {
            CreateBulletHole(collision);
            Destroy(gameObject);


        }

        if (collision.gameObject)
        {
            CreateBulletHole(collision);
            Destroy(gameObject);


        }
    }

    public void CreateBulletHole(Collision col)
    {
        ContactPoint contact = col.contacts[0];
        GameObject hole = Instantiate(
            GlobalReferences.Instance.BulletImpact,
            contact.point,
            Quaternion.LookRotation(contact.normal)
            );
        hole.transform.SetParent(col.gameObject.transform);
    }


}
