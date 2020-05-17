using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;
using UnityEngine.UI;
using TMPro;

public class ScoresPanel : MonoBehaviour
{
    public void UpdateLeaderBoard()
    {
        List<ScoreManager.ScoreElement> bestScore = ScoreManager.GetBestsScores();
        Transform scoreContainerTransform = gameObject.transform.Find("Content").Find("ScoreContainer");
        int i = 0;
        foreach (ScoreManager.ScoreElement score in bestScore)
        {
            GameObject obj = scoreContainerTransform.GetChild(i).gameObject;
            string namestr = "\"";
            namestr += score.name;
            namestr += "\"";
            obj.transform.Find("Nom").GetComponent<TextMeshProUGUI>().text = namestr;

            string scorestr = "(";
            scorestr += score.score.ToString();
            scorestr += " points)";
            obj.transform.Find("point").GetComponent<TextMeshProUGUI>().text = scorestr;
            i++;
        }
    }
}
