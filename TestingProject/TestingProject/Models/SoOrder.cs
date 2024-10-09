using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestingProject.Models
{
    [Table("SO_ORDER")] 
    public class SoOrder
    {
        [Key]
        [Column("SO_ORDER_ID")]
        public Int64 SoOrderId { get; set; }

        [Column("ORDER_NO")]
        public string OrderNo { get; set; }

        [Column("ORDER_DATE")]
        public DateTime OrderDate { get; set; }

        [Column("COM_CUSTOMER_ID")]
        public int ComCustomerId { get; set; } = 1;

        [Column("ADDRESS")]
        public string Address { get; set; }

        public virtual ComCustomer ComCustomer { get; set; }
        public virtual List<SoItem> SoItems { get; set; } = new List<SoItem>();
    }

    [Table("SO_ITEM")]
    public class SoItem
    {
        [Column("SO_ITEM_ID")]
        public Int64 SoItemId { get; set; }

        [Column("SO_ORDER_ID")]
        public Int64 SoOrderId { get; set; } 

        [ForeignKey("SoOrderId")] 
        public virtual SoOrder SoOrder { get; set; } 

        [Column("ITEM_NAME")]
        public string ItemName { get; set; }

        [Column("QUANTITY")]
        public int Quantity { get; set; }

        [Column("PRICE")]
        public double Price { get; set; }
    }
}
