using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class uiTraversal : MonoBehaviour
{
    //if selected = go to single/multiplayer select screen
    public void playerSelect()
    {
        SceneManager.LoadScene("PlayerSelectScreen");
    }

    //If selected = go to single player character select screen
    public void characterSelect1P()
    {
        SceneManager.LoadScene("CharacterSelectScreen1P");
    }

    //If selected = go to multi player character select screen
    //For Future Iterations of the Game
    //public void characterSelect2P()
    //{
    //    SceneManager.LoadScene("CharacterSelectScreen2P");
    //}

    //If selected = go to options screen
    public void optionsSelect()
    {
        SceneManager.LoadScene("OptionsScreen");
    }

    //If selected = return to main menu
    public void returnMenu()
    {
        SceneManager.LoadScene("StartScreen");
    }

    //If selected = quit game to desktop
    public void quitGame()
    {
        Application.Quit();

        //UnityEditor.EditorApplication.isPlaying = false;
    }
}
