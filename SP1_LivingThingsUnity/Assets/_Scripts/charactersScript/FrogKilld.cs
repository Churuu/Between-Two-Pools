using UnityEngine;

public class FrogKilld : MonoBehaviour
{
    [SerializeField] GameObject normel;
    [SerializeField] GameObject rocke;
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
            GetComponent<Frog>().DestroyTounge();
            if (transform.GetChild(0) != null)
            {
                transform.GetChild(0).gameObject.active = false;
            }

            
            GetComponent<Frog>().enabled = false;
            normel.SetActive(false);
            rocke.SetActive(false);
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

            if (normel.GetComponent<AudioSource>().enabled == true)
            {
                normel.GetComponent<PlayerAudioScript>().Death();
            }
            else
            {
                rocke.GetComponent<PlayerAudioScript>().Death();
            }


        }
    }
}
