using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface ISlugService
    {
        string GenerateSlug(string input);
        Task<string> GenerateUniqueSlug(string input, Func<string, Task<bool>> slugExistsCheck);

    }
}
