using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeManager : MonoBehaviour
{
    private List<SkillNode> skills = new List<SkillNode>();

    private PlayerController playerController; 
    private AudioSource buttonClick;

    [SerializeField] private Button[] buttons;
    [SerializeField] private RawImage[] borderButtons;


    private string[] skillNames = 
    { 
        "Player", "Speed", "Damage", "JumpForce", "Life", 
        "Axe", "ReloadTime", "BulletSpeed", "FireRate", "ExtraBullets" 
    };

    private bool[] isUnlocked = { true, false, false, false, false, true, false, false, false, false };


    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        buttonClick = GetComponent<AudioSource>();

        AddSkillsNodes();
        AddChildsToNodes();
    }


    public void ClickButton(int index)
    {
        //if (PlayerController.Score >= skills[index].Price)
        //{
            buttonClick.Play();
            skills[index].CheckIfCanUnlockSkill();

            switch (index)
            {
                case 1:
                    playerController.PlayerControllerSkills.SpeedSkill();
                    break;
                case 2:
                    playerController.PlayerControllerSkills.damageSkill();
                    break;
                case 3:
                    playerController.PlayerControllerSkills.JumpForceSkill();
                    break;
                case 4:
                    playerController.PlayerControllerSkills.LifeSkill();
                    break;
                case 6:
                    playerController.PlayerControllerSkills.ReloadingTime();
                    break;
                case 7:
                    playerController.PlayerControllerSkills.AxeSpeed();
                    break;
                case 8:
                    playerController.PlayerControllerSkills.FireRate();
                    break;
                case 9:
                    playerController.PlayerControllerSkills.ExtraBullets();
                    break;
            }

        //PlayerController.SubScore(skills[index].Price);
        //}
    }


    private void AddSkillsNodes()
    {
        for (int i = 0; i < skillNames.Length; i++)
        {
            skills.Add(new SkillNode(skillNames[i], isUnlocked[i], buttons[i], borderButtons[i]));
        }
    }

    private void AddChildsToNodes()
    {
        skills[0].AddChild(skills[1]);
        skills[0].AddChild(skills[2]);
        skills[1].AddChild(skills[3]);
        skills[2].AddChild(skills[4]);

        skills[5].AddChild(skills[6]);
        skills[5].AddChild(skills[7]);
        skills[6].AddChild(skills[8]);
        skills[7].AddChild(skills[9]);
    }
}
