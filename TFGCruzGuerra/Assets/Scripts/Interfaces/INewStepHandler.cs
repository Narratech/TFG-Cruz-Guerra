
namespace tfg.Interfaces
{
    public interface INewStepHandler
    {
        public void OnNewStep(Logic.Step step, Logic.Source source,int remainingSteps);
    }

}
