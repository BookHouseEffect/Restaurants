using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurants.API.Models.EntityFramework
{
    [Serializable]
    public class OutOfSchedulePeriods : BaseEntity
    {
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime OutOfSchedulePeriodStarts { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime OutOfSchedulePeriodEnds { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public long OpenHoursScheduleId { get; set; }

        [NonSerialized]
        private OpenHoursSchedule _Schedule;

        [ForeignKey("OpenHoursScheduleId")]
        public virtual OpenHoursSchedule TheRealSchedule
        {
            get
            {
                return _Schedule;
            }
            set
            {
                _Schedule = value;
            }
        }
    }
}
