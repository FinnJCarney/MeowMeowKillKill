using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RatController : MonoBehaviour
{

    NavMeshAgent nma;
    public Transform targetPos;
    public Transform floor;
    public Transform floor2;
    SphereCollider sc;
    ParticleSystem ps;
    Light light;
    AudioSource walkAS;
    public AudioSource death;
    public Transform[] runAways;

    int deathTimer;

    public int floorChoice;

    bool dead;

    int timerLimit;
    void Start()
    {
        GameManager.me.ratNum++;
        transform.parent = null;
        nma = GetComponent<NavMeshAgent>();
        sc = GetComponent<SphereCollider>();
        ps = GetComponentInChildren<ParticleSystem>();
        light = GetComponentInChildren<Light>();
        walkAS = GetComponent<AudioSource>();
        SetNewTarget();

        walkAS.pitch = Random.Range(0.8f, 1.2f);
        walkAS.PlayDelayed(Random.Range(0, 1.9f));
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            nma.SetDestination(targetPos.transform.position);
        }
        else
        {
            nma.SetDestination(transform.position);
        }

        if(dead && deathTimer < GameManager.me.timer)
        {
            GameManager.me.ratNum--;
            Destroy(this.gameObject);
        }
    }

    void FixedUpdate()
    {
        if (!dead)
        {
            if (timerLimit < GameManager.me.timer || Vector3.Distance(transform.position, targetPos.position) < 2)
            {
                SetNewTarget();
            }
        }
    }

    void SetNewTarget()
    {
        floorChoice = Random.Range(0, 2);
        if (floorChoice == 0)
        {
            targetPos.position = floor.position + new Vector3(floor.transform.localScale.x * Random.Range(-0.5f, 0.5f), 0, floor.transform.localScale.z * Random.Range(-0.5f, 0.5f));
        }                                                                                                               
        else                                                                                                            
        {                                                                                                               
            targetPos.position = floor2.position + new Vector3(floor2.transform.localScale.x * Random.Range(-0.5f, 0.5f), 0, floor2.transform.localScale.z * Random.Range(-0.5f, 0.5f));
        }
        timerLimit = (Random.Range(750, 1500)) + GameManager.me.timer;
    }

    public void SetTarget1()
    {
        targetPos.position = floor.position + new Vector3(floor.transform.localScale.x * Random.Range(-0.35f, 0.35f), 0, floor.transform.localScale.z * Random.Range(-0.35f, 0.35f));
        timerLimit = (Random.Range(750, 1500)) + GameManager.me.timer;
    }

    public void SetTarget2()
    {
        targetPos.position = floor2.position + new Vector3(floor2.transform.localScale.x * Random.Range(-0.35f, 0.35f), 0, floor2.transform.localScale.z * Random.Range(-0.35f, 0.35f));
        timerLimit = (Random.Range(750, 1500)) + GameManager.me.timer;
    }

    public void RunFromPlayer()
    {
        float maxDis = 0;
        for (int i = 0; i < runAways.Length; i++)
        {
            maxDis = Mathf.Max(maxDis, Vector3.Distance(CatController.me.transform.position, runAways[i].position));
        }
        for (int i = 0; i < runAways.Length; i++)
        {
            if(Mathf.Approximately(maxDis, Vector3.Distance(CatController.me.transform.position, runAways[i].position)))
            {
                targetPos.position = runAways[i].position;
            }
        }
        timerLimit = (Random.Range(250, 750)) + GameManager.me.timer;
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Attack" && !dead)
        {
            dead = true;
            ps.Play();
            light.color = Color.red;
            death.pitch = Random.Range(0.8f, 1.2f);
            death.Play();

            deathTimer = GameManager.me.timer + 400;
        }
    }
}
