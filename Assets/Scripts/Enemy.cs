using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject Playground;
    public GameObject Ragdoll;
    public int health;
    public int Bulletdamage;

    private void OnCollisionEnter(Collision collision)
    {
        health -= Bulletdamage;
        if(health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        PlaygroundEnemyScript target = Playground.GetComponent<PlaygroundEnemyScript>();
        target.UpdateTargetsForTargets--;
        KhtPool.ReturnObject(gameObject);
        /*GameObject ragdoll = KhtPool.GetObject(Ragdoll);
        ragdoll.SetActive(true);
        ragdoll.transform.SetPositionAndRotation(gameObject.transform.position, gameObject.transform.rotation);
        KhtPool.ReturnObject(ragdoll);*/

    }
}
