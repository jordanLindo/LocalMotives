using DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface IStationManager
    {
        List<Station> GetAllStationsByActive(bool active = true);

        Station GetStationByName(string name);

        Station GetStationByID(int stationID);

        int AddStation(Station station);

        bool EditStation(Station station, Station updateStation);

        bool ActivateStation(int stationID);

        bool DeactivateStation(int stationID);

        bool Validate(string input);

        List<string> GetStationTypes();
    }
}
