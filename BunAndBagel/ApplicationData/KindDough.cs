//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BunAndBagel.ApplicationData
{
    using System;
    using System.Collections.Generic;
    
    public partial class KindDough
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KindDough()
        {
            this.ProductBunAndBagel = new HashSet<ProductBunAndBagel>();
        }
    
        public int Id { get; set; }
        public string KindDough1 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductBunAndBagel> ProductBunAndBagel { get; set; }
    }
}
