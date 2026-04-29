using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIEditor : MonoBehaviour
{
    [SerializeField] private Button saveButton;
    [SerializeField] private TMP_InputField level;
    [SerializeField] private Editor editor;
    public void OnSaveLevel()
    {
        string levelName = level.text;
        editor.SaveMap(levelName);
    }
    public void OnLoadLevel()
    {
        string levelName = level.text;
        editor.LoadLevel(levelName);
    }
    public void OnClear()
    {
        editor.ClearCurrentMap();
    }
}
