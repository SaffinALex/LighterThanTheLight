using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Le but de cette Classe est de générer un level de manière correcte selon des contraintes
 */
public class GeneratorLGI {
    /**
     * Permet de récupérer la distance entre deux scores
     */
    static int ScoreDistance(in int score1, in int score2){
        return (score2 - score1) * (score2 - score1);
    }

    public static List<Event> GenerateLevel(in float wantedScore, in float approximation, in List<string> levelFlow){
        List<Event> levelEvent = new List<Event>();
        List<List<int>> domains = new List<List<int>>();
        List<int> assignation = new List<int>();

        // 1 - Remplissage des domaines des différentes variables
        for(int f = 0; f < levelFlow.Count; f++){
            domains.Add(new List<int>());
            string typeLevel = levelFlow[f];
            for(int domainElement = 0; domainElement < App.ALL_EVENTS[typeLevel].Count; domainElement++){
                if (!ViolateContrainst(wantedScore, approximation, levelFlow, f, domainElement, domains, assignation)) domains[f].Add(domainElement);
            }
            if(domains[f].Count <= 0){
                Debug.Log("Impossible de généré un nieau (Approximation : " + approximation + " & Base : " + wantedScore + ")");
                return levelEvent;
            }
            domains[f] = shuffle(domains[f]);
        }
        
        int maxRecursion = 1000;
        bool level = AssignVariable(wantedScore, approximation, levelFlow, 0, 0, domains, ref assignation, ref maxRecursion);
        
        if(level){
            // Debug.Log("YEESSSSS" + maxRecursion);
            float total = 0;
            for(int i = 0; i < assignation.Count; i++){
                float tmp = GetScoreEvent(levelFlow[i], assignation[i]);
                // Debug.Log(i + " - " + levelFlow[i] + " -> " + assignation[i] + " = " + tmp);
                total += tmp;
                levelEvent.Add(App.ALL_EVENTS[levelFlow[i]][assignation[i]]);
            }
            Debug.Log("Niveau généré ayant un score de " + total + " (Approximation : " + approximation + " & Base : " + wantedScore + ")");
        }else{
            Debug.Log("NOPE " + maxRecursion);
        }

        return levelEvent;
    }

    //Permet d'assigner une variable récursivement et retourne l'assignation qui est consistante.
    static bool AssignVariable(in float wantedScore, in float approximation, in List<string> levelFlow, in float currentScore, in int variablePos, in List<List<int>> domains, ref List<int> assignation, ref int maxRecursion){
        if(--maxRecursion < 0) return false; //Protection pour éviter une loop trop grande
        if(variablePos >= domains.Count) return true;
        for (int i = 0; i < domains[variablePos].Count; i++) {
            int variableValue = domains[variablePos][i];
            List<int> newAssignation = new List<int>(assignation);
            newAssignation.Add(variableValue);
            List<List<int>> cloneDomain = CloneDomains(domains);

            if(ReduceDomains(wantedScore, approximation, levelFlow, currentScore, variablePos, variableValue, ref cloneDomain, newAssignation)){
                float scoreEvent = GetScoreEvent(levelFlow[variablePos], variableValue);
                if(AssignVariable(wantedScore, approximation, levelFlow, currentScore + scoreEvent, variablePos + 1, cloneDomain, ref newAssignation, ref maxRecursion)){
                    assignation = newAssignation; //On fait remonter l'assignation réussie
                    return true;
                }
            }
        }
        return false;
    }

    //Permet de récupérer le score pour un type et un index donné d'un event
    static float GetScoreEvent(in string levelType, in int indexLevel){
        Event e = App.ALL_EVENTS[levelType][indexLevel];
        return e.GetScore();
    }

    static List<int> shuffle(List<int> list){
        for (int i = 0; i < list.Count; i++) {
            int temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
        return list;
    }

    //Permet de réduire les différents domaines pour une variable donnée
    //Renvois faux si la solution n'est pas consistante
    //Renvois vrai sinon
    static bool ReduceDomains(in float wantedScore, in float approximation, in List<string> levelFlow, in float currentScore, in int variablePos, in int variableValue, ref List<List<int>> domains, in List<int> assignation){
        float score = GetScoreEvent(levelFlow[variablePos], variableValue);
        float minWantedScore = (wantedScore - approximation / 2);
        float maxWantedScore = (wantedScore + approximation / 2);
        int nextVariablePos = variablePos + 1;
        bool isLastLevel = (nextVariablePos == levelFlow.Count - 1);

        // Debug.Log("On souhaite attribuer la valeur " + variableValue + " à la variable " + variablePos + " d'un score de " + score);

        for(int domainIndex = variablePos + 1; domainIndex < domains.Count; domainIndex++){
            bool haveViolate = false;
            for(int v = 0; v < domains[domainIndex].Count; v++){
                float scoreDomainElement = GetScoreEvent(levelFlow[domainIndex], domains[domainIndex][v]);
                //On vérifie si l'assignation avec cette potentielle valeur ne viole pas de contraintes actuellement
                // Debug.Log(" -> La variable " + domainIndex + " pour une valeur de " + domains[domainIndex][v] + " ayant un score de " + scoreDomainElement);
                if(ViolateContrainst(wantedScore, approximation, levelFlow, domainIndex, domains[domainIndex][v], domains, assignation)){
                    // Debug.Log(" --> Ne correspond pas");
                    domains[domainIndex].RemoveAt(v);
                    v--;
                    haveViolate = true;
                }
            }
            // Debug.Log("La Taille du domaine pour " + domainIndex + " = " + domains[domainIndex].Count);
            if(domains[domainIndex].Count <= 0) return false; //Domaine vide on a pas de solutions
            if(haveViolate) domainIndex = variablePos;
        }

        return true; //La solution est consistante
    }

    //Permet de vérifier si une assignation viole une contrainte
    static bool ViolateContrainst(in float wantedScore, in float approximation, in List<string> levelFlow, in int variablePos, in int variableValue, in List<List<int>> domains, in List<int> assignation){
        float score = GetScoreEvent(levelFlow[variablePos], variableValue);
        float minWantedScore = (wantedScore - approximation / 2);
        float maxWantedScore = (wantedScore + approximation / 2);
        int nextVariablePos = variablePos + 1;
        bool isLastLevel = (nextVariablePos == levelFlow.Count - 1);

        float minScore = -1, maxScore = -1;
        CalculateExtremumScores(levelFlow, domains, assignation, ref maxScore, ref minScore, variablePos);

        // Debug.Log(variablePos + " = " + score + " | " + minScore + " : " + maxScore + " | " + minWantedScore + " : " + maxWantedScore + " --> " + !( score + minScore > maxWantedScore || score + maxScore < minWantedScore ));

        //Si on obtient le pire score possible et qu'il est au dessus du maximum alors valeure impossible
        if (score + minScore > maxWantedScore) return true;
        //Si on obtient le pire score possible et qu'il est au dessus du maximum alors valeure impossible
        if (score + maxScore < minWantedScore) return true;
        //--> Permet de faire des scores uniformes
        // if (score > wantedScore / levelFlow.Count + 2) return true;
        // if (score < wantedScore / levelFlow.Count - 2) return true;
        return false;
    }

    static List<List<int>> CloneDomains(List<List<int>> domains){
        List<List<int>> newDomains = new List<List<int>>();
        for(int d = 0; d < domains.Count; d++){
            newDomains.Add(new List<int>(domains[d]));
        }
        return newDomains;
    }

    //Permet d'obtenir les scores extremaux à partir d'un index donné dans les domains possible suivant
    static void CalculateExtremumScores(in List<string> levelFlow, in List<List<int>> domains, List<int> assignation, ref float maxScore, ref float minScore, in int index){
        minScore = 0; maxScore = 0;
        for(int i = 0; i < levelFlow.Count; i++){
            // Debug.Log(i);
            if(i != index){
                //Le cas où on a déjà assigné la variable
                if(i < assignation.Count){
                    // Debug.Log(i + " : Déjà assigné");
                    minScore += GetScoreEvent(levelFlow[i], assignation[i]);
                    maxScore += GetScoreEvent(levelFlow[i], assignation[i]);
                }
                //Le cas où le domaine de la variable existe déjà
                else if(i < domains.Count && domains[i].Count > 0){
                    // Debug.Log(i + " : Domain existe déjà");
                    MinMax extrems = new MinMax();
                    for (int j = 0; j < domains[i].Count; j++)
                    {
                        extrems.AddValue(GetScoreEvent(levelFlow[i], domains[i][j]));
                    }
                    minScore += extrems.Min;
                    maxScore += extrems.Max;
                }
                //Le cas où il n'y pas le domaine
                else{
                    // Debug.Log(i + " : Domain n'existe pas");
                    MinMax extrems = CalculateExtremumType(levelFlow[i]);
                    minScore += extrems.Min;
                    maxScore += extrems.Max;
                }
            }
        }
    }

    static void CalculateExtremumScoresType(in List<string> levelFlow, in List<List<int>> domains, ref float maxScore, ref float minScore, in int index){
        minScore = 0; maxScore = 0;
        for (int i = 0; i < levelFlow.Count; i++) {
            if(i != index){
                MinMax extrems = CalculateExtremumType(levelFlow[i]);
                minScore += extrems.Min;
                maxScore += extrems.Max;
            }
        }
    }

    static MinMax CalculateExtremumType(string Type){
        MinMax extrems = new MinMax();
        for(int i = 0; i < App.ALL_EVENTS[Type].Count; i++){
            extrems.AddValue(App.ALL_EVENTS[Type][i].GetScore());
        }
        return extrems;
    }
}
