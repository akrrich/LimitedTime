public class PlayerSkills
{
    private PlayerController playerController;


    private float speed = 3.5f;
    private float jumpForce = 1.25f;
    private int damage = 1;
    private int life = 2;

    public PlayerSkills(PlayerController playerController)
    {
        this.playerController = playerController; 
    }


    public void SpeedSkill()
    {
        playerController.Speed += speed;
    }

    public void JumpForceSkill()
    {
        playerController.JumpForce += jumpForce;
    }

    public void damageSkill()
    {
        playerController.Damage += damage;
    }

    public void LifeSkill()
    {
        playerController.Life += life;
    }
}
