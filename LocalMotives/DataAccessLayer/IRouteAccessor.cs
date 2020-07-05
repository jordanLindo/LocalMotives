using DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IRouteAccessor
    {
        List<Route> SelectAllRoutesByActive(bool active = true);

        List<RouteLineVM> SelectRouteLineByRouteID(int routeID);

        int UpdateRoute(Route oldRoute, Route newRoute);

        int InsertRoute(Route route);

        int UpdateRouteLine(RouteLine oldRouteLine, RouteLine newRouteLine);

        int InsertRouteLine(RouteLine routeLine);
        
    }
}
