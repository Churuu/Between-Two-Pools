//Joakim
using UnityEngine;

public class CrushedPlayer : MonoBehaviour
{
    public float crushDistance = 1f;
    [SerializeField] Collider2D[] crushingColliders;
    [SerializeField] Vector3 crushedOfSet = new Vector3(0, 1, 0);
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Crushed();
    }

    public void Crushed()
    {

        RaycastHit2D hitDown = Physics2D.Raycast(transform.position + crushedOfSet, Vector2.down, crushDistance);

        RaycastHit2D hitUp = Physics2D.Raycast(transform.position + crushedOfSet, Vector2.up, crushDistance);

        if (hitDown.collider != null && hitUp.collider != null)
        {
            if (crushingColliders.Length > 0)
            {


                for (int i = 0; i < crushingColliders.Length; i++)
                {
                    for (int j = 0; j < crushingColliders.Length; j++)
                    {
                        Debug.Log(hitDown.collider + " = " + crushingColliders[i] + "    " + hitUp.collider + " " + crushingColliders[j]);
                        if (hitDown.collider == crushingColliders[i] && hitUp.collider == crushingColliders[j])
                        {
                            Debug.Log("Player crushed");
                            if (EventManager.instance.onKilld != null)
                            {
                                EventManager.instance.onKilld(this.gameObject);
                            }
                        }

                    }
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(transform.position + crushedOfSet, Vector3.down * crushDistance);
        Gizmos.DrawRay(transform.position + crushedOfSet, Vector3.up * crushDistance);
    }
}
