using DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface IRouteManager
    {
        List<Route> GetAllRoutesByActive(bool active = true);

        List<RouteLineVM> GetAllRouteLinesByRouteID(int routeID);

        bool EditRoute(Route oldRoute, Route newRoute);

        bool AddRoute(Route route);

        bool EditRouteLine(RouteLine oldRouteLine, RouteLine newRouteLine);

        bool AddRouteLine(RouteLine routeLine);
    }
}
