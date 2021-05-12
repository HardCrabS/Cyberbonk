using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealUp : MonoBehaviour
{
    public int heal = 30;
    public float reloadTime = 10;

    bool canHeal = true;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 1, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerCharacter>() && canHeal)
        {
            other.GetComponent<PlayerCharacter>().Hurt(-heal);
            canHeal = false;
            GetComponent<TextMeshPro>().enabled = false;
            StartCoroutine(ReloadHeal());
        }
    }
    IEnumerator ReloadHeal()
    {
        yield return new WaitForSeconds(reloadTime);
        canHeal = true;
        GetComponent<TextMeshPro>().enabled = true;
    }
}