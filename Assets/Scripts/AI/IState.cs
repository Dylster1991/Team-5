// Interface that will be used to create a state.
public interface IState
{
    // Functions that all states will have (although they might be empty in some cases).
    void Tick();
    void OnEnter();
    void OnExit();
}
