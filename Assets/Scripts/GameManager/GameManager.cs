using UnityEngine;

static class GameManager
{
    //public static Voiceover voiceover;
    //public static Sfx sfx;
    //public static Music music;
    //public static Scoring scoring;

    static GameManager(){
        GameObject g = safeFind("__app");
        
        //sfx = (Sfx)SafeComponent( g, "Sfx" );
        //voiceover = (Voiceover)SafeComponent( g, "Voiceover" );
        //music = (Music)SafeComponent( g, "Music" );
        //scoring = (Scoring)SafeComponent( g, "Music" );
    }

    private static GameObject safeFind(string s) {
        GameObject g = GameObject.Find(s);
        if (g == null) Woe("GameObject " +s+ "  not on _preload.");
        return g;
    }

    private static Component SafeComponent(GameObject g, string s) {
        Component c = g.GetComponent(s);
        if (c == null) Woe("Component " +s+ " not on _preload.");
        return c;
    }

    private static void Woe(string error) {
        Debug.Log(">>> Cannot proceed... " +error);
        Debug.Log(">>> It is very likely you just forgot to launch");
        Debug.Log(">>> from scene zero, the _preload scene.");
    }
}
