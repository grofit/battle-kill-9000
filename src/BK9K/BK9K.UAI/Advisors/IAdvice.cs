namespace BK9K.UAI.Advisors
{
    public interface IAdvice
    {
        int AdviceId { get; }
        float UtilityValue { get; }
        object RelatedContext { get; }
    }
}