using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using Ninject.Modules;
using ConnectUO.Framework.Services;
using ConnectUO.Services;
using ConnectUO.Data;
using ConnectUO.Framework.Web;
using ConnectUO.Forms;
using ConnectUO.Controls;
using Ninject.Activation;
using ConnectUO.Framework.Debug;
using ConnectUO.Framework;

namespace ConnectUO
{
    public class StarupModule : NinjectModule
    {
        private ShellForm _shellForm;
        private ShellController _shellController;
        private ApplicationService _applicationService;
        private StorageService _storageService;
        private SettingsService _settingsService;
        private SiteService _siteService;

        public override void Load()
        {
            Bind<IApplicationService>().ToConstant(_applicationService = Kernel.Get<ApplicationService>());
            Bind<IStorageService>().ToConstant(_storageService = Kernel.Get<StorageService>());
            Bind<ISettingsService>().ToConstant(_settingsService = Kernel.Get<SettingsService>());
            Bind<ISiteService>().ToConstant(_siteService = Kernel.Get<SiteService>());

            Bind<CuoUri>().ToSelf();
            Bind<ShellForm>().ToSelf().InSingletonScope();
            Bind<ShellController>().ToSelf().InSingletonScope();

            _shellForm = Kernel.Get<ShellForm>();
            _shellController = Kernel.Get<ShellController>();

            Bind<IShell>().ToConstant(_shellForm);
            Bind<IShellController>().ToConstant(_shellController);

            Bind<PatcherForm>().ToSelf();
            Bind<SplashScreen>().ToSelf();
            Bind<AboutControl>().ToSelf();
            Bind<SettingsControl>().ToSelf();
            Bind<NavigationControl>().ToSelf();
            Bind<EditLocalServerControl>().ToSelf();
            Bind<LocalServerListControl>().ToSelf();
            Bind<PublicServerListControl>().ToSelf();
            Bind<FavoritesServerListControl>().ToSelf();
        }
    }
}
