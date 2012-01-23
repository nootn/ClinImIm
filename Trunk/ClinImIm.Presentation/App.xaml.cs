using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Waf;
using System.Waf.Applications;
using System.Windows;
using System.Windows.Threading;
using ClinImIm.Applications.Controllers;
using ClinImIm.Domain;
using log4net;
using log4net.Config;

namespace ClinImIm.Presentation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private CompositionContainer _container;
        private IApplicationController _controller;
        private static readonly ILog _log = LogManager.GetLogger(typeof(App));

        static App()
        {
#if (DEBUG)
            WafConfiguration.Debug = true;
#endif
        }


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

#if (DEBUG != true)
            // Don't handle the exceptions in Debug mode because otherwise the Debugger wouldn't
            // jump into the code when an exception occurs.
            DispatcherUnhandledException += AppDispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += AppDomainUnhandledException;
#endif
            XmlConfigurator.Configure();
            _log.Debug("OnStartup called");

            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(Controller).Assembly));
            catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(IApplicationController).Assembly));
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(ValidationModel).Assembly));

            _container = new CompositionContainer(catalog);
            var batch = new CompositionBatch();
            batch.AddExportedValue(_container);
            _container.Compose(batch);

            _controller = _container.GetExportedValue<IApplicationController>();
            _controller.Initialize();
            _controller.Run();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _controller.Shutdown();
            _container.Dispose();

            base.OnExit(e);
        }

        private void AppDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            _log.ErrorFormat("AppDispatcherUnhandledException: {0}", e);
            HandleException(e.Exception, false);
            e.Handled = true;
        }

        private static void AppDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            _log.ErrorFormat("AppDomainUnhandledException: {0}", e);
            HandleException(e.ExceptionObject as Exception, e.IsTerminating);
        }

        private static void HandleException(Exception e, bool isTerminating)
        {
            if (e == null) { return; }

            _log.ErrorFormat("HandleException: IsTerminating:{0} Exception: {1}", isTerminating, e);

            if (!isTerminating)
            {
                MessageBox.Show(string.Format(CultureInfo.CurrentCulture,
                        Presentation.Properties.Resources.UnknownError, e)
                    , ApplicationInfo.ProductName, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
