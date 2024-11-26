using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SkillNode
{
    private string name;            
    private bool isUnlocked;     
    private int price = 250;
    private List<SkillNode> childs = new List<SkillNode>();     
    private Button button;
    private RawImage borderButton;

    public int Price { get => price; }


    public SkillNode(string name, bool isUnlocked, Button button, RawImage borderButton)
    {
        this.name = name;
        this.isUnlocked = isUnlocked;
        this.button = button;
        this.borderButton = borderButton;

        button.interactable = isUnlocked;
        button.image.color = Color.black;
    }


    public void AddChild(SkillNode child)
    {
        childs.Add(child);
    }

    public void CheckIfCanUnlockSkill()
    {
        if (!isUnlocked)
        {
            return;
        }

        // iria aca el metodo;

        button.interactable = false;
        isUnlocked = false;
        button.image.color = Color.white;
        borderButton.color = Color.green;


        // se borraria esto
        foreach (var child in childs)
        {
            child.isUnlocked = true;
            child.button.interactable = true;
        }
    }


    // metodo optativo por si quisieramos desbloquear todos los hijos, deberiamos cambiar la gerarquia y que todos sean hijos de player
    private void UnlockSkillRecursively()
    {
        foreach (var child in childs)
        {
            child.isUnlocked = true;
            child.button.interactable = true;
            child.UnlockSkillRecursively();
        }
    }
}
