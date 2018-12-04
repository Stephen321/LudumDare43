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
    public Config Config;
    public Canvas Canvas;
    public Player Player;
    public Text CollectedText;

    public Text Who_Text;
    public Text Chat_Text;
    public Image Head_Image;
    public Text HelpText;

    public Text Yes_Effects_Text;


    public SacrificeSubject current_sacrifice;


    private float help_text_alive_timer;
    private bool display_text;

    private int MaxSacrifices;

    void Start()
    {
        help_text_alive_timer = 3.0f;
        MaxSacrifices = 5;
        Config.Game_Won = false;
        display_text = false;
        HelpText.gameObject.SetActive(false);
    }

    public bool CanSpawnMoreCollectables(int collectables_alive)
    {
        return ((MaxSacrifices - Sacrifice_Subjects.Count) + collectables_alive + 1) <= MaxSacrifices;
    }
    void Update()
    {
        CollectedText.text = string.Format("{0}/{1}", MaxSacrifices- Sacrifice_Subjects.Count, MaxSacrifices);




        if (display_text)
        {
            help_text_alive_timer -= Time.deltaTime;
            if (help_text_alive_timer < 0.0f)
            {
                HelpText.gameObject.SetActive(false);
                display_text = false;
            }
        }
    }

    public void SetupSacrifice()
    {

        HelpText.gameObject.SetActive(false);
        display_text = false;

        Time.timeScale = .0f;
        Canvas.gameObject.SetActive(true);

        int r = 0;//Random.Range(0, Sacrifice_Subjects.Count);
        current_sacrifice = Sacrifice_Subjects[r];
        Sacrifice_Subjects.RemoveAt(r);


        Who_Text.text = current_sacrifice.who;
        Chat_Text.text = current_sacrifice.chat;
        Head_Image.sprite = current_sacrifice.sprite;

        int speed_change = current_sacrifice.SpeedChange;
        int jump_change = current_sacrifice.JumpChange;
        bool gain_double_jump = Player.Coolness + 1 == Player.Coolness_For_Double_Jump;
        Debug.Log("gain_double_jump: " + gain_double_jump);


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

        if (Sacrifice_Subjects.Count == 0)
        {
            Yes_Effects_Text.text = "The ultimate power: Flight!";
        }
        else
        {
            Yes_Effects_Text.text = string.Join("\n", yes_effects.ToArray());
        }


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

        if (Sacrifice_Subjects.Count == 0)
        {
            Debug.Log("Game won!");
            Config.Game_Won = true;

            HelpText.text = "You won! Time to fly...";
            display_text = true;
            help_text_alive_timer = 3.0f;
            HelpText.gameObject.SetActive(true);
        }
        else
        {
            if (MaxSacrifices - Sacrifice_Subjects.Count == Player.Coolness_For_Double_Jump)
                //just got double jump so display help text
            {
                Debug.Log("fade in double jump text");
                HelpText.text = "Press the jump button again while in the air to double jump!";
                display_text = true;
                help_text_alive_timer = 3.0f;
                HelpText.gameObject.SetActive(true);
            }
        }
    }

    public IEnumerator FadeTextToZeroAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}
