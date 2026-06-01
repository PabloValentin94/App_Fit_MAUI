using App_Fit_MAUI.ViewModel.Modules;

namespace App_Fit_MAUI.View.Modules;

public partial class ExerciseForm : ContentPage
{
	public ExerciseForm()
	{
		InitializeComponent();

        this.BindingContext = new ExerciseFormViewModel();
	}
}