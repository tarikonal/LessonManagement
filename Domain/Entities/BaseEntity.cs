using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class BaseEntity : IEntity
    {
        public BaseEntity()
        {
            
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } // Primary key guid değeri

        public bool? AktifMi { get; set; } = true; // Silinip silinmediği kontrolü
        public DateTime? EklemeTarihi { get; set; } // Kaydın eklenme tarihi.
        public Guid? EkleyenKullaniciId { get; set; } // Kaydı ekleyen kişinin ID bilgisi.
        public DateTime? GuncellemeTarihi { get; set; } // Kaydın son güncellenme tarihi
        public Guid? GuncelleyenKullaniciId { get; set; } // Kaydı son güncelleyen kişinin ID bilgisi.
    }
}