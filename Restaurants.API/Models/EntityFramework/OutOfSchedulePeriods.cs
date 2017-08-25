using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurants.API.Models.EntityFramework
{
	[Serializable]
    public class OutOfSchedulePeriods : BaseEntity
    {
        [Required]
        [DataType(DataType.DateTime)]
        [Column(Order = 1)]
        public DateTimeOffset OutOfSchedulePeriodStarts { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Column(Order = 2)]
        public DateTimeOffset OutOfSchedulePeriodEnds { get; set; }

        [Required]
        [Column(Order = 3)]
        public string Description { get; set; }

        [Required]
        [Column(Order = 4)]
        public long OpenHoursScheduleId { get; set; }

        [NonSerialized]
        private OpenHoursSchedule _Schedule;

		[JsonIgnore]
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
