using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


[System.Serializable]
public class SacrificeSubject : System.Object
{
    [TextArea(1, 2)]
    public string who;
    [TextArea(1, 4)]
    public string chat;
    public Sprite sprite;

    public int SpeedChange;
    public int JumpChange;
}

public class Sacrifice : MonoBehaviour {
    public List<SacrificeSubject> Sacrifice_Subjects;
    public Canvas Canvas;
    public Player Player;

    public Text Who_Text;
    public Text Chat_Text;
    public Image Head_Image;

    public Text Yes_Effects_Text;

    private bool game_won;

    public SacrificeSubject current_sacrifice;

    void Start()
    {
        game_won = false;
    }

    public void SetupSacrifice()
    {
        if (game_won)
            return;

        Time.timeScale = .0f;
        Canvas.gameObject.SetActive(true);

        current_sacrifice = Sacrifice_Subjects[Random.Range(0, Sacrifice_Subjects.Count)];

        Who_Text.text = current_sacrifice.who;
        Chat_Text.text = current_sacrifice.chat;
        Head_Image.sprite = current_sacrifice.sprite;

        int speed_change = current_sacrifice.SpeedChange;
        int jump_change = current_sacrifice.JumpChange;
        bool gain_double_jump = (Player.Coolness + 1) <= Player.Coolness_For_Double_Jump;


        List<string> yes_effects = new List<string>();


        if (speed_change > 0)
        {
            yes_effects.Add(string.Format("+{0} Speed", speed_change));
        }
        if (jump_change > 0)
        {
            yes_effects.Add(string.Format("+{0} Jump", jump_change));
        }

        if (gain_double_jump)
        {
            yes_effects.Add("Gain double jump");
        }
        
        Yes_Effects_Text.text = string.Join("\n", yes_effects.ToArray());
    }

    public void ReturnHome()
    {
        Player.UpdateStats();
        Stats.Died = false;
        SceneManager.LoadScene(2);
    }

    public void Continue()
    {
        int speed_change = current_sacrifice.SpeedChange;
        int jump_change = current_sacrifice.JumpChange;

        Player.Speed += speed_change;
        Player.Jump += (float)jump_change / 20;
        Player.Coolness++;

        Time.timeScale = 1.0f;
        Canvas.gameObject.SetActive(false);
    }
}
