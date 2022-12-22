using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public GameObject meleeAttackObject;

    [SerializeField]
    private MeleeData data;

    public bool isSwinging;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.red);
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
        if (Physics.Raycast(meleeAttackObject.transform.position, meleeAttackObject.transform.forward, out hit, data.swingRange))
        {
            if (hit.collider.gameObject != null)
            {
                Health target = hit.transform.GetComponent<Health>();
                if (target != null) //  only do this if component is found, target is not null (nothing)
                {
                    Debug.Log("melee hit!");
                    target.TakeDamage(damage: data.damage);
                }
            }
        }
    }

}
