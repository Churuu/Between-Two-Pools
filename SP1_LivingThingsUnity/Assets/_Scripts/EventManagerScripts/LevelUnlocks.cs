using UnityEngine;
using UnityEngine.UI;

public class LevelUnlocks : MonoBehaviour
{

    public LevelUnlocksSave levelUnlocksSave;
    public Button[] levelButton;
    private void Start()
    {
        //  SaveCharacter(0);
        LoadCharacter();
    }
    void Update()
    {
        //  if (Input.GetKeyDown(KeyCode.S))
        //       SaveCharacter(characterData, 0);

        //   if (Input.GetKeyDown(KeyCode.L))
        //        characterData = LoadCharacter(0);
    }

    public void SaveCharacter(int characterSlot)
    {
        int numer = 1;

        PlayerPrefs.SetInt(levelUnlocksSave.stringNameLevel[characterSlot], numer);
        PlayerPrefs.Save();
    }

    public void LoadCharacter()
    {
        // LevelUnlocksSave lod = new LevelUnlocksSave();

        for (int i = 0; i < levelUnlocksSave.stringNameLevel.Length; i++)
        {
            if (PlayerPrefs.GetInt(levelUnlocksSave.stringNameLevel[i]) != null)
            {
                if (PlayerPrefs.GetInt(levelUnlocksSave.stringNameLevel[i]) > 0)
                {
                    if (i < levelButton.Length)
                    {
                      
                        levelButton[i].interactable = true;
                    }
                }
                else
                if (i < levelButton.Length)
                {
                    levelButton[i].interactable = false;
                }
            }
        }

    }
}
