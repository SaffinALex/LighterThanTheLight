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
    public static PlayerManager playerManager = new PlayerManager();
    protected static LevelGeneratorInfo levelGeneratorInfo;
    protected static float difficulty = 0.0f;
    protected static EnemyList enemyList;
    [SerializeField] protected List<WaveEvent> waveEvents;
    [SerializeField] protected List<DisasterEvent> DisasterEvents;
    [SerializeField] protected List<BossEvent> BossEvents;



    public void Awake(){
        DontDestroyOnLoad(gameObject);
        app = this;
    }

    void Start(){
        InputManager.Subscribe(GetComponent<KeyboardInputSystem>());

        DontDestroyOnLoad(sfxObject.gameObject);
        sfx = sfxObject;

        sfx.PlaySound("TestSound", 3);

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
    public static void SetDifficulty(float d)
    {
        difficulty = d;
    }

    //Permet de get la difficulté
    public static float GetDifficulty()
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
}
