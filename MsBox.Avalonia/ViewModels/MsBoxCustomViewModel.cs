using MsBox.Avalonia.Base;
using MsBox.Avalonia.Dto;
using MsBox.Avalonia.Models;
using MsBox.Avalonia.ViewModels.Commands;

namespace MsBox.Avalonia.ViewModels;

public class MsBoxCustomViewModel : AbstractMsBoxViewModel, ISetFullApi<string>
{
    private IFullApi<string>? _fullApi;

    public MsBoxCustomViewModel(MessageBoxCustomParams @params) : base(@params, @params.Icon, @params.ImageIcon)
    {
        ButtonDefinitions = @params.ButtonDefinitions;
        ButtonClickCommand = new RelayCommand(o => ButtonClick(o?.ToString()));
    }

    public void SetFullApi(IFullApi<string> fullApi)
    {
        _fullApi = fullApi;
        base.SetCopy(fullApi);
    }

    public IEnumerable<ButtonDefinition> ButtonDefinitions { get; }

    #region Hyperlink properties
    public override RelayCommand? HyperLinkCommand { get; internal set; }
    public override string HyperLinkText { get; internal set; } = string.Empty;
    public override bool IsHyperLinkVisible { get; internal set; }
    #endregion

    #region Input properties
    public override string InputLabel { get; internal set; } = string.Empty;
    public override string InputValue { get; set; } = string.Empty;
    public override bool IsInputMultiline { get; internal set; }
    public override bool IsInputVisible { get; internal set; }
    #endregion

    public RelayCommand ButtonClickCommand { get; }

    public void ButtonClick(string? parameter)
    {
        if (_fullApi == null)
        {
            throw new InvalidOperationException("Full API is not initialized.");
        }

        if (string.IsNullOrWhiteSpace(parameter))
        {
            _fullApi.Close();
            return;
        }

        foreach (var bd in ButtonDefinitions)
        {
            if (!parameter.Equals(bd.Name)) continue;
            _fullApi.SetButtonResult(bd.Name);
            break;
        }

        _fullApi.Close();
    }
}