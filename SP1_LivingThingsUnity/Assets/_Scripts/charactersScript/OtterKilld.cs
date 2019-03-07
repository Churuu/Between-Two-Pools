using UnityEngine;

public class OtterKilld : MonoBehaviour
{
    [SerializeField] Sprite ghost;
    [SerializeField] int layerGhost = 15;
    public GameObject[] animChild; // 1. Normal/Red 2. RedActive 3. Blue 4. BlueActive
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
            GetComponent<Otter>().enabled = false;
            for (int i = 0; i < animChild.Length; i++)
            {
                if (animChild[i] != null)
                {
                    animChild[i].GetComponent<Animator>().enabled = false;
                    animChild[i].GetComponent<SpriteRenderer>().sprite = ghost; 
                }
            }
        //    GetComponent<Animator>().enabled = false;
            GetComponent<PlayerController>().enabled = false;
            GetComponent<CrushedPlayer>().enabled = false;
            GetComponent<ContactWithEnemy>().enabled = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            gameObject.layer = layerGhost;
            GetComponent<EnterExit>().enabled = false;
          //  GetComponent<SpriteRenderer>().sprite = ghost;
            notKilld = false;
            EventManager.instance.onKilld -= OnKilld;
            if (EventManager.instance.OnGameOver != null)
            {
                EventManager.instance.OnGameOver();
            }

            for (int i = 0; i < animChild.Length; i++)
            {
                if (animChild[i] != null && animChild[i].GetComponent<AudioSource>().enabled == true)
                {
                    animChild[i].GetComponent<PlayerAudioScript>().Death();
                }
            }

            
        }
    }
}
