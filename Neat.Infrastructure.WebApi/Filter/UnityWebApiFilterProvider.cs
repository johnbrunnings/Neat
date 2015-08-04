using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Microsoft.Practices.Unity;

namespace Neat.Infrastructure.WebApi.Filter
{
    public class UnityWebApiFilterProvider : ActionDescriptorFilterProvider, IFilterProvider
    {
        private readonly IUnityContainer _container;

        public UnityWebApiFilterProvider(IUnityContainer container)
        {
            _container = container;
        }

        public new IEnumerable<FilterInfo> GetFilters(HttpConfiguration configuration, HttpActionDescriptor actionDescriptor)
        {
            var filters = base.GetFilters(configuration, actionDescriptor);

            var enumerable = filters as IList<FilterInfo> ?? filters.ToList();

            foreach (var filter in enumerable)
            {
                _container.BuildUp(filter.Instance.GetType(), filter.Instance);
            }

            return enumerable;
        }
    }
}