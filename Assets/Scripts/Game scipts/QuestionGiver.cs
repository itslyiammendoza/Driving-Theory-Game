using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Mono.Data.Sqlite;

public class QuestionGiver : MonoBehaviour, IInteractable
{
    public static QuestionGiver Instance; //allows me to interact with values here on another scipt and scene

    [SerializeField] private string prompt;

    public static bool Paused = false;
    public GameObject questionUI;

    public bool timerOn = false;
    private float secondsLeft = 10;
    public TMPro.TMP_Text timerOnScreen;

    public Sprite[] RoadSignImages = new Sprite[20];
    public Image roadSignImage;
    public Button button1;
    public TMPro.TMP_Text button1Text;
    public Button button2;
    public TMPro.TMP_Text button2Text;
    public Button button3;
    public TMPro.TMP_Text button3Text;
    public Button button4;
    public TMPro.TMP_Text button4Text;
    public int questnum;

    public TMPro.TMP_Text pointsText;
    private readonly float timeOnScreen = 2.5f;
    public static float timeWhenDisappear;

    public Health health;

    public string[] questions = new string[4];

    public bool questionInProcess = false;

    public int radNum;

    private int enter1 = 1;
    private int enter2 = 1;
    private int enter3 = 1;
    private int enter4 = 1;

    public string correctQuestionNum;

    private string DrivingTheoryGameDB = "URI=file:DrivingTheoryGame.db;foreign keys=true;"; //stated here so methods can access it

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

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

    public void Start()
    {
        CreateDB();
    }
    public string InteractionPrompt => prompt;
    //returns prompt when interaactionPromt is received
    public bool Interact(Interactor interactor)
    {
        if (PlayerQuests.takenQuests.Count != 0)
        {
            if (questionInProcess == false)
            {
                questionUI.SetActive(true);
                Paused = true;
                timerOn = true;
                secondsLeft = 10;
                Debug.Log("question given.");
                //turns on screen once timer starts

                questnum = PlayerQuests.takenQuests.Dequeue();
                correctQuestionNum = questnum.ToString();
                roadSignImage.sprite = RoadSignImages[questnum - 1];
                //does image for roadSigms
                questions[0] = descriptions[(questnum).ToString()];
                int x = 1;
                while (questions[3] == "")
                {
                    radNum = Random.Range(1, 21);
                    for (int i = 0; i < questions.Length; i++)
                    {
                        if (questions[i] == descriptions[radNum.ToString()])
                        {
                            radNum = Random.Range(1, 21);
                        }
                    }
                    questions[x] = descriptions[radNum.ToString()];
                    x += 1;
                }//fills array with right answer and random descriptions

                enter1 = Random.Range(1, 4);
                enter2 = Random.Range(1, 4);
                enter3 = Random.Range(1, 4);
                enter4 = Random.Range(1, 4);
                while (enter1 == enter2)
                {
                    enter2 = Random.Range(1, 4);
                }
                while (enter3 == enter2 || enter3 == enter1)
                {
                    enter3 = Random.Range(1, 4);
                }
                while (enter4 == enter1 || enter4 == enter2 || enter4 == enter3)
                {
                    enter4 += 1;
                }
                Debug.Log(enter1);
                Debug.Log(enter2);
                Debug.Log(enter3);
                Debug.Log(enter4);
                button1Text.text = questions[enter1 - 1];
                button2Text.text = questions[enter2 - 1];
                button3Text.text = questions[enter3 - 1];
                button4Text.text = questions[enter4 - 1];
                //does text for roadsigns

                //displays the roadsign depending on the value in the queue

                questionInProcess = true;
            }
        }

        return true;
    }

    private void Update()
    {
        if (timerOn)
        {
            if (secondsLeft > 0)
            {
                secondsLeft -= Time.deltaTime;
                updateTimer(secondsLeft);
                //if timer has started and there is time remaining it decreases the timer
            }
            else
            {
                Debug.Log("Timer done");
                secondsLeft = 0;
                timerOn = false;

                PlayerQuests.points -= 1000;
                pointsText.color = Color.red;
                pointsText.text = "-1000";
                pointsText.enabled = true;
                timeWhenDisappear = Time.time + timeOnScreen;
                health.TakeDamage(10);
                saveQuestion();

                questionUI.SetActive(false);
                Paused = false;
                questionInProcess = false;
                //turns off the question screen

            }
            
        }
        if (pointsText.enabled == true && Time.time >= timeWhenDisappear)
            {
                pointsText.enabled = false;

                //turns off points shown text
            }
        
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
                command.CommandText = "CREATE TABLE IF NOT EXISTS FeedBack_Table (question STRING NOT NULL, user_id STRING, mistake_id INTEGER NOT NULL PRIMARY KEY, FOREIGN KEY(user_id) REFERENCES Account_Table(username) ON DELETE CASCADE);";
                command.ExecuteNonQuery();
            }

            connection.Close();
        }

    }

    public void saveQuestion()
    {
        var loginDB = LoginDB.Instance;
        string userName = loginDB.userInput.text;
        //gets the instance from the login page and stores the inputted usrname into a string

        using (var connection = new SqliteConnection(DrivingTheoryGameDB))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO FeedBack_Table (question, user_id) VALUES('" + correctQuestionNum + "', '" + userName + "');";
                command.ExecuteNonQuery();
                //enters question number along with username into the feedback table
            }

            connection.Close();
        }
    }

    void updateTimer(float currentTime)
    {
        timerOnScreen.text = secondsLeft.ToString("f2");
    }

    public void clicked()
    {
        string answer = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>().text;
        Debug.Log(answer);
        if (questions[0] == answer)
        {
            Debug.Log("points given.");
            float pointsGiven = Mathf.Round(1000 * (secondsLeft / 10)); //gives user points by doing 1000 multiplied by the amount of seconds left divided by 10 and then rounds that number to nearest whole number
            PlayerQuests.points += pointsGiven;
            pointsText.color = Color.green;
            pointsText.text = "+" + pointsGiven.ToString(); //shows the how many points were given to the user by displaying it on screen with appropriate colour
            pointsText.enabled = true;
            timeWhenDisappear = Time.time + timeOnScreen; //sets the time to dissapear after clicked

            //gives player points according to time remaining
        }
        else
        {
            PlayerQuests.points -= 1000;
            pointsText.color = Color.red;
            pointsText.text = "-1000";
            pointsText.enabled = true;
            timeWhenDisappear = Time.time + timeOnScreen;
            health.TakeDamage(10);
            saveQuestion();
        }

        Debug.Log("Timer done");
        secondsLeft = 0;
        timerOn = false;
        //stops the timer

        questionUI.SetActive(false);
        Paused = false;
        questionInProcess = false;
        //turns off the question screen
    }
}
