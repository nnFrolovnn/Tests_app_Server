using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tests_server_app.Models.DBModels
{
    public class Icon
    {
        [Key]
        public long IconId { get; set; }
        public byte[] Data { get; set; }

        public virtual ICollection<Achievement> Achievements { get; set; }

        public Icon()
        {
            Achievements = new List<Achievement>();
        }
    }
}
