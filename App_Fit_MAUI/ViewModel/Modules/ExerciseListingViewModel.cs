using System.ComponentModel;

using System.Windows.Input;

using System.Collections.ObjectModel;

using App_Fit_MAUI.Model;

using App_Fit_MAUI.Helper.Modules;

namespace App_Fit_MAUI.ViewModel.Modules
{
    public class ExerciseListingViewModel : INotifyPropertyChanged
    {
        // Evento de mapeamento de alterações de valores de campos.

        public event PropertyChangedEventHandler? PropertyChanged = null;

        // Campos.

        private ObservableCollection<Model.Exercise> exercises_list = new ObservableCollection<Model.Exercise>();

        private string search_value = String.Empty;

        // Atributos.

        public string Search_Value
        {
            get => this.search_value;
            set
            {
                this.search_value = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(search_value)));
            }
        }

        public ObservableCollection<Model.Exercise> Exercises_List
        {
            get => this.exercises_list;
            set
            {
                this.exercises_list = value;
            }
        }

        // Métodos.

        public ICommand Search
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        List<Model.Exercise> exercises = await (new Helper.Modules.Exercise()).Search(this.Search_Value);

                        this.Exercises_List.Clear();

                        exercises.ForEach(exercise => this.Exercises_List.Add(exercise));
                    }
                    catch (Exception ex)
                    {
                        await Shell.Current.DisplayAlertAsync("Erro!", ex.Message, "OK");
                    }
                });
            }
        }

        public ICommand LoadListing
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        List<Model.Exercise> exercises = await (new Helper.Modules.Exercise()).Select();

                        this.Exercises_List.Clear();

                        exercises.ForEach(exercise => this.Exercises_List.Add(exercise));
                    }
                    catch (Exception ex)
                    {
                        await Shell.Current.DisplayAlertAsync("Erro!", ex.Message, "OK");
                    }
                });
            }
        }

        public ICommand EditExercise
        {
            get
            {
                return new Command<int>(async (int id) =>
                {
                    try
                    {
                        string shell_url = $"//Form?exercise_id_parameter={id}";

                        await Shell.Current.GoToAsync(shell_url);
                    }
                    catch (Exception ex)
                    {
                        await Shell.Current.DisplayAlertAsync("Erro!", ex.Message, "OK");
                    }
                });
            }
        }

        public ICommand DeleteExercise
        {
            get
            {
                return new Command<int>(async (int id) =>
                {
                    try
                    {
                        Model.Exercise? item = this.Exercises_List.Where(exercise => exercise.Id == id).FirstOrDefault();

                        if (item != null)
                        {
                            if (await Shell.Current.DisplayAlertAsync("Atenção!", $"Realmente deseja excluir o exercício '{item.Description}'?", "Sim", "Cancelar"))
                            {
                                int rows_affected = await (new Helper.Modules.Exercise()).Delete(id);

                                if (rows_affected > 0)
                                {
                                    await Shell.Current.DisplayAlertAsync("Atenção!", "Exercício excluído com sucesso.", "OK");

                                    this.Exercises_List.Remove(item);
                                }
                                else
                                {
                                    throw new Exception("Não foi possível excluir o exercício.");
                                }
                            }
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
