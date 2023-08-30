using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerQuests : MonoBehaviour
{
    public static Queue<int> takenQuests = new Queue<int>(3);

    public Image quest1;
    public Image quest2;
    public Image quest3;

    public static float points;
    public TMPro.TMP_Text gamePoint;

    public TMPro.TMP_Text remainingQuests;


    // Start is called before the first frame update
    void Start()
    {
        points = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (takenQuests.Count == 3)
        {
            quest1.gameObject.SetActive(true);
            quest2.gameObject.SetActive(true);
            quest3.gameObject.SetActive(true);
        }
        else if (takenQuests.Count == 2)
        {
            quest1.gameObject.SetActive(true);
            quest2.gameObject.SetActive(true);
            quest3.gameObject.SetActive(false);
        }
        else if (takenQuests.Count == 1)
        {
            quest1.gameObject.SetActive(true);
            quest2.gameObject.SetActive(false);
            quest3.gameObject.SetActive(false);
        }
        else
        {
            quest1.gameObject.SetActive(false);
            quest2.gameObject.SetActive(false);
            quest3.gameObject.SetActive(false);
        }
        //turns on the images depending on how many quests are accepted


        gamePoint.text = "Points: " + points.ToString();
    }

    

}
