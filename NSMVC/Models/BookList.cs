using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NSMVC.Models
{
    public class BookList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookListId { get; set; }
        [ForeignKey("Customers")]
        [DisplayName("Customer")]
        public int FK_CustomerId { get; set; }
        public virtual Customer? Customers { get; set; }//nav
        [ForeignKey("Books")]
        [DisplayName("Book")]
        public int FK_BookId { get; set; }
        public virtual Book? Books { get; set; }
        [DisplayName("Due date")]
        public DateTime? ReturnAt { get; set; }
        [DisplayName("Returned?")]
        public Boolean IsReturned { get; set; }
        [DisplayName("Return date")]
        public DateTime? ReturnDate { get; set; }
        
    }
}
