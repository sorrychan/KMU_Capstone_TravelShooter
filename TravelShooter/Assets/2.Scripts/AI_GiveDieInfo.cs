using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_GiveDieInfo : MonoBehaviour
{
    public GameObject Parent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Object")/*&&collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude> CollisionSpeed)*/      //태그가 불릿이나 오브젝트이고, 속도가 일정 이상이 되면
        {
           Parent.GetComponent<AI>().enemyState = AI.EnemyState.die;
        }
    }
}
