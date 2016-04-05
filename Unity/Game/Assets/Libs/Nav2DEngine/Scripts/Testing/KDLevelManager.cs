//-------------------------------------------------------------------------------------------------
//  ©Copyright KDToons & Games 2014
//-------------------------------------------------------------------------------------------------

using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class KDLevelManager : MonoBehaviour
{
    private int _currentLevel;
    [SerializeField]
    private string[] _levelNames;
    [SerializeField]
    private Texture2D _btnTexture;
    private static KDLevelManager _instanceLevelManager;

    public void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (_instanceLevelManager == null)
            _instanceLevelManager = this;
        else if (_instanceLevelManager != this)
            DestroyImmediate(gameObject);

        _currentLevel = _levelNames.ToList().IndexOf(Application.loadedLevelName);
    }

    protected void OnGUI()
    {
        GUI.backgroundColor = new Color(0, 0, 0, 0);
        if (GUI.Button(new Rect(10, 10, Screen.width / 4f, Screen.height / 10f), _btnTexture))
        {
            int nextIndex = _currentLevel + 1;
            _currentLevel = nextIndex == _levelNames.Length ? 0 : nextIndex;
            string levelName = _levelNames[_currentLevel];
            Application.LoadLevel(levelName);
        }
    }
}