using UnityEngine;

public class OtterKilld : MonoBehaviour
{

    //Jonas Thunberg 2019-02-26

    private void Start()
    {
        EventManager.instance.onKilld += OnKilld;

    }




    private void OnKilld(GameObject gameObject)
    {
        if (gameObject == this.gameObject)
        {
            GetComponent<Otter>().enabled = false;
            GetComponent<Animator>().enabled = false;
            GetComponent<PlayerController>().enabled = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            GetComponent<PolygonCollider2D>().enabled = false;
            GetComponent<EnterExit>().enabled = false;
            EventManager.instance.onKilld -= OnKilld;
        }
    }
}
