using UnityEngine;

public class SealKilld : MonoBehaviour
{

    [SerializeField] Sprite ghost;
    [SerializeField] int layerGhost = 15;
    public bool notKilld = true;
    //Jonas Thunberg 2019-02-26

    private void Start()
    {
        EventManager.instance.onKilld += OnKilld;

    }




    private void OnKilld(GameObject gameObject)
    {
        if (gameObject == this.gameObject)
        {
            //ljud
            if (transform.GetChild(0) != null)
            {
                transform.GetChild(0).gameObject.active = false;
            }
            //GetComponent<Seal>().enabled = false;
            GetComponent<Animator>().enabled = false;
            GetComponent<PlayerController>().enabled = false;
            GetComponent<CrushedPlayer>().enabled = false;
            GetComponent<ContactWithEnemy>().enabled = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            gameObject.layer = layerGhost;
            GetComponent<EnterExit>().enabled = false;
            GetComponent<SpriteRenderer>().sprite = ghost;

            notKilld = false;

            EventManager.instance.onKilld -= OnKilld;

            if (EventManager.instance.OnGameOver != null)
            {
                EventManager.instance.OnGameOver();
            }
            GetComponent<PlayerAudioScript>().Death();
        }
    }
}
