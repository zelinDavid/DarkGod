public class BornState : IState
{
    public void Enter(EntityBase entity, params object[] args)
    {
         
    }

    public void Exit(EntityBase entity, params object[] args)
    {
        
    }

    public void Process(EntityBase entity, params object[] args)
    {
     
        entity.SetAction(Constant.ActionBorn);
        TimeSvc.Instance.AddTimeTask((deltaTime) =>{
            entity.SetAction(Constant.ActionDefault);
        },1.12 * 1000);
    }
}