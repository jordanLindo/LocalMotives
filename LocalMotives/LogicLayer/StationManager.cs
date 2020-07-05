using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObject;

namespace LogicLayer
{
    public class StationManager : IStationManager
    {
        private IStationAccessor _stationAccessor;

        public StationManager()
        {
            _stationAccessor = new StationAccessor();
        }

        public bool ActivateStation(int stationID)
        {
            bool result = false;
            try
            {
                result = (1 == _stationAccessor.SetStationActive(stationID, true));
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Activation failed ",ex);
            }
            return result;
        }

        public int AddStation(Station station)
        {
            int stationID = 0;
            try
            {
                stationID = _stationAccessor.InsertStation(station);
            }
            catch (Exception ex)
            {

                throw ex;
            }


            return stationID;
        }

        public bool DeactivateStation(int stationID)
        {
            bool result = false;
            try
            {
                result = (1 == _stationAccessor.SetStationActive(stationID, false));
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Activation failed ", ex);
            }


            return result;
        }

        public bool EditStation(Station oldStation, Station updateStation)
        {
            bool result = false;

            try
            {
                result = (1 == _stationAccessor.UpdateStation(oldStation, updateStation));
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Update failed ",ex);
            }


            return result;
        }

        public List<Station> GetAllStationsByActive(bool active = true)
        {
            try
            {
                return _stationAccessor.SelectAllStationsByActive(active);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data exception ",ex);
            }
        }

        public Station GetStationByID(int stationID)
        {
            try
            {
                return _stationAccessor.SelectStationByID(stationID);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Station GetStationByName(string name)
        {
            Station result = null;

            try
            {
                result = _stationAccessor.SelectStationByName(name);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data exception ",ex);
            }
            return result;
        }

        public List<string> GetStationTypes()
        {
            try
            {
                return _stationAccessor.SelectDistinctStationType();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to retieve list.",ex);
            }
        }

        public bool Validate(string input)
        {
            bool valid = false;

            if (!input.Equals(null) && !input.Equals("")
                && !input.Contains("--") && !input.Contains("/*")
                    && !input.Contains("*/") && !input.Contains("xp_")
                    && !input.Contains(";") && !input.Contains("'")
                    && (input.IndexOf("drop", StringComparison.OrdinalIgnoreCase) == -1))
            {
                valid = true;
            }

            return valid;
        }


    }
}
