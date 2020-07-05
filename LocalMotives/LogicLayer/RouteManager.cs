using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObject;

namespace LogicLayer
{
    public class RouteManager : IRouteManager
    {
        private IRouteAccessor _routeAccessor;

        public RouteManager()
        {
            _routeAccessor = new RouteAccessor();
        }

        public bool AddRoute(Route route)
        {
            bool result = false;

            try
            {
                result = (1 == _routeAccessor.InsertRoute(route));
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Route not added ",ex);
            }
            return result;
        }

        public bool AddRouteLine(RouteLine routeLine)
        {
            bool result = false;

            try
            {
                result = (1 == _routeAccessor.InsertRouteLine(routeLine));
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Line not added ",ex);
            }
            return result;
        }

        public bool EditRoute(Route oldRoute, Route newRoute)
        {
            bool result = false;

            try
            {
                result = (1 == _routeAccessor.UpdateRoute(oldRoute, newRoute));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Update failed ",ex);
            }
            return result;
        }

        public bool EditRouteLine(RouteLine oldRouteLine, RouteLine newRouteLine)
        {
            bool result = false;

            try
            {
                result = (1 == _routeAccessor.UpdateRouteLine(oldRouteLine, newRouteLine));
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Update failed ",ex);
            }


            return result;
        }

        public List<RouteLineVM> GetAllRouteLinesByRouteID(int routeID)
        {
            try
            {
                return _routeAccessor.SelectRouteLineByRouteID(routeID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to retrieve list ",ex);
            }
        }

        public List<Route> GetAllRoutesByActive(bool active = true)
        {
            try
            {
                return _routeAccessor.SelectAllRoutesByActive(active);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to retrieve list ",ex);
            }
        }
    }
}
