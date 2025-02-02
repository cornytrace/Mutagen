using Noggog.WPF;
using ReactiveUI;
using System.Reactive.Disposables;

namespace Mutagen.Bethesda.WPF.Reflection.Fields
{
    public class UnknownSettingsNodeViewBase : NoggogUserControl<UnknownSettingsVM> { }

    /// <summary>
    /// Unknowneraction logic for UnknownSettingsNodeView.xaml
    /// </summary>
    public partial class UnknownSettingsNodeView : UnknownSettingsNodeViewBase
    {
        public UnknownSettingsNodeView()
        {
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                this.WhenAnyValue(x => x.ViewModel!.Meta.DisplayName)
                    .BindTo(this, x => x.SettingNameBlock.Text)
                    .DisposeWith(disposable);
            });
        }
    }
}
