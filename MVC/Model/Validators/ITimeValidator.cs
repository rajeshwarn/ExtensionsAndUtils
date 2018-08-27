namespace Model.Validations
{
    public interface ITimeValidator
    {
        bool TwoHigherThanOne(Time one, Time two);
        bool TwoHigherOrEqualThanOne(Time one, Time two);
    }
}