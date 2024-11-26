using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeManager : MonoBehaviour
{
    private List<Skill> skills = new List<Skill>();

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

        AddSkills();
        AddChilds();
    }


    public void PlayerButton()
    {
        buttonClick.Play();
        skills[0].CheckIfCanUnlockSkill();
    }

    public void SpeedButton()
    {
        buttonClick.Play();
        skills[1].CheckIfCanUnlockSkill();

        playerController.PlayerControllerSkills.SpeedSkill();
    }

    public void DamageButton()
    {
        buttonClick.Play();
        skills[2].CheckIfCanUnlockSkill();

        playerController.PlayerControllerSkills.damageSkill();
    }

    public void JumpForceButton()
    {
        buttonClick.Play();
        skills[3].CheckIfCanUnlockSkill();

        playerController.PlayerControllerSkills.JumpForceSkill();
    }

    public void LifeButton()
    {
        buttonClick.Play();
        skills[4].CheckIfCanUnlockSkill();

        playerController.PlayerControllerSkills.LifeSkill();
    }


    private void AddSkills()
    {
        for (int i = 0; i < skillNames.Length; i++)
        {
            skills.Add(new Skill(skillNames[i], isUnlocked[i], buttons[i], borderButtons[i]));
        }
    }

    private void AddChilds()
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

    private void ClickButton(int index)
    {
        buttonClick.Play();
        skills[index].CheckIfCanUnlockSkill();
    }
}
