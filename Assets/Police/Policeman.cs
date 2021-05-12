using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Policeman : MonoBehaviour, IDamagable
{
    public float health = 100;
    public GameObject deathPart;

    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerCharacter.Instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player);
    }
    public void ReactToHit(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            var part = Instantiate(deathPart, transform.position, transform.rotation);
            Destroy(part, 1);
            Destroy(gameObject);
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
        Destroy(this.gameObject);
    }
}