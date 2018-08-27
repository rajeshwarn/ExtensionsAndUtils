namespace Model.Validations
{
    public class HoursValidator : ITimeValidator
    {
        public bool TwoHigherThanOne(Time one, Time two)
        {
            if (one != null && two != null)
            {
                return two.ToHours() > one.ToHours();
            }
            return true;
        }

        public bool TwoHigherOrEqualThanOne(Time one, Time two)
        {
            if (one != null && two != null)
            {
                return two.ToHours() >= one.ToHours();
            }
            return true;
        }
    }
}