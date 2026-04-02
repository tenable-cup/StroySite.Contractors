using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace StroySite.WPF.Models
{
    public class ContractorsType
    {
        public int Id { get; set; }
        public string TypeName { get; set; }

        // Навигационное свойство для связи с контрагентами
        public virtual ICollection<Contractors> Contractors { get; set; }
    }
}