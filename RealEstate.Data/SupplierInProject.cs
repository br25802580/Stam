//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RealEstate.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class SupplierInProject
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SupplierInProject()
        {
            this.Debts = new HashSet<Debt>();
            this.Payments = new HashSet<Payment>();
        }
    
        public int Id { get; set; }
        public Nullable<int> ProjectId { get; set; }
        public Nullable<int> SupplierId { get; set; }
        public Nullable<int> FlatId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    
        public virtual Flat Flat { get; set; }
        public virtual Supplier Supplier { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Debt> Debts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual Project Project { get; set; }
    }
}
