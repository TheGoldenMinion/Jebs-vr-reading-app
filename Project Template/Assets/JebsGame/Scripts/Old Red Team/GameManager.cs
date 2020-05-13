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

public enum GameState { LAGOON, SPACESHIP, SEQLVL }

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
    private string name;                // Characters name
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
    //CREATE A DICTIONARY WITH INTEGER VALUES FOR ALL LETTERS
    private void PopulateDictionary()
    {
        LettersDiction.Add("A", 0);
        LettersDiction.Add("B", 1);
        LettersDiction.Add("C", 2);
        LettersDiction.Add("D", 3);
        LettersDiction.Add("E", 4);
        LettersDiction.Add("F", 5);
        /*       LettersDiction.Add("g", 6);
               LettersDiction.Add("h", 7);
               LettersDiction.Add("i", 8);
               LettersDiction.Add("j", 9);
               LettersDiction.Add("k", 10);
               LettersDiction.Add("l", 11);
               LettersDiction.Add("m", 12);
               LettersDiction.Add("n", 13);
               LettersDiction.Add("o", 14);
               LettersDiction.Add("p", 15);
               LettersDiction.Add("q", 16);
               LettersDiction.Add("r", 17);
               LettersDiction.Add("s", 18);
               LettersDiction.Add("t", 19);
               LettersDiction.Add("u", 20);
               LettersDiction.Add("v", 21);
               LettersDiction.Add("w", 22);
               LettersDiction.Add("x", 23);
               LettersDiction.Add("y", 24);
               LettersDiction.Add("z", 25); */
        LettersDiction.Add("a", 26);
        LettersDiction.Add("b", 27);
        LettersDiction.Add("c", 28);
        LettersDiction.Add("d", 29);
        LettersDiction.Add("e", 30);
        LettersDiction.Add("f", 31);
    }
    //--------------------------------------------------------------------------------------

    //-------------------------INIT METHOD---------------------------------------------------
    public void startState()
    {
        print("Creating a new game state");

        // Set default properties (THESE ARE PLACEHOLDERS):

        activeLevel = "newRedScene";
        name = "Jeb";
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
    // getName()
    // --------------------------------------------------------------------------------------------------- 
    // Returns the characters name
    // ---------------------------------------------------------------------------------------------------
    public string getName()
    {
        return name;
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


}
