using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(FPSCounter))]
public class FPSDisplay : MonoBehaviour {

    public Text fpsLabel;
    public Text highestFpsLabel;
    public Text lowestFpsLabel;
    

    private FPSCounter fpsCounter;

    private void Awake () {
        //fetch boy
        fpsCounter = GetComponent<FPSCounter>();
    }

    
    // //recommended so it doesn't create temp strings every second
    // static string[] stringsFrom00To99 = {
    //     "00", "01", "02", "03", "04", "05", "06", "07", "08", "09",
    //     "10", "11", "12", "13", "14", "15", "16", "17", "18", "19",
    //     "20", "21", "22", "23", "24", "25", "26", "27", "28", "29",
    //     "30", "31", "32", "33", "34", "35", "36", "37", "38", "39",
    //     "40", "41", "42", "43", "44", "45", "46", "47", "48", "49",
    //     "50", "51", "52", "53", "54", "55", "56", "57", "58", "59",
    //     "60", "61", "62", "63", "64", "65", "66", "67", "68", "69",
    //     "70", "71", "72", "73", "74", "75", "76", "77", "78", "79",
    //     "80", "81", "82", "83", "84", "85", "86", "87", "88", "89",
    //     "90", "91", "92", "93", "94", "95", "96", "97", "98", "99"
    // };
    private void Update () {
            fpsLabel.text = fpsCounter.Fps.ToString();
            highestFpsLabel.text = fpsCounter.MaxFps.ToString();    
            lowestFpsLabel.text = fpsCounter.MinFps.ToString();    

    }

    public void toggleAnalytics()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}