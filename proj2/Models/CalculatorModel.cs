namespace proj2.Models
{
    public class CalculatorModel
    {
        public double Number1 { get; set; }
        public double Number2 { get; set; }
        public double? Result { get; set; } // Nullable in case result is not calculated yet
    }
}
