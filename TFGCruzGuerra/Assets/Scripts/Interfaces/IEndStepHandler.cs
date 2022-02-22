
namespace tfg.Interfaces
{

    public interface IEndStepHandler
    {
        public void OnEndStep(Logic.Step step, Logic.Source source,int remainingSteps);
    }
}
