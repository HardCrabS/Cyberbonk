using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NPC_State
{
    walking,
    scared,
    policeman
}

public class ReactiveTarget : MonoBehaviour, IDamagable
{
    public float health = 100;

    public float notifyAreaDistance = 10f;
    public LayerMask npcMask;

    public float scaredTime = 5f;

    public NPC_State currState = NPC_State.walking;

    float lastTimeScared = 0;

    Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void ReactToHit(float damage)
    {
        lastTimeScared = Time.time;
        health -= damage;
        if (health <= 0)
        {
            StartCoroutine(Die());
        }
        else if(currState == NPC_State.walking)
        {
            SetScaredState();
            NotifyNearbyNPC();
        }
    }
    public void SetScaredState()
    {
        if (currState == NPC_State.scared) return;

        /*WanderingAI behavior = GetComponent<WanderingAI>();
        if (behavior != null)
        {
            behavior.enabled = false;
        }*/

        animator.SetBool("Scared", true);
        currState = NPC_State.scared;
        StartCoroutine(ScaredStateTimer());
    }
    IEnumerator ScaredStateTimer()
    {
        yield return new WaitUntil(() => Time.time > lastTimeScared + scaredTime);

        animator.SetBool("Scared", false);
        currState = NPC_State.walking;
        
        Vector3 pos = transform.GetChild(0).position;
        transform.GetChild(0).position = pos + Vector3.up * 0.15f;

        /*WanderingAI behavior = GetComponent<WanderingAI>();
        if (behavior != null)
        {
            behavior.enabled = true;
        }*/
    }
    void NotifyNearbyNPC()
    {
        var nearbyNPCs = Physics.OverlapSphere(transform.position, notifyAreaDistance, npcMask);
        foreach(var npc in nearbyNPCs)
        {
            npc.GetComponent<ReactiveTarget>().SetScaredState();
        }
    }
    private IEnumerator Die()
    {
        WanderingAI behavior = GetComponent<WanderingAI>();
        if (behavior != null)
        {
            behavior.enabled = false;
        }
        this.transform.Rotate(-90, 0, 0);
        Vector3 currPos = transform.position;
        transform.position = new Vector3(currPos.x, 0, currPos.z);
        yield return new WaitForSeconds(1.5f);
        PoliceController.Instance.CallThePolice();
        SceneController.Instance.SpawnNewNPC();
        Destroy(this.gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, notifyAreaDistance);
    }
}