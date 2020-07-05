using DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IStationAccessor
    {
        List<Station> SelectAllStationsByActive(bool active);

        Station SelectStationByName(string name);

        Station SelectStationByID(int stationID);

        int UpdateStation(Station oldStation, Station updateStation);

        int InsertStation(Station station);

        int SetStationActive(int stationID, bool active);

        List<string> SelectDistinctStationType();
    }
}
