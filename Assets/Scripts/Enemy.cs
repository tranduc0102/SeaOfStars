using CoreActor;

namespace CoreActor
{
    public class Enemy : Actor
    {
        public MainCharacter TargetCharacter { get; private set; }

        public void AcquireTarget()
        {
            TargetCharacter = ControllerGamePlay.Instance.CurrentCharacter;

           
        }
    }
}