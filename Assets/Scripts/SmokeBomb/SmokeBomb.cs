using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SmokeBomb : MonoBehaviour
{
    public Collider smokebombTrigger;
    public GameObject particles;
    bool activated = false;

    private void Start()
    {
        particles= GameObject.FindAnyObjectByType<ParticleSystem>(FindObjectsInactive.Include).gameObject;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player") && activated == false)
        { 
            activated = true;
        StartCoroutine(smokeBombActive());
        particles.transform.position = gameObject.transform.position;
        particles.SetActive(true);

        }
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            //aplicar Stun
        }
    }

    IEnumerator smokeBombActive()
    {
        smokebombTrigger.enabled = true;
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }

}
