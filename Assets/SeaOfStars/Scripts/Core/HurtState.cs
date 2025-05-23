namespace CoreActor
{
    public class HurtState : IState
    {
        private Actor _owner;

        public HurtState(Actor owner)
        {
            this._owner = owner;
        }
        public void Enter()
        {
            _owner.Animator.SetTrigger("Hit");
        }

        public void Execute()
        {
        }

        public void Exit()
        {
        }
    }
}