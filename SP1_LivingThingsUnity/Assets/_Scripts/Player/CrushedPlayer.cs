//Joakim
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushedPlayer : MonoBehaviour {
    public float crushDistance = 1f;
    [SerializeField] Collider2D[] crushingColliders;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Crushed(); 
	}

    public bool Crushed()
    {

        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down, crushDistance);
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up, crushDistance);

        if (hitDown.collider != null && hitUp.collider != null)
            if(crushingColliders != null)
                for(int i = 0; i < crushingColliders.Length; i++)
                {
                    for (int j = 0; j < crushingColliders.Length; j++)
                    {
                        if (hitDown.collider == crushingColliders[i] && hitUp.collider == crushingColliders[j])
                        {
                            Debug.Log("Player crushed");
                            EventManager.instance.onKilld(this.gameObject);
                            return true;
                        }
                           
                    }  
                }
        return false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(transform.position, Vector3.down * crushDistance);
        Gizmos.DrawRay(transform.position, Vector3.up * crushDistance);
    }
}
