using L046_Labb3_Code_along.Command;

namespace L046_Labb3_Code_along.ViewModels;

internal class ConfigurationViewModel : ViewModelBase
{
    private readonly MainWindowViewModel? _mainWindowViewModel;

    public RelayCommand ChangePackNameCommand { get; }

    private string _demoText = string.Empty;
    public string DemoText {
        get => _demoText;
        set => SetProperty(ref _demoText, value); 
    }

    public QuestionPackViewModel? ActivePack { get => _mainWindowViewModel?.ActivePack; }

    public void RaiseActivePackChanged() => RaisePropertyChanged(nameof(ActivePack));

    public ConfigurationViewModel(MainWindowViewModel? mainWindowViewModel)
    {
        _mainWindowViewModel = mainWindowViewModel;

        ChangePackNameCommand = new RelayCommand(ChangePackName);
    }

    private void ChangePackName(object? obj)
    {
        ActivePack?.Name = DemoText;
    }
}
