namespace Model.Validations
{
    public class DaysValidator : ITimeValidator
    {
        public bool TwoHigherThanOne(Time one, Time two)
        {
            if (one != null && two != null)
            {
                return two.ToDays() > one.ToDays();
            }
            return true;
        }

        public bool TwoHigherOrEqualThanOne(Time one, Time two)
        {
            if (one != null && two != null)
            {
                return two.ToDays() >= one.ToDays();
            }
            return true;
        }
    }
}
