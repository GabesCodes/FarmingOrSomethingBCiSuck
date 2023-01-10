using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public GameObject meleeAttackObject;

    [SerializeField]
    private MeleeData data;

    [SerializeField]
    private LayerMask damagableLayer;

    public bool isSwinging;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isSwinging)
        {
            isSwinging = true;
            TryMeleeAttack();
        }
        else
        {
            isSwinging = false;
        }
    }

    private void TryMeleeAttack()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position,Camera.main.transform.forward, out hit, data.swingRange, damagableLayer))
        {
            Health target = hit.collider.GetComponent<Health>();
            if (target != null)
            {
                    Debug.Log("melee hit!");
                    target.TakeDamage(damage: data.damage);   
            }
        }
    }

}
