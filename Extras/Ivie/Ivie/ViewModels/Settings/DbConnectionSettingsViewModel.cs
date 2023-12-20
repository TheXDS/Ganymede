using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheXDS.Ganymede.Services.Configuration;
using TheXDS.Ganymede.ViewModels;
using TheXDS.Ivie.Properties;

namespace TheXDS.Ivie.ViewModels.Settings;

public class DbConnectionSettingsViewModel : AwaitableDialogViewModel
{
    public DbConnectionSettingsViewModel() : this(null!)
    {
    }

    public DbConnectionSettingsViewModel(TheXDS.ServicePool.ServicePool pool)
    {
        Configurators = pool.DiscoverAll<ITritonServiceConfigurator>(false);
        Configuration = pool.Resolve<Configuration>()!;
    }

    public IEnumerable<ITritonServiceConfigurator> Configurators { get; }
    public Configuration Configuration { get; }
}
