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
        particles = GameObject.FindAnyObjectByType<ParticleSystem>(FindObjectsInactive.Include).gameObject;
        if (particles.name=="ParticleSubSystem")
        {
            particles = particles.transform.parent.gameObject;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player") && activated == false)
        { 
            activated = true;
            gameObject.GetComponent<Rigidbody>().freezeRotation = true;
            gameObject.GetComponent<Rigidbody>().linearVelocity = new Vector3(0,0,0);
            StartCoroutine(smokeBombActive());
            particles.transform.position = gameObject.transform.position+new Vector3(0,1.5f,0);
            particles.SetActive(true);

        }
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IStunnable>(out IStunnable enemy))
        {
            enemy.Stun();
        }
    }

    IEnumerator smokeBombActive()
    {
        smokebombTrigger.enabled = true;
        
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }

}
