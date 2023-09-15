using Alteration.Application.Infrastructure.Store;
using Alteration.Application.Infrastructure.Validations;
using System.ComponentModel.DataAnnotations;

namespace Alteration.Application
{
    public class ApplicationOptions
    {
        [Required, ValidateObject]
        public SqlServerOptions? SqlServerOptions { get; set; }
    }
}
