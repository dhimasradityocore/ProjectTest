using System.ComponentModel.DataAnnotations.Schema;

namespace TestingProject.Models
{
    [Table("COM_CUSTOMER")]
    public class ComCustomer
    {
       

        [Column("COM_CUSTOMER_ID")]
        public int ComCustomerID { get; set; }

        [Column("CUSTOMER_NAME")]
        public string CustomerName { get; set; }

        public virtual ICollection<SoOrder> SoOrders { get; set; } = new List<SoOrder>();

    }
}
