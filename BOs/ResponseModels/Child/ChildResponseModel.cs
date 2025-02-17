using BOs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOs.ResponseModels.Child
{
    public class ChildResponseModel
    {
        public int Id { get; set; }

        public string FullName { get; set; } = null!;

        public DateOnly Dob { get; set; }

        public string Gender { get; set; }

        public string? Note { get; set; }


    }
}
