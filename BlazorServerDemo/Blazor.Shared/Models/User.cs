using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Shared.Models;

public class User
{
    public bool IsAuthenticated { get; set; }
    public string? LoginName { get; set; }
    public string? DisplayName { get; set; }
}
