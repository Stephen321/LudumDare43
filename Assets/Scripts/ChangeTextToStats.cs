using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTextToStats : MonoBehaviour {

    public Text StatsText;

    void Start () {
        StatsText.text = string.Format(StatsText.text, Stats.ConnectionsSacrificed, Stats.Time.ToString("N2"), Stats.DistanceTravelled.ToString("N2"));
    }

}
