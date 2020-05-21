using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    [SerializeField] protected List<Event> specialBossEvent;
    [SerializeField] protected List<Event> rewardsEvent;
    [SerializeField] protected List<Event> specialRewardsEvent;
    [SerializeField] protected EnemyList enemiesList;
    static protected TreeNode treeNode;
    public WeaponPlayer baseWeaponPrefab;
    static public WeaponPlayer baseWeapon;

    public static RessourcesLoader ressourcesLoader;

    public string weaponsPrefabsFolderPath;
    public string weaponsPrefabsRewardsFolderPath;
    public string shipUpgradesPrefabsFolderPath;
    public string dashUpgradesPrefabsFolderPath;
    public string ondeUpgradesPrefabsFolderPath;
    public string weaponUpgradesPrefabsFolderPath;

    static public PlayerShip playerShip;
    public PlayerShip playerShipPrefab;

    static public LoadingPanelManager loadingManager;
    public LoadingPanelManager loadingManagerPrefab;

    public static Dictionary<string, List<Event>> ALL_EVENTS = new Dictionary<string, List<Event>>();

    public void Awake(){
        DontDestroyOnLoad(gameObject);
        app = this;
        SetEnemyList(enemiesList);
    }

    void Start(){
        playerShip = Instantiate(playerShipPrefab);
        playerShip.InitConstraints();
        DontDestroyOnLoad(playerShip); //Sauvegarde de la pérénité du gameobject tout au long du jeu
        playerShip.gameObject.SetActive(false);

        loadingManager = Instantiate(loadingManagerPrefab);
        DontDestroyOnLoad(loadingManager);

        baseWeapon = baseWeaponPrefab;

        playerManager = new PlayerManager(playerShip.GetComponent<PlayerShip>());
        playerManager.getInventory().setParent(this.transform);
        ressourcesLoader = new RessourcesLoader(this.transform,
                                                weaponsPrefabsFolderPath,
                                                shipUpgradesPrefabsFolderPath,
                                                dashUpgradesPrefabsFolderPath,
                                                ondeUpgradesPrefabsFolderPath, 
                                                weaponUpgradesPrefabsFolderPath,
                                                weaponsPrefabsRewardsFolderPath);

        InputManager.Subscribe(GetComponent<KeyboardInputSystem>());

        DontDestroyOnLoad(sfxObject.gameObject);
        sfx = sfxObject;

        sfx.PlaySound("TestSound", 3);

        addEvents();
        SceneManager.LoadScene(1);
    }

    static public PlayerShip GetPlayerShip(){
        Debug.Log("VALEUR PLAYER SHIP" + playerShip);
        return playerShip;
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
        ALL_EVENTS.Add("SpecialBoss", specialBossEvent);
        ALL_EVENTS.Add("Reward", rewardsEvent);
        ALL_EVENTS.Add("SpecialReward", specialRewardsEvent);

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

    static public TreeNode GetTreeNode()
    {
        return treeNode;
    }

    /**
     * Commence un niveau
     */
    static public void StartLevel(){
        treeNode.gameObject.SetActive(false);
        SceneManager.LoadScene("__Level");
    }

    /**
        * joueur mort, appel ui fin partie
        */
    static public void EndGame()
    {
        playerManager.endOfLevelRoutine();
        PanelUIManager.GetPanelUI().ToggleEndGamePanel();
    }
    /**
     * joueur mort, fermeture de la partie
     */
    static public void CloseGame()
    {
        Destroy(treeNode.gameObject);
        PanelUIManager.GetPanelUI().inGame = false;
        treeNode = null;
        PanelUIManager.GetPanelUI().ressourcePanel.gameObject.SetActive(false);
    }

    /**
    * Niveau terminé, appel ui fin niveau
    */
    static public void EndLevel()
    {
        PanelUIManager.GetPanelUI().ToggleEndLevelPanel();
    }
    /**
     * Niveau terminé,appel ui fin niveau
     */
    static public void CloseLevel()
    {
        playerManager.endOfLevelRoutine();
        treeNode.gameObject.SetActive(true);

        if (playerManager.getInventory().isNotEmpty())
           EquipmentManager.GetEquipmentUI().openLoot();
    }

   static public void testLoot()
    {
        ReadOnlyCollection<GameObject> allUpgrades = App.ressourcesLoader.getUpgrades();
        ReadOnlyCollection<GameObject> allWeapons = App.ressourcesLoader.getWeapons();

        for (int i = 0; i < 4; i++)
        {
            bool isAWeapon = Random.Range(0, 3) == 0; //1 / 3 chance
            if (isAWeapon)
            {
                Debug.Log(allWeapons[Random.Range(0, allWeapons.Count)]);
                GameObject go = Instantiate(allWeapons[Random.Range(0, allWeapons.Count)]);
                playerManager.getInventory().addWeaponInventory(go.GetComponent<WeaponPlayer>());
            }
            else
            {
                Debug.Log(allUpgrades[Random.Range(0, allUpgrades.Count)]);
                GameObject go = Instantiate(allUpgrades[Random.Range(0, allWeapons.Count)]);
                playerManager.getInventory().addUpgradeInventory(go.GetComponent<Upgrade>());
            }
        }
        //

        // if (playerManager.getInventory().isNotEmpty())
        //     EquipmentManager.GetEquipmentUI().openLoot();
    }
}
