using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityGuide.Shared.Models;
namespace CityGuide.Shared
{

    public enum ResultStatus
    {
        Success,
        NullModel,
        AlreadyExists,
        NotFound,
        Failed
    }

    
}
