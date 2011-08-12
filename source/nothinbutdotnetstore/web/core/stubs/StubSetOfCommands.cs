﻿using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using nothinbutdotnetstore.web.application;
using nothinbutdotnetstore.web.application.catalogbrowsing;
using nothinbutdotnetstore.web.application.stubs;

namespace nothinbutdotnetstore.web.core.stubs
{
    public class StubSetOfCommands : IEnumerable<IProcessOneSpecificRequest>
    {
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<IProcessOneSpecificRequest> GetEnumerator()
        {
            yield return
                new RequestCommand(x => true, new TimingProxy(new ViewReport<IEnumerable<Product>>(Stub.of<StubReportEngine>(),
                                                                                      new ProductsInADepartment()), Stub.of<StubLogger>()));

        }

        public class MainDepartments : IQuery<IEnumerable<Department>>
        {
            public IEnumerable<Department> run(IContainRequestInformation request)
            {
                return Stub.of<StubDepartmentRepository>().get_the_main_departments_in_the_store();
            }
        }
    }

    public class ProductsInADepartment : IQuery<IEnumerable<Product>>
    {
        public IEnumerable<Product> run(IContainRequestInformation request)
        {
            return Stub.of<StubProductRepository>().get_the_products_of(request.map<Department>());
        }
    }
}