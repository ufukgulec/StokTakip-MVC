//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcStok.Models.EntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class urun
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public Nullable<int> Adet { get; set; }
        public Nullable<int> KategoriId { get; set; }
        public Nullable<decimal> Fiyat { get; set; }
        public Nullable<bool> SatistaMi { get; set; }
    
        public virtual kategori kategori { get; set; }
    }
}