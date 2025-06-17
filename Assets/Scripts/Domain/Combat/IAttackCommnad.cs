/// <summary>
/// IAttackStrategy es una interfaz que define un comportamiento de ataque.
/// Separa el comportamiento de ataque del objeto que lo implementa, permitiendo diferentes estrategias de ataque sin modificar la clase principal.
/// Separa el como atacar de quien lo hace.
/// </summary>

public interface IAttackCommnad
{
    void ExecuteAttack(IAnimatorAdapter animator);
}