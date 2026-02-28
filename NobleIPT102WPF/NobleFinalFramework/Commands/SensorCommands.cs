using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NobleFinalFramework.Commands
{
    public interface ISensorCommandService
    {
        Task AddSensorAsync(string name, string type, string location);
        Task UpdateSensorAsync(int id, string name, string type, string location);
        Task DeleteSensorAsync(int id);
    }
}
