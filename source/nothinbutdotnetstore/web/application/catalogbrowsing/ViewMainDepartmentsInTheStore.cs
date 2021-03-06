using nothinbutdotnetstore.web.core;

namespace nothinbutdotnetstore.web.application.catalogbrowsing
{
    public class ViewMainDepartmentsInTheStore : IProcessAnApplicationBehaviour
    {
        IReturnDepartments department_repository;
        IRenderReports view_engine;

        public ViewMainDepartmentsInTheStore(IReturnDepartments department_repository, IRenderReports view_engine)
        {
            this.department_repository = department_repository;
            this.view_engine = view_engine;
        }

        public void process(IContainRequestInformation request)
        {
            view_engine.render(department_repository.get_the_main_departments_in_the_store());
        }
    }
}