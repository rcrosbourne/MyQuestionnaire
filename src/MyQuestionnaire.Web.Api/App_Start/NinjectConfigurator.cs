using System.Web.Http;
using MyQuestionnaire.Web.Api.DBContext;
using Ninject;
using log4net;
using Ninject.Syntax;

namespace MyQuestionnaire.Web.Api.App_Start
{
    public class NinjectConfigurator
    {
        internal void Configure(IKernel container)
        {
            // Add all bindings/dependencies
            AddBindings(container);

            // Use the container and our NinjectDependencyResolver as
            // application's resolver
            var resolver = new NinjectDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }

        private static void AddBindings(IBindingRoot container)
        {
            ConfigureLog4Net(container);
            //Add Other bindings here
            //container.Bind<ISomthing>().To<Somthing>();
            container.Bind<IDbContext>().To<MyQuestionnaireDbContext>();
        }

        private static void ConfigureLog4Net(IBindingRoot container)
        {
            log4net.Config.XmlConfigurator.Configure();
            var loggerForWebSite = LogManager.GetLogger("MyQuestionnaireWebApi");
            container.Bind<ILog>().ToConstant(loggerForWebSite);
        }
    }
}
