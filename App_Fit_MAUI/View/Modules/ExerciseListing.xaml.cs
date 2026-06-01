using App_Fit_MAUI.ViewModel.Modules;

namespace App_Fit_MAUI.View.Modules;

public partial class ExerciseListing : ContentPage
{
	public ExerciseListing()
	{
		InitializeComponent();

		this.BindingContext = new ExerciseListingViewModel();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

		try
		{
			ExerciseListingViewModel view_model = (ExerciseListingViewModel)this.BindingContext;

			view_model.LoadListing.Execute(null);
		}
        catch (Exception ex)
        {
            await DisplayAlertAsync("Erro!", ex.Message, "OK");
        }
    }

    private async void btn_new_exercise_Clicked(object sender, EventArgs e)
    {
		try
		{
			await Shell.Current.GoToAsync("//Form");
		}
		catch (Exception ex)
		{
			await DisplayAlertAsync("Erro!", ex.Message, "OK");
		}
    }
}