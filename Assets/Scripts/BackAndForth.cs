using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAndForth : MonoBehaviour
{
    public float speed = 3.0f;
    public float maxZ = 16.0f;
    public float minZ = -16.0f;

    private int _direction = 1;
    void Update()
    {
        transform.Translate(0, 0, _direction * speed * Time.deltaTime);
        bool bounced = false;
        if (transform.localPosition.z > maxZ || transform.localPosition.z < minZ)
        {
            _direction = -_direction;
            bounced = true;
        }
        if (bounced)
        {
            transform.Translate(0, 0, _direction * speed * Time.deltaTime);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector3 currPos = transform.position;
        Gizmos.DrawLine(currPos + transform.forward * minZ + transform.right, currPos + transform.forward * maxZ + transform.right);
        //Gizmos.DrawLine(new Vector3(currPos.x, currPos.y, currPos.z + minZ), new Vector3(currPos.x, currPos.y, currPos.z + maxZ));
        //Gizmos.DrawLine(new Vector3(currPos.x + minZ, currPos.y, currPos.z), new Vector3(currPos.x + maxZ, currPos.y, currPos.z));
    }
}