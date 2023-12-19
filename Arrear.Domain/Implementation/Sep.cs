using Arrear.Domain.AbstractCore;
using Arrear.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Arrear.Domain.Implementation
{
    public class Sep
    {
        public Guid Id { get; set; }
        [JsonIgnore]
        public Customer Customer { get; set; }
        public Guid? ManagerID { get; set; }
        public Guid CustomerID { get; set; }
        [JsonIgnore]
        public Manager? Manager { get; set; }
        public Sex Sex { get; set; }
        public int Size { get; set; }
        public string DeliveryAdress { get; set; }

        public SepStatus Status { get; set; }

        public Sep(Customer customer, Sex sex, int size, string deliveryAdress)
        {
            Customer = customer;
            Id = Guid.NewGuid();
            Sex = sex;
            Size = size;
            Status = SepStatus.OrderAssembly;
            DeliveryAdress = deliveryAdress;
        }

        public Sep()
        {
        }

        public void ClearManager()
        {
            Manager?.seps.Remove(this);
            Manager = null;
        }

        public void SetManager(Manager manager)
        {
            Manager = manager;
            manager.seps.Add(this);
        }
    }
}
