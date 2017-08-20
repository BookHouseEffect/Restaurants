using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurants.API.Models.EntityFramework
{
	//TODO: Make name plural
	[Serializable]
    public class OpenHoursSchedule : BaseEntity
    {
        [Required]
        [Column(Order = 1)]
        public DayOfWeek StartDay { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Column(Order = 2)]
        public TimeSpan StartTime { get; set; }

        [Required]
        [Column(Order = 3)]
        public DayOfWeek EndDay { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Column(Order = 4)]
        public TimeSpan EndTime { get; set; }

        [Required]
        [Column(Order = 5)]
        public long RestaurantId { get; set; }

        [NonSerialized]
        private RestaurantObjects _Restaurant;

		[JsonIgnore]
		[ForeignKey("RestaurantId")]
        public virtual RestaurantObjects TheRestaurantOwningTheSchedule
        {
            get
            {
                return _Restaurant;
            }
            set
            {
                _Restaurant = value;
            }
        }

        [NonSerialized]
        private ICollection<OutOfSchedulePeriods> _OutOfSchedule;

		[JsonIgnore]
		public virtual ICollection<OutOfSchedulePeriods> TheScheduleExceptions
        {
            get
            {
                return _OutOfSchedule;
            }
            set
            {
                _OutOfSchedule = value;
            }
        }
    }

}
