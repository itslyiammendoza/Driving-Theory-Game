using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;

public class LoginDB : MonoBehaviour
{

    ToMainMenu toMainMenu;
    public GameObject changeScene;
    private string DrivingTheoryGameDB = "URI=file:DrivingTheoryGame.db"; //stated here so methods can access it

    public TMPro.TMP_Text messageText;
    public TMPro.TMP_InputField userInput;
    public TMPro.TMP_InputField passwordInput;

    public static LoginDB Instance; //allows me to interact with values here on another scipt and scene

    private void Awake()
    {
        toMainMenu = GameObject.Find("Login Screen").GetComponent<ToMainMenu>(); //assigns component to code as it is an empty field without this

        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        //method to create table if it doesn't exist
        CreateDB();

        DisplayUsers();

    }

    

    public void CreateDB()
    {
        // creates the db connection
        using(var connection = new SqliteConnection(DrivingTheoryGameDB))
        {
            connection.Open();

            // create command object allowing the code to control db
            using (var command = connection.CreateCommand())
            {
                // creates Account_Table and 2 fields: username and password
                command.CommandText = "CREATE TABLE IF NOT EXISTS Account_Table (username VARCHAR(15) NOT NULL PRIMARY KEY, password TEXT NOT NULL);";
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    public void RegisterUser()
    {
        bool created = false;
        bool hasNum = false;
        bool hasUpper = false;
        bool hasLower = false;

        using (var connection = new SqliteConnection(DrivingTheoryGameDB))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                var newUsername = userInput.text;
                var newPassword = passwordInput.text;
                //the text inputs to input into the database
                command.CommandText = "SELECT * FROM Account_Table;";

                using (SqliteDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read() && created == false)
                        if (reader["username"].ToString() == newUsername)
                        {
                            created = true;
                            //checks if username inputted is in the database
                        }

                    reader.Close();

                    for (int i = 0; i < newPassword.Length; i++)
                    {
                        if (char.IsDigit(newPassword[i]))
                        {
                            hasNum = true;
                        }
                        if (char.IsUpper(newPassword[i]))
                        {
                            hasUpper = true;
                        }
                        if (char.IsLower(newPassword[i]))
                        {
                            hasLower = true;
                        }
                    }

                    if (created == true)
                    {
                        messageText.text = "Username is taken.";
                        Debug.Log("Username not available.");
                        //if already created an error message is displayed
                    }
                    else if (newUsername == "" || newPassword == "")
                    {
                        messageText.text = "Cannot leave boxes empty.";
                        Debug.Log("Missing info");
                        //error message if left empty
                    }
                    else if (newPassword.Length < 5 ) 
                    {
                        messageText.text = "Passwords must be 5 characters or more. Inlude a lowercase, uppercase and number.";
                        Debug.Log("Not long enough");
                        //error message if below char count
                    }
                    else if (hasNum == false)
                    {
                        messageText.text = "Passwords must be 5 characters or more. Inlude a lowercase, uppercase and number.";
                        Debug.Log("Needs number");
                        //error message if no number
                    }
                    else if (hasUpper == false)
                    {
                        messageText.text = "Passwords must be 5 characters or more. Inlude a lowercase, uppercase and number.";
                        Debug.Log("Needs upper case");
                        //error message if no number
                    }
                    else if (hasLower == false)
                    {
                        messageText.text = "Passwords must be 5 characters or more. Inlude a lowercase, uppercase and number.";
                        Debug.Log("Needs lower case");
                        //error message if no number
                    }
                    else
                    {
                        CreatingUser();
                    }

                }
            }
            connection.Close();
        }
    }

    public void CreatingUser()
    {
        using (var connection = new SqliteConnection(DrivingTheoryGameDB))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                var newUsername = userInput.text;
                var newPassword = passwordInput.text;
                //the text inputs to input into the database
                command.CommandText = "INSERT INTO Account_Table (username, password) VALUES('" + newUsername + "', '" + newPassword + "');";
                //inserts the inputted username and password into a database
                command.ExecuteNonQuery();
            }
            connection.Close();
        }

        toMainMenu.toMainMenu();
        Debug.Log("Registered and Logged in.");
        //uses different script and plays the function
    }

    public void LoginUser()
    {
        bool found = false;

        using (var connection = new SqliteConnection(DrivingTheoryGameDB))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                var newUsername = userInput.text;
                var newPassword = passwordInput.text;
                //the text inputs to input into the database
                command.CommandText = "SELECT * FROM Account_Table;";

                using (SqliteDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read() && found == false)
                        if ((reader["username"].ToString() == newUsername) && (reader["password"].ToString() == newPassword))
                        {
                            toMainMenu.toMainMenu();
                            found = true;
                            //looks through database for the same username and password which was entered and stops once found
                            Debug.Log("Logged in.");
                        }
                        else 
                        {
                            messageText.text = "Either username or password is not correct.";
                            Debug.Log("User not found.");
                            //error message displayed if not found
                        }

                    reader.Close();

                }  
            }
            connection.Close();
        }
    }

    public void DisplayUsers()
    {
        using (var connection = new SqliteConnection(DrivingTheoryGameDB))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Account_Table;";

                using(SqliteDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read())
                        Debug.Log("Username: " + reader["username"] + " Password: " + reader["password"]);

                    reader.Close();
                }
            }
            connection.Close();
        }
    }


}
