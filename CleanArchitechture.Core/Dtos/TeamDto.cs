using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitechture.Core.DBEntities;
using CleanArchitechture.Core.Interfaces.Common;

namespace CleanArchitechture.Core.Dtos
{
    public class TeamDto:BaseDto,IMapFrom<Team>
    {
        public string Name { get; set; }
        public int TeamMember { get; set; }
    }

}
