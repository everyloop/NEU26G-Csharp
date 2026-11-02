namespace L049_Windows_in_MVVM;

internal class ViewModel : ViewModelBase
{
    public event EventHandler? OpenSettings;

    public RelayCommand OpenSettingsCommand { get; }

    private string _buttonName = "Default button name";

    public string ButtonName
    {
        get => _buttonName;
        set => SetProperty(ref _buttonName, value);
    }


    public ViewModel()
    {
        OpenSettingsCommand = new RelayCommand(_ => RaiseOpenSettings());
    }


    private void RaiseOpenSettings()
    {
        OpenSettings?.Invoke(this, EventArgs.Empty);
    }
}
