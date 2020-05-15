using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public abstract class Biome {
    public readonly static int typeID = 0;
    public int GetTypeID() { return typeID; }

    // Liste de tous les biomes possible
    protected static List<Type> allBiomesType = new List<Type>();

    public Biome() {}

    /**
     * Permet de générer un biome selon un id donné
     */
    public static Biome GenerateBiome(int biomeId){
        for(int i = 0; i < allBiomesType.Count; i++){
            int typeID = (int)allBiomesType[i].GetField("typeID").GetValue(null); //On récupère la propriété typeID
            if(biomeId == typeID) return (Biome) Activator.CreateInstance(allBiomesType[i]); //Si match alors on renvoit une instance de la classe (l'équivalent de faire un new TypeBiome() )
        }
        //Le cas où on trouve pas 
        return (Biome) Activator.CreateInstance(allBiomesType[0]);
    }

    public static void LoadAllBiomeType(){
        //On récupère l'ensemble des types de classe qui appartiennent à la même Assembly que Biome ( Assembly est un espace où sont regroupé les Types, c'est propre au C# )
        Type[] subclassTypes = Assembly.GetAssembly(typeof(Biome)).GetTypes();

        //Pour chaques Types qu'on a obtenu, si celui-ci est une sous-classe de Biome alors on l'ajoute à allBiomesType
        for(int i = 0; i < subclassTypes.Length; i++){
            if( subclassTypes[i].IsSubclassOf(typeof(Biome)) ){
                allBiomesType.Add(subclassTypes[i]);
            }
        }
    }
}
