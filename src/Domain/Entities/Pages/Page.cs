using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Pages
{
    public  class Page : AuditableAndSoftDeletableEntity, ISanitizable
    {
        public string Slug { get;  set; } = default!;
        public string Background { get; set; } = "";
        public string Section1 { get; set; } = "";
        public string Section2 { get; set; } = "";

        public void SetSlug(string value) => Slug = value;

        public void SanitizeForSaving()
        {    Slug  = Slug .Trim();
            Background = Background.Trim(); 
            Section1 = Section1.Trim();
            Section2 = Section2.Trim();
        }
    }
}
