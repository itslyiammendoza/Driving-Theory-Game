using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;


public class LevelSelectMenu : MonoBehaviour
{

    public int unlockedLevels = 1;
    private int temp = 0;
    public Button[] lvlButtons;

    private string DrivingTheoryGameDB = "URI=file:DrivingTheoryGame.db;foreign keys=true;";


    void Start()
    {

        for  (int i = 0; i < lvlButtons.Length; i++)
        {
            if (temp < unlockedLevels)
            {
                lvlButtons[i].interactable = true;
                temp += 1;
            }
            else
            {
                lvlButtons[i].interactable = false;
                temp += 1;
            }
        }//iterates through checking what levels have been completed and displaying them appropriately

        CreateDB();
        checkDB();
    }

    public void CreateDB()
    {
        // creates the db connection
        using (var connection = new SqliteConnection(DrivingTheoryGameDB))
        {
            connection.Open();

            // create command object allowing the code to control db
            using (var command = connection.CreateCommand())
            {
                // creates Account_Table and 2 fields: username and password
                command.CommandText = "CREATE TABLE IF NOT EXISTS Levels_Table (level_id INTEGER NOT NULL,user_id STRING, PRIMARY KEY(user_id, level_id), FOREIGN KEY(user_id) REFERENCES Account_Table(username) ON DELETE CASCADE);";
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    public void checkDB()
    {
        var loginDB = LoginDB.Instance;
        string userName = loginDB.userInput.text;
        //allows for a link between the inputted username from the login screen
        bool created = false;

        using (var connection = new SqliteConnection(DrivingTheoryGameDB))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Levels_Table;";

                using (SqliteDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read() && created == false)
                        if (reader["user_id"].ToString() == userName)
                        {
                            created = true;
                        }//checks to see if base data has already been inputted

                    reader.Close();

                    if (created == true)
                    {
                        editLevel(unlockedLevels);
                        Debug.Log("USER DATA UPDATED.");
                    }
                    else
                    {
                        createLevels(unlockedLevels);
                        Debug.Log("USER DATA CREATED.");
                    }

                }
                connection.Close();
            }

        }
    }

    public void createLevels(int unlockedLevels)
     {
        var loginDB = LoginDB.Instance;
        string userName = loginDB.userInput.text;
        //allows for a link between the inputted username from the login screen

        using (var connection = new SqliteConnection(DrivingTheoryGameDB))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO Levels_Table (user_id, level_id) VALUES('" + userName + "', '" + unlockedLevels + "');";
                command.ExecuteNonQuery();
                //uses the username as a foreign key to input the unlocked levels
            }
            connection.Close();
        }

        Debug.Log("CONFIRM USER DATA MADE");
    }

    //add level data first then update after
    public void editLevel(int unlockedLevels)
    {
        var loginDB = LoginDB.Instance;
        string userName = loginDB.userInput.text;
        //allows for a link between the inputted username from the login screen
        Debug.Log(userName);
        Debug.Log(unlockedLevels);

        using (var connection = new SqliteConnection(DrivingTheoryGameDB))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE Levels_Table SET level_id  = '" + unlockedLevels + "' WHERE user_id = '" + userName + "';";
                //updates the completed levels in the table using the corresponding username

            }
            connection.Close();
        }
        Debug.Log("Works");
    }

}
