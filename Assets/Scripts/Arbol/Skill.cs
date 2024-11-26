using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Skill
{
    private string name;            
    private bool isUnlocked;     
    private List<Skill> childs = new List<Skill>();     
    private Button button;
    private RawImage borderButton;


    public Skill(string name, bool isUnlocked, Button button, RawImage borderButton)
    {
        this.name = name;
        this.isUnlocked = isUnlocked;
        this.button = button;
        this.borderButton = borderButton;

        button.interactable = isUnlocked;
        button.image.color = Color.black;
    }


    public void AddChild(Skill child)
    {
        childs.Add(child);
    }

    public void CheckIfCanUnlockSkill()
    {
        if (!isUnlocked)
        {
            return;
        }

        button.interactable = false;
        isUnlocked = false;
        button.image.color = Color.white;
        borderButton.color = Color.green;

        foreach (var child in childs)
        {
            child.isUnlocked = true;
            child.button.interactable = true;
        }
    }
}
