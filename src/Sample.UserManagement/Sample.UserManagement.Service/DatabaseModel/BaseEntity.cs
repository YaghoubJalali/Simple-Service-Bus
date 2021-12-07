using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sample.UserManagement.Service.DatabaseModel
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
