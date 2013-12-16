using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyQuestionnaire.Web.Api.DBContext;
using MyQuestionnaire.Web.Api.TypeMappers;
using MyQuestionnaire.Web.Common;
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
            //The logging tools
            
            container.Bind<IExceptionMessageFormatter>().To<ExceptionMessageFormatter>();
            container.Bind<IActionLogHelper>().To<ActionLogHelper>();
            container.Bind<IActionExceptionHandler>().To<ActionExceptionHandler>();

            //Db Context
            container.Bind<IDbContext>().To<MyQuestionnaireDbContext>();

            //Mappings
            container.Bind<IOpenEndedQuestionMap>().To<OpenEndedQuestionMap>();

        }

        private static void ConfigureLog4Net(IBindingRoot container)
        {
            log4net.Config.XmlConfigurator.Configure();
            var loggerForWebSite = LogManager.GetLogger("MyQuestionnaireWebApi");
            container.Bind<ILog>().ToConstant(loggerForWebSite);
        }
    }
}
