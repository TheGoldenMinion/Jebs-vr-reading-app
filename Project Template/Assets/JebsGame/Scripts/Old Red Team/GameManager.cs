using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//-------------------------GAME MANAGER SCRIPT-------------------------------------------
//
// DOES NOT DESTROY ON LOAD | HOLDS PROPERTIES, LETTERS, STATES | 
//
// To call GameManager Methods from other scripts use:
//
//                   GameManager.Instance.methodToCall();
//--------------------------------------------------------------------------------------

    //=====================THE DIFFERENT GAME STATES===================================
public enum GameState { LAGOON, SPACESHIP, SEQLVL }  //      THESE ARE PLACEHOLDERS

public delegate void OnStateChangeHandler();

public class GameManager : MonoBehaviour
{

    //--------------------------STATE MANAGEMENT STUFF---------------------------------------

    protected GameManager() { }
    private static GameManager instance = null;
    public event OnStateChangeHandler OnStateChange;
    public GameState gameState { get; private set; }

    //-----------------------------PROPERTIES-----------------------------------------------

    // Declare properties

    private string activeLevel;         // Active level
    private string charname;                // Characters name
    private string handedness;          // Players Handedness

    private int vit;                        // Characters Vitality
    private int exp;						// Characters Experience Points

    //--------------------------------------------------------------------------------------
    Scene LoadedScene;
    private letter_spawner letterSpawnScript;
    //--------------------------------------------------------------------------------------

    public Dictionary<string, int> LettersDiction = new Dictionary<string, int>();

    //--------------------------------------------------------------------------------------
    [SerializeField]
    int currentLetterInt;

    //--------------------------------------------------------------------------------------
    public static GameManager Instance
    {
        get
        {
            if (GameManager.instance == null)
            {
                DontDestroyOnLoad(GameManager.instance);
                GameManager.instance = new GameManager();
            }
            return GameManager.instance;
        }

    }
    //--------------------------------------------------------------------------------------
    void Awake()
    {      
        PopulateDictionary();
    }
    //--------------------------------------------------------------------------------------
    // called first
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {     
    }
    //--------------------------------------------------------------------------------------


    void Start()
    {
        //GET LETTER SPAWNER SCRIPT
        //letterSpawnScript = letterSpawner.GetComponent("letter_spawner") as letter_spawner;
        LoadedScene = SceneManager.GetActiveScene();
    }

    //--------------------------------------------------------------------------------------
    public void SetGameState(GameState state)
    {
        this.gameState = state;
        OnStateChange();
    }
    //--------------------------------------------------------------------------------------
    public void OnApplicationQuit()
    {
        GameManager.instance = null;
    }

    //--------------------------------------------------------------------------------------

    //-------------------------INIT METHOD---------------------------------------------------
    public void startState()
    {
        print("Creating a new game state");

        // Set default properties (THESE ARE PLACEHOLDERS):

        activeLevel = "newRedScene";
        charname = "Jeb";
        vit = 5;
        exp = 0;

        // Load Scene
        SceneManager.LoadScene("activeLevel");
    }

    //=================================EXAMPLE METHODS====================================================
    // ---------------------------------------------------------------------------------------------------
    // getLevel()
    // --------------------------------------------------------------------------------------------------- 
    // Returns the currently active level
    // ---------------------------------------------------------------------------------------------------
    public string getLevel()
    {
        return activeLevel;
    }


    // ---------------------------------------------------------------------------------------------------
    // setLevel()
    // --------------------------------------------------------------------------------------------------- 
    // Sets the currently active level to a new value
    // ---------------------------------------------------------------------------------------------------
    public void setLevel(string newLevel)
    {
        // Set activeLevel to newLevel
        activeLevel = newLevel;
    }


    // ---------------------------------------------------------------------------------------------------
    // getCharName()
    // --------------------------------------------------------------------------------------------------- 
    // Returns the characters name
    // ---------------------------------------------------------------------------------------------------
    public string getCharName()
    {
        return charname;
    }


    // ---------------------------------------------------------------------------------------------------
    // getHP()
    // --------------------------------------------------------------------------------------------------- 
    // Returns the characters hp
    // ---------------------------------------------------------------------------------------------------
    public int getVit()
    {
        return vit;
    }

    // ---------------------------------------------------------------------------------------------------
    // getMP()
    // --------------------------------------------------------------------------------------------------- 
    // Returns the characters mp
    // ---------------------------------------------------------------------------------------------------
    public int getEXP()
    {
        return exp;
    }

    //--------------------------------------------------------------------------------------
    //CREATE A DICTIONARY WITH INTEGER VALUES FOR ALL LETTERS
    //--------------------------------------------------------------------------------------
    private void PopulateDictionary()
    {
        //----------Upper Case-----------------
        //-------------------------------------
        LettersDiction.Add("A", 0);
        LettersDiction.Add("B", 1);
        LettersDiction.Add("C", 2);
        LettersDiction.Add("D", 3);
        LettersDiction.Add("E", 4);
        LettersDiction.Add("F", 5);
        LettersDiction.Add("G", 6);
        //----------Lower Case-----------------
        //-------------------------------------
        LettersDiction.Add("a", 26);
        LettersDiction.Add("b", 27);
        LettersDiction.Add("c", 28);
        LettersDiction.Add("d", 29);
        LettersDiction.Add("e", 30);
        LettersDiction.Add("f", 31);
        LettersDiction.Add("g", 32);
    }
}
