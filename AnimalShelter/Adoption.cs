//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AnimalShelter
{
    using System;
    using System.Collections.Generic;
    
    public partial class Adoption
    {
        public int ID_adoption { get; set; }
        public int Animal { get; set; }
        public int New_owner { get; set; }
        public System.DateTime Date_of_adoption { get; set; }
        public string Contract { get; set; }
        public int Adoption_status { get; set; }
        public string Comment { get; set; }
        public string ContractPDF { get; set; }
    
        public virtual Adoption_status Adoption_status1 { get; set; }
        public virtual Animal Animal1 { get; set; }
        public virtual New_owner New_owner1 { get; set; }
    }
}
