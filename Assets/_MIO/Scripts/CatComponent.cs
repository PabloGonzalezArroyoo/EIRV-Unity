using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit;

public class CatComponent : MonoBehaviour
{
    NavMeshAgent agent;
    Animator anim;
    AudioSource audioSource;
    [SerializeField]
    AudioClip meow;
    [SerializeField]
    AudioClip prrr;

    float timer;
    float nextTime;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();   
        audioSource = GetComponent<AudioSource>();
        timer = 0;
        nextTime = Random.Range(1.0f, 4.5f);
        agent.isStopped = false;
        anim.SetBool("movimiento", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!anim.GetBool("acariciar"))
        {
            if (timer >= nextTime)
            {
                if (agent.isStopped)
                {
                    agent.SetDestination(RandomNavMeshPoint());
                    agent.isStopped = false;
                    anim.SetBool("movimiento", true);
                    PlaySound(meow);
                }
                else
                {
                    agent.isStopped = true;
                    anim.SetBool("movimiento", false);
                }
                timer = 0;
                nextTime = Random.Range(2.0f, 4.5f);
            }
            else {
                timer += Time.deltaTime;
            }
        }
    }


    Vector3 RandomNavMeshPoint()
    {
        Vector3 randomPoint = Vector3.zero;
        if (NavMesh.SamplePosition(transform.position + Random.insideUnitSphere * 10, out NavMeshHit hit, 10.0f, NavMesh.AllAreas))
        {
            randomPoint = hit.position;
        }

        return randomPoint;
    }

    void PlaySound(AudioClip clip) 
    {
        audioSource.clip = clip;
        if (clip != prrr) audioSource.pitch = 1 + Random.Range(-0.15f, 0.15f);
        audioSource.Play();
    }

    public void BeginPet() 
    {
        PlaySound(prrr);
        audioSource.loop = true;
        anim.SetBool("acariciar", true);
        agent.isStopped = true;
    }

    public void EndPet()
    {
        audioSource.Stop();
        audioSource.loop = false;
        anim.SetBool("acariciar", false);
    }

    public void BeginGrab() 
    {
        anim.SetTrigger("aupar");
    }

    public void EndGrab()
    {
        anim.SetTrigger("finAupar");
    }
}
