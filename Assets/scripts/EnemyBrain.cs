using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;



public class EnemyBrain : MonoBehaviour
    {
        private Transform target;
        private float shootdistance;
        private float pathUpdateInterval = 0.2f;
    private float pathUpdateDeadLine;


        private NavMeshAgent nav;
        private Animator anim;
        
        private void Awake()
        {
            nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        target = GlobalReferences.Instance.player;

    }

    void Start()
    {
        shootdistance = nav.stoppingDistance;
    }

    void Update()
    {
        if (target != null) { 
            bool inRange = Vector3.Distance(transform.position, target.position) <= shootdistance;

            if (inRange)
            {
                LookAtTarget();
            }
            else {
                UpdatePath();
            }
            anim.SetBool("shooting", inRange);
        }
        anim.SetFloat("speed", nav.desiredVelocity.sqrMagnitude);
    }
    private void LookAtTarget()
    {
        Vector3 lookPos = target.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation,rotation,0.2f);
    }
    private void UpdatePath() {

        nav.SetDestination(target.position);
    }
}

