//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GrupuriDeStudiuIP35
{
    using System;
    using System.Collections.Generic;
    
    public partial class GRUPURI
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GRUPURI()
        {
            this.APARTENENTAs = new HashSet<APARTENENTA>();
            this.MATERIALEs = new HashSet<MATERIALE>();
            this.SUBIECTEs = new HashSet<SUBIECTE>();
            this.CERERIs = new HashSet<CERERI>();
        }
    
        public int Id { get; set; }
        public int IdMaterie { get; set; }
        public string Nume { get; set; }
        public string Tip { get; set; }
        public string Descriere { get; set; }
        public Nullable<System.DateTime> Data { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<APARTENENTA> APARTENENTAs { get; set; }
        public virtual MATERII MATERII { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MATERIALE> MATERIALEs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SUBIECTE> SUBIECTEs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CERERI> CERERIs { get; set; }
    }
}
