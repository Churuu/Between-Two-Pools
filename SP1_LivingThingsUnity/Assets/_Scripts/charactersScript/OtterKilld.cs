using UnityEngine;

public class OtterKilld : MonoBehaviour
{
    [SerializeField] Sprite ghost;
    [SerializeField] int layerGhost = 15;
    public bool notKilld = true;
    //Jonas Thunberg 2019-02-26

    private void Start()
    {
        EventManager.instance.onKilld += OnKilld;

    }
    private void Update()
    {
        if (Time.timeSinceLevelLoad > 5)
        {
            if (EventManager.instance.onKilld != null)
                EventManager.instance.onKilld(gameObject);
        }

    }



    private void OnKilld(GameObject gameObject)
    {
        if (gameObject == this.gameObject)
        {
            //ljud
            GetComponent<Otter>().enabled = false;
            GetComponent<Animator>().enabled = false;
            GetComponent<PlayerController>().enabled = false;
            GetComponent<CrushedPlayer>().enabled = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            gameObject.layer = layerGhost;
            GetComponent<EnterExit>().enabled = false;
            GetComponent<SpriteRenderer>().sprite = ghost;
            notKilld = false;
            EventManager.instance.onKilld -= OnKilld;

        }
    }
}
