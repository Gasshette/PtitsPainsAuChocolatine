using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetitsPainsAuChocolatine_PasDeBagarre.Entity
{
    public class User
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// User's firstname
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// User's lastname
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// User's last breakfast delivery
        /// </summary>
        public DateTime? LastDelivery { get; set; }

        /// <summary>
        /// User's breakfast delivery date
        /// </summary>
        public DateTime? Delivery { get; set; }

        /// <summary>
        /// Define the delivery date as past (true) or not (false) :  allow us to set style in view
        /// </summary>
        public bool IsDeliveryPast { get; set; }

    }
}
