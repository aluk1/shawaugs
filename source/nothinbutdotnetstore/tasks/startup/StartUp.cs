using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Compilation;
using nothinbutdotnetstore.infrastructure.containers;
using nothinbutdotnetstore.web.application;
using nothinbutdotnetstore.web.core;
using nothinbutdotnetstore.web.core.stubs;

namespace nothinbutdotnetstore.tasks.startup
{
    public class StartUp
    {
        static Dictionary<Type, ICreateASingleDependency> dependencies;
        static DependencyResolver the_container;

        public static void run()
        {
//            Start.by<ConfiguringTheContainer>()
//                .follow_with<LoadingConfiguration>()
//                .follow_with<PrimeTheCaches>()
//                .finish_by<TriggerFirstHits>();

        }

        static void configure_the_front_controller_components()
        {
            register<IProcessWebRequests,FrontController>();
            register<IFindCommandsThatCanProcessRequests,CommandRegistry>();
            register<IRenderReports,ReportEngine>();
            register<GetTheCurrentHttpContext>(() => HttpContext.Current);
            register<PageFactory>(BuildManager.CreateInstanceFromVirtualPath);
            register<ICreateRequestsTheFrontControllerCanProcess, StubRequestMapper>();
        }

        static void register<Contract, Implementation>() where Implementation : Contract
        {
           dependencies.Add(typeof(Contract),new AutomaticDependencyFactory(typeof(Implementation),
               the_container,new GreedyConstructorSelectionStrategy())); 
        }

        static void register<Contract>(Contract implementation)
        {
            dependencies.Add(typeof(Contract), new ExplicitDependencyFactory(() => implementation));
        }

        static void configure_core_facilities()
        {
            dependencies = new Dictionary<Type, ICreateASingleDependency>();
            the_container = new DependencyResolver(dependencies);
            ContainerFacadeResolver resolver = () => the_container;
            Container.facade_resolver = resolver;
        }
    }
}