using SQLite;

namespace App_Fit_MAUI.Model
{
    public class Exercise
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; } = 0; // ID.

        public string Description { get; set; } = String.Empty; // Descrição.

        public DateTime Date { get; set; } = DateTime.Now; // Data.

        public double Weight { get; set; } = 0.00; // Peso.

        public string Notes { get; set; } = String.Empty; // Observações.
    }
}
