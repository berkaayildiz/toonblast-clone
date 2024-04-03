using UnityEngine;

/// <summary>
/// 
/// ParticleManager is a singleton that manages particle effects in the game.
/// 
/// </summary>
public class ParticleManager : Singleton<ParticleManager>
{
    public ParticleSystem CubeParticleBlue;
    public ParticleSystem CubeParticleRed;
    public ParticleSystem CubeParticleYellow;
    public ParticleSystem CubeParticleGreen;
    public ParticleSystem ComboHintParticle;
    public ParticleSystem BoxParticle;
    public ParticleSystem StoneParticle;
    public ParticleSystem VaseParticle;

    public void PlayParticle(Item item)
    {
        ParticleSystem particleSystemReference;
        switch(item.ItemType)
        {
            case ItemType.GreenCube:
                particleSystemReference = CubeParticleGreen;
                break;
            case ItemType.BlueCube:
                particleSystemReference = CubeParticleBlue;
                break;
            case ItemType.RedCube:
                particleSystemReference = CubeParticleRed;
                break;
            case ItemType.YellowCube:
                particleSystemReference = CubeParticleYellow;
                break;
            case ItemType.Box:
                particleSystemReference = BoxParticle;
                break;
            case ItemType.Stone:
                particleSystemReference = StoneParticle;
                break;
            case ItemType.VaseLayer1:
            case ItemType.VaseLayer2:
                particleSystemReference = VaseParticle;
                break;
            default:
                return;
        }
        Vector3 newton = new Vector3(item.transform.position.x, item.transform.position.y, -10);
        var particle = Instantiate(particleSystemReference, newton, Quaternion.identity, item.Cell.gameGrid.particlesParent);

        particle.Play();
    }

    public ParticleSystem PlayComboParticleOnItem(Item item)
    {
        var particle = Instantiate(ComboHintParticle, item.transform.position, Quaternion.identity, item.transform);
        particle.Play(true);
        return particle;
    }
    public void CurrentItemParticleDestroyer(Item item)
    {
        if (item.Particle != null)
        {
            Destroy(item.Particle.gameObject);
        }
    }

}
