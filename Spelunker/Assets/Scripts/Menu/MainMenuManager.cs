using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public IntVariable seedVariable;

    public string playSceneName;

    public TMP_InputField seedInputField;

    void Start()
    {
        seedInputField.onEndEdit.AddListener(this.UpdateSeed);

        // set initial random seed
        UpdateSeed(Random.Range(0, int.MaxValue));
    }

    public void UpdateSeed(string seed)
    {
        int intSeed;
        if (int.TryParse(seed, out intSeed))
        {
            UpdateSeed(intSeed);
        }
    }

    public void UpdateSeed(int seed)
    {
        this.seedVariable.Value = seed;
        seedInputField.text = seed.ToString();
    }


        public void StartGame()
    {
        SceneManager.LoadScene(playSceneName);
    }
}
