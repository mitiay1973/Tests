//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfApp2
{
    using System;
    using System.Collections.Generic;
    
    public partial class Basket
    {
        public int ID { get; set; }
        public Nullable<int> id_Book { get; set; }
        public Nullable<int> koll { get; set; }
    
        public virtual Books Books { get; set; }
    }
}
