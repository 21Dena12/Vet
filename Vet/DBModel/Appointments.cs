//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Vet.DBModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Appointments
    {
        public int AppointmentID { get; set; }
        public int AnimalID { get; set; }
        public Nullable<int> VeterinarianID { get; set; }
        public System.DateTime AppointmentDate { get; set; }
        public string Status { get; set; }
        public string Diagnosis { get; set; }
        public string Recommendations { get; set; }
    
        public virtual Animals Animals { get; set; }
        public virtual Users Users { get; set; }
    }
}
