using Objects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PositionService : IPositionService
    {
        private readonly IPositionRepository iPositionRepository;
        public PositionService()
        {
            iPositionRepository = new PositionRepository();
        }
        public List<Position> GetPositions()
        {
            return iPositionRepository.GetPositions();
        }
    }
}
