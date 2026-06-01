using System.ComponentModel;

using System.Windows.Input;

using App_Fit_MAUI.Model;

using App_Fit_MAUI.Helper.Modules;

namespace App_Fit_MAUI.ViewModel.Modules
{
    [QueryProperty("Exercise_Id_Parameter", "exercise_id_parameter")]
    public class ExerciseFormViewModel : INotifyPropertyChanged
    {
        // Evento de mapeamento de alterações de valores de campos.

        public event PropertyChangedEventHandler? PropertyChanged = null;

        // Campos.

        private int id = 0;

        private string description = String.Empty;

        private DateTime date = DateTime.Now;

        private double weight = 0.00;

        private string notes = String.Empty;

        // Parâmetro de Navegação.

        public string Exercise_Id_Parameter
        {
            set
            {
                if (value != null)
                {
                    int id = int.Parse(Uri.UnescapeDataString(value));

                    this.LoadFormData.Execute(id);
                }
                else
                {
                    this.ClearInputs();
                }
            }
        }

        // Atributos.

        public int Id
        {
            get => this.id;
            set
            {
                this.id = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
            }
        }

        public string Description
        {
            get => this.description;
            set
            {
                this.description = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Description)));
            }
        }

        public DateTime Date
        {
            get => this.date;
            set
            {
                this.date = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Date)));
            }
        }

        public double Weight
        {
            get => this.weight;
            set
            {
                this.weight = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Weight)));
            }
        }

        public string Notes
        {
            get => this.notes;
            set
            {
                this.notes = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Notes)));
            }
        }

        // Métodos.

        public async void ClearInputs()
        {
            try
            {
                this.Id = 0;

                this.Description = String.Empty;

                this.Date = DateTime.Now;

                this.Weight = 0.00;

                this.Notes = String.Empty;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Erro!", ex.Message, "OK");
            }
        }

        public ICommand LoadFormData
        {
            get
            {
                return new Command<int>(async (int id) =>
                {
                    try
                    {
                        Model.Exercise model = await (new Helper.Modules.Exercise()).Find(id);

                        if (model.Id > 0)
                        {
                            this.Id = model.Id;

                            this.Description = model.Description;

                            this.Date = model.Date;

                            this.Weight = model.Weight;

                            this.Notes = model.Notes;
                        }
                        else
                        {
                            throw new Exception("Registro não encontrado.");
                        }
                    }
                    catch (Exception ex)
                    {
                        await Shell.Current.DisplayAlertAsync("Erro!", ex.Message, "OK");
                    }
                });
            }
        }

        public ICommand Save
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        Model.Exercise payload = new Model.Exercise()
                        {
                            Id = this.id,
                            Description = this.description,
                            Date = this.date,
                            Weight = this.weight,
                            Notes = this.notes
                        };

                        int rows_affected = await (new Helper.Modules.Exercise()).Save(payload);

                        if (rows_affected > 0)
                        {
                            this.ClearInputs();

                            await Shell.Current.GoToAsync("//Listing");
                        }
                        else
                        {
                            throw new Exception("Ocorreu um erro ao tentar salvar o registro.");
                        }
                    }
                    catch (Exception ex)
                    {
                        await Shell.Current.DisplayAlertAsync("Erro!", ex.Message, "OK");
                    }
                });
            }
        }
    }
}
