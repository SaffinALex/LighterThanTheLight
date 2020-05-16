using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(KeyboardInputSystem))]
public class App : MonoBehaviour
{
    public static App app;
    public MusicManager sfxObject;
    public static MusicManager sfx;
    public static PlayerManager playerManager;
    protected static LevelGeneratorInfo levelGeneratorInfo;
    protected static int difficulty = 0;
    protected static EnemyList enemyList;
    [SerializeField] public List<Vector3> spawnList;
    [SerializeField] protected List<Event> waveEvents;
    [SerializeField] protected List<Event> disasterEvents;
    [SerializeField] protected List<Event> bossEvents;
    [SerializeField] protected EnemyList enemiesList;
    static protected TreeNode treeNode;
    public WeaponPlayer baseWeaponPrefab;
    static public WeaponPlayer baseWeapon;

    static public PlayerShip playerShip;
    public PlayerShip playerShipPrefab;

    public static Dictionary<string, List<Event>> ALL_EVENTS = new Dictionary<string, List<Event>>();

    public void Awake(){
        DontDestroyOnLoad(gameObject);
        app = this;
        SetEnemyList(enemiesList);
    }

    void Start(){
        playerShip = Instantiate(playerShipPrefab);
        playerShip.enabled = false;
        baseWeapon = baseWeaponPrefab;

        playerManager = new PlayerManager(playerShip.GetComponent<PlayerShip>());

        InputManager.Subscribe(GetComponent<KeyboardInputSystem>());

        DontDestroyOnLoad(sfxObject.gameObject);
        sfx = sfxObject;

        sfx.PlaySound("TestSound", 3);

        addEvents();

        SceneManager.LoadScene(1);
    }

    public static bool IsInit(){
        return app != null;
    }

    //Permet de set le levelGeneratorInfo
    public static void SetLevelGenerator(LevelGeneratorInfo l){
        levelGeneratorInfo = l;
    }

    //Permet de get le levelGeneratorInfo
    public static LevelGeneratorInfo GetLevelGenerator(){
        return levelGeneratorInfo;
    }

    //Permet de set la difficulté
    public static void SetDifficulty(int d)
    {
        difficulty = d;
    }

    //Permet de get la difficulté
    public static int GetDifficulty()
    {
        return difficulty;
    }

    //Permet de set l'enemyList
    public static void SetEnemyList(EnemyList e)
    {
        enemyList = e;
    }

    //Permet de get l'enemyList
    public static EnemyList GetEnemyList()
    {
        return enemyList;
    }

    //Permet de get l'enemyList
    public static List<Vector3> GetSpawn()
    {
        return app.spawnList;
    }

    //Remplit le Dico All_Events d'events
    public void addEvents()
    {
        ALL_EVENTS.Add("Wave", waveEvents);
        ALL_EVENTS.Add("Disaster", disasterEvents);
        ALL_EVENTS.Add("Boss", bossEvents);

        /*
        var enumerator = ALL_EVENTS.GetEnumerator();
        while (enumerator.MoveNext())
        {
            Debug.Log(enumerator.Current.Key+" : "+enumerator.Current.Value);
        }
        */
    }

    /**
     * Permet d'ajouter l'objet contenant l'arborescence des niveaux
     */
    static public void SetTreeNode(TreeNode tree){
        treeNode = tree;
    }

    /**
     * Commence un niveau
     */
    static public void StartLevel(){
        treeNode.gameObject.SetActive(false);
        SceneManager.LoadScene("LevelPrototype");
    }

    /**
     * Finir un niveau
     */
    static public void EndLevel(){
        SceneManager.LoadScene("NodeNavigation");
        playerManager.endOfLevelRoutine();
        treeNode.gameObject.SetActive(true);
    }
}
