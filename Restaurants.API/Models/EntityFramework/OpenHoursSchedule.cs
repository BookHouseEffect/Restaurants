using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurants.API.Models.EntityFramework
{
    [Serializable]
    public class OpenHoursSchedule : BaseEntity
    {
        [Required]
        public DayOfWeek StartDay { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan StartTime { get; set; }

        [Required]
        public DayOfWeek EndDay { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan EndTime { get; set; }

        [Required]
        public long RestaurantId { get; set; }

        [NonSerialized]
        private RestaurantObjects _Restaurant;

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
