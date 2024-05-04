using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit;

public class CatComponent : MonoBehaviour
{
    XRGrabInteractable grab;
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
        grab = GetComponent<XRGrabInteractable>();
        anim = GetComponent<Animator>();   
        audioSource = GetComponent<AudioSource>();
        timer = 0;
        nextTime = Random.Range(1.0f, 4.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (grab.isSelected) 
        {
           anim.SetBool("aupar",true);
        }
        else
        {
            if (timer >= nextTime)
            {
                if (agent.isStopped)
                {
                    Vector3 moveDirection = RandomNavMeshPoint();
                    Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                    agent.SetDestination(RandomNavMeshPoint());
                    transform.rotation = targetRotation;
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
        NavMeshHit hit;
        Vector3 randomPoint = Vector3.zero;
        if (NavMesh.SamplePosition(transform.position + Random.insideUnitSphere * 10, out hit, 10.0f, NavMesh.AllAreas))
        {
            randomPoint = hit.position;
        }

        return randomPoint;
    }

    void PlaySound(AudioClip clip) 
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
