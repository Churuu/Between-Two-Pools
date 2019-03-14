using UnityEngine;

public class MagneticBoxAddToOtter : MonoBehaviour
{
    [SerializeField] private float minDistanceStopMoving = 2.35f;
    private GameObject[] characters;
    bool One = true;
    private Rigidbody2D rb2D;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        characters = new GameObject[2];
        characters[0] = GameObject.Find("Frog");
        characters[1] = GameObject.Find("Seal");
      //  Debug.Log(characters[1]);
    }
    // Update is called once per frame
    void Update()
    {
        if (One)
        {
            if (EventManager.instance.OnStartAddMagneticBox != null)
            {
               // Debug.Log("Im In Guys");
                EventManager.instance.OnStartAddMagneticBox(gameObject.GetComponent<Rigidbody2D>());
            }
            One = false;
        }
        for (int i = 0; i < characters.Length; i++)
        {
            if(characters[i] != null)
            {
                float Distance = Vector3.Distance(this.transform.position, characters[i].transform.position);

                if (Distance < minDistanceStopMoving)
                {
                    Debug.Log("Nu ska den vara stilla");
                    rb2D.velocity = new Vector2(0, 0);
                }
            }
        }
    }
}
