using Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class PositionDAO
    {
        private static List<Position> listPositions;
        public static List<Position> GetPositions()
        {
            listPositions = new List<Position>();
            try
            {
                using var db = new FuhrmContext();
                listPositions = db.Positions.ToList();
            }
            catch (Exception e) { }
            return listPositions;
        }
    }
}
