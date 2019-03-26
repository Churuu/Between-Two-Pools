using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationTriggerObject : MonoBehaviour
{
    public float TypingDelay = 0.1f;
    public Text convoText;
    public Image conversationImage;
    public ConversationObject conversation;
    public enum characterTypes { Otter = 1, Seal = 2, Frog = 3 };
    public characterTypes characters;

    [SerializeField]
    private List<AudioClip> convoStarter = new List<AudioClip>();

    [SerializeField]
    private AudioClip textSFX;

    bool entered = false;
    bool thisActive = false;
    bool playing = false;

    int currentIndex = 0;

    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject obj = col.gameObject;
        if (obj.CompareTag(Enum.GetName(typeof(characterTypes), characters)) && !entered)
        {
            currentIndex = 0;


            PlayConversation();
            thisActive = true;
            entered = true;
            switch (characters)
            {
                case characterTypes.Otter:
                    GetComponent<AudioSource>().clip = convoStarter[0];
                    GetComponent<AudioSource>().Play();
                    break;
                case characterTypes.Seal:
                    GetComponent<AudioSource>().clip = convoStarter[1];
                    GetComponent<AudioSource>().Play();
                    break;
                case characterTypes.Frog:
                    GetComponent<AudioSource>().clip = convoStarter[2];
                    GetComponent<AudioSource>().Play();
                    break;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && thisActive && !FindObjectOfType<MenuSystem>().P_Pressed && !playing) // Skips current dialog
        {
            StopAllCoroutines();
            PlayConversation();
        }
        else if (Input.GetKeyDown(KeyCode.E) && thisActive && !FindObjectOfType<MenuSystem>().P_Pressed && playing)
        {
            StopAllCoroutines();
            FinishCurrentDialog();
        }

    }

    void PlayConversation()
    {
        if (currentIndex < conversation.dialog.Length)
            StartCoroutine(PlayDialog(conversation));

        conversationImage.gameObject.SetActive(true);

        if (EventManager.instance.OnChatActiv != null)
            EventManager.instance.OnChatActiv();


        if (currentIndex == conversation.dialog.Length)
        {
            if (EventManager.instance.OnChatEnd != null)
                EventManager.instance.OnChatEnd();

            thisActive = false;
            conversationImage.gameObject.SetActive(false);
            convoText.text = "";
            Destroy(gameObject);
        }
    }

    IEnumerator PlayDialog(ConversationObject conversation)
    {
        convoText.text = "";
        conversationImage.sprite = conversation.characters[currentIndex];
        playing = true;
        foreach (var letter in conversation.dialog[currentIndex].ToCharArray())
        {
            convoText.text += letter;
            if (!GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().clip = textSFX;
                GetComponent<AudioSource>().Play();
            }
            yield return new WaitForSeconds(TypingDelay);
        }
        playing = false;
        currentIndex++;
    }

    void FinishCurrentDialog()
    {
        convoText.text = conversation.dialog[currentIndex];
        playing = false;
        currentIndex++;
    }
}
