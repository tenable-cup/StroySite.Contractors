namespace StroySite.WPF.Models
{
    public class Contractors
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int TypeId { get; set; }

        // Навигационное свойство
        public virtual ContractorsType ContractorsType { get; set; }
    }
}