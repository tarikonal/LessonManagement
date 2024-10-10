namespace Domain.DTOs.Family
{
    public class UpdateFamilyDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? GuncelleyenKullaniciId { get; set; }
        public Guid? EkleyenKullaniciId { get; set; }
        public DateTime? EklemeTarihi { get; set; }
    }
}
