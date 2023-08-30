using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using UnityEngine.UI;

public class DisplayPrevMistake : MonoBehaviour
{
    private string DrivingTheoryGameDB = "URI=file:DrivingTheoryGame.db;foreign keys=true;"; //stated here so methods can access it

    public Image roadSignImage1;
    public TMPro.TMP_Text roadSignText1;
    public Image roadSignImage2;
    public TMPro.TMP_Text roadSignText2;
    public Image roadSignImage3;
    public TMPro.TMP_Text roadSignText3;
    public Image roadSignImage4;
    public TMPro.TMP_Text roadSignText4;
    public Image roadSignImage5;
    public TMPro.TMP_Text roadSignText5;

    public Sprite[] RoadSignImages = new Sprite[20];
    public IDictionary<string, string> descriptions = new Dictionary<string, string>(){
        {"1", "This is giving a compulsory instruction to a driver to turn left."},
        {"2", "This should prevent the driver turning left at a junction."},
        {"3", "This means that there are no motor vehicles allowed, so pedestrians and bicycles are free to use the road."},
        {"4", "To warn the drivers about an upcoming turn left."},
        {"5", "Instructs drivers that it is not safe to overtake the vehicle in front."},
        {"6", "Warns drivers that they're leaving a one-way roadway and entering a raodway with opposing traffic."},
        {"7", "Indicates that there are road workers in/near the roadway."},
        {"8", "This is giving a compulsory instruction to a driver to turn right."},
        {"9", "Warns drivers that the road ahead has a speed bump or a series of speed bumps."},
        {"10", "Indicates to drivers that there is a mini-roundabout."},
        {"11", "Indicates to drivers to stop and not to continue until it is safe."},
        {"12", "Indicates that parking there is prohibited."},
        {"13", "To warn the drivers about an upcoming turn right."},
        {"14", "Means there may be water ,ice or snow on the road."},
        {"15", "Used to alert drivers of damage in the road surface."},
        {"16", "This should prevent the driver turning right at a junction."},
        {"17", "Indicated that ahead is a junction where the thicker line is the route with right of way."},
        {"18", "This indicates to drivers to give way to oncoming traffic."},
        {"19", "This indicates to drivers that the maximum speed limit is 30 mph."},
        {"20", "This indicates that cycling is prohibited beyound the sign."}
        };//dictionary for the road sign descriptions

    // Start is called before the first frame update
    void Start()
    {
        showRoadSigns();
    }

    public void showRoadSigns()
    {
        var loginDB = LoginDB.Instance;
        string userName = loginDB.userInput.text;
        // creates the db connection
        using (var connection = new SqliteConnection(DrivingTheoryGameDB))
        {
            connection.Open();

            // create command object allowing the code to control db
            using (var command = connection.CreateCommand())
            {
                // creates Account_Table and 2 fields: username and password
                command.CommandText = "SELECT question FROM FeedBack_Table WHERE user_id = '" + userName + "' ORDER BY mistake_id DESC;";
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    int i = 0;
                    int iteration = 0;
                    int[] roadSignNums = new int[5];
                    int rsNum;
                    while (reader.Read())
                    {
                        rsNum = Convert.ToInt32(reader["question"]);
                        if (roadSignNums[4].ToString() == "0")
                        {
                            roadSignNums[i] = rsNum - 1; //adds roadsign to array
                        }
                        i++;
                    }
                    while (iteration < 5)
                    {
                        if (roadSignNums[iteration] != 0)
                        {
                            if (iteration == 0)
                            {
                                roadSignImage1.sprite = RoadSignImages[roadSignNums[iteration]];
                                roadSignText1.text = descriptions[(roadSignNums[iteration] + 1).ToString()];
                            }
                            if (iteration == 1)
                            {
                                roadSignImage2.sprite = RoadSignImages[roadSignNums[iteration]];
                                roadSignText2.text = descriptions[(roadSignNums[iteration] + 1).ToString()];
                            }
                            if (iteration == 2)
                            {
                                roadSignImage3.sprite = RoadSignImages[roadSignNums[iteration]];
                                roadSignText3.text = descriptions[(roadSignNums[iteration] + 1).ToString()];
                            }
                            if (iteration == 3)
                            {
                                roadSignImage4.sprite = RoadSignImages[roadSignNums[iteration]];
                                roadSignText4.text = descriptions[(roadSignNums[iteration] + 1).ToString()];
                            }
                            if (iteration == 4)
                            {
                                roadSignImage5.sprite = RoadSignImages[roadSignNums[iteration]];
                                roadSignText5.text = descriptions[(roadSignNums[iteration] + 1).ToString()];
                            }
                        }
                        //displays each roadsign on individual space
                        iteration++;
                    }

                    reader.Close();
                }
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
}
