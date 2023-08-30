using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;

public class GameOver : MonoBehaviour
{

    public GameObject GameUI;
    public GameObject GameOverUI;

    public TMPro.TMP_Text highScore;
    public TMPro.TMP_Text currentScore;
    public TMPro.TMP_Text bestTime;
    public TMPro.TMP_Text currentTime;

    public QuestGiver questGiver;
    public GameObject timer;

    private string DrivingTheoryGameDB = "URI=file:DrivingTheoryGame.db;foreign keys=true;"; //stated here so methods can access it

    public string DBhighscore;
    public string DBbestTime;

    public void Start()
    {
        CreateDB();
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
                command.CommandText = "CREATE TABLE IF NOT EXISTS Points_Table (points INTEGER NOT NULL, time STRING NOT NULL, user_id STRING, PRIMARY KEY(user_id, points, time), FOREIGN KEY(user_id) REFERENCES Account_Table(username) ON DELETE CASCADE);";
                command.ExecuteNonQuery();
            }

            connection.Close();
        }

    }
    public void End()
    {

        QuestGiver.timerStarted = false;

        var loginDB = LoginDB.Instance;
        string userName = loginDB.userInput.text;
        //allows for a link between the inputted username from the login screen
        if (QuestGiver.gameStarted == true) {
            using (var connection = new SqliteConnection(DrivingTheoryGameDB))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO Points_Table (points, time, user_id) VALUES('" + PlayerQuests.points + "', '" + Timer.minutes.ToString() + ":" + Timer.seconds.ToString() + "', '" + userName + "');";
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
        findScore();
        findTimer();

        GameUI.SetActive(false);
        Time.timeScale = 0f;
        GameOverUI.SetActive(true);

        highScore.text = "HighScore: " + DBhighscore;
        currentScore.text = "Your Score: " + PlayerQuests.points.ToString();

        bestTime.text = "Best Time: " + DBbestTime;
        currentTime.text = "Your Time: " + timer.GetComponent<TMPro.TMP_Text>().text;
        //finishes game and displays points and time
    }

    public void findScore()
    {
        using (var connection = new SqliteConnection(DrivingTheoryGameDB))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT points FROM Points_Table ORDER BY points DESC;";
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    for (int i = 0; i < 1; i++)
                        DBhighscore = reader[0].ToString();
                        Debug.Log("HighScore: " + reader[0]);

                    reader.Close();
                }
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
    public void findTimer()
    {
        using (var connection = new SqliteConnection(DrivingTheoryGameDB))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT time FROM Points_Table ORDER BY time DESC;";
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    for (int i = 0; i < 1; i++)
                        DBbestTime = reader[0].ToString();
                        Debug.Log("Best Time: " + reader[0]);

                    reader.Close();
                }
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
}
    