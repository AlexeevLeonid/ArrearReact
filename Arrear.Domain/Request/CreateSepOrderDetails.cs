using Arrear.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arrear.Domain.Request
{
    public class CreateSepOrderDetails
    {
        public string Sex { get; set; }
        public string Size { get; set; }
        public string DeliveryAdress { get; set; }
        public CreateSepOrderDetails(string Sex, string Size, string DeliveryAdress) 
        {
            this.Sex = Sex;
            this.Size = Size;
            this.DeliveryAdress = DeliveryAdress;
        }
    }
}
