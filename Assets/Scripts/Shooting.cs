using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shooting : MonoBehaviour
{
    public GameObject fireObject;
    public GameObject bulletPrefabs;
    public float shootRate = 1.5f;
    public float bulletForce;
    float nextShootTime = 0;
    RaycastHit hit;
    Touch touch;

    void Update()
    {

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            
            if (Time.time >= nextShootTime)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log(hit.transform.name);
                    Shoot();
                    nextShootTime = Time.time + 1f / shootRate;
                }
                

            }
            fireObject.transform.LookAt(hit.transform);
        }

        void Shoot()
        {
            GameObject bullet = KhtPool.GetObject(bulletPrefabs);
            bullet.SetActive(true);
            bullet.transform.SetPositionAndRotation(fireObject.transform.position, fireObject.transform.rotation);
            Bullet bull = bullet.GetComponent<Bullet>();
            bull.rb.velocity = bull.transform.forward * bulletForce;

        }
    }
}
