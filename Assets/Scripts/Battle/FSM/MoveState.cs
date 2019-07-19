public class MoveState : IState
{
    public void Enter(EntityBase entity, params object[] args)
    {
       
    }

    public void Exit(EntityBase entity, params object[] args)
    {
        
    }

    public void Process(EntityBase entity, params object[] args)
    {
         entity.SetBlend(Constant.BlendMove);
    }
    
}