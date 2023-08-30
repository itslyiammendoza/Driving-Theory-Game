using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayingRS : MonoBehaviour
{

    public string[] RoadSigns = new string[20];
    public TMPro.TMP_InputField RSNumber;
    public TMPro.TMP_Text RSDescription;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            var newInput = RSNumber.text;
            DisplayRS();
            //checking for what the user has entered and converts it into the a string for the DisplayRS function
        }
    }
    public void DisplayRS()
    {
        bool found = false;
        var newInput = RSNumber.text;
        var descriptions = new Dictionary<string, string>(){
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
        {"20", "This indicates that cycling is prohibited beyond the sign."}
        };//dictionary for the road sign descriptions

        for (int i = 0; i <= RoadSigns.Length; i++)
        {
            if (newInput == i.ToString())
            {
                RSDescription.text = "Road Sign: "+ RoadSigns[i-1] + "\nDescription: " + descriptions[i.ToString()];
                Debug.Log("Desciption outputted.");
                found = true;
                //if number inputted is one of the items in the dictionary it is outputted
            }
        }
        if (found == false)
        {
            RSDescription.text = "MORE ROADSIGNS TO COME!!!";
            Debug.Log("Input not in range.");
            found = true;
        }//if goes through whole dictionary and hasn't been found, error outputed
    }

}
