using System;
using System.ComponentModel.DataAnnotations;

namespace Restaurants.API.Models.Api
{
	public class TimeModel
	{

		protected const Int32 OneDayInMinutes = 1440;

		[Required]
		[Range(minimum: -12, maximum: +14)]
		public Int32 UtcHours { get; set; }

		[Required]
		[Range(minimum: 0, maximum: 45)]
		public Int32 UtcMinutes { get; set; }

		[Required]
		[Range(minimum: 0, maximum: 23)]
		public Int32 StartHours { get; set; }

		[Required]
		[Range(minimum: 0, maximum: 23)]
		public Int32 EndHours { get; set; }

		[Required]
		[Range(minimum: 0, maximum: 59)]
		public Int32 StartMinutes { get; set; }

		[Required]
		[Range(minimum: 0, maximum: 59)]
		public Int32 EndMinutes { get; set; }

		protected Int32 GetUtcTimeInMinutes()
		{
			return this.UtcHours * 60 + (this.UtcHours < 0 ? -1 : 1) * this.UtcMinutes;
		}

		protected Int32 GetStartTimeInMinutes()
		{
			Int32 utcTime = this.GetUtcTimeInMinutes();
			Int32 startTime = this.StartHours * 60 + this.StartMinutes;
			return startTime - utcTime;
		}

		protected Int32 GetEndTimeInMinutes()
		{
			Int32 utcTime = this.GetUtcTimeInMinutes();
			Int32 endTime = this.EndHours * 60 + this.EndMinutes;
			return endTime - utcTime;
		}

		public TimeSpan GetUtcStartTimeSpan()
		{
			Int32 coordinatedTime = this.GetStartTimeInMinutes();

			if (coordinatedTime < 0)
				return TimeSpan.FromMinutes(coordinatedTime + OneDayInMinutes);

			if (coordinatedTime > OneDayInMinutes)
				return TimeSpan.FromMinutes(coordinatedTime - OneDayInMinutes);

			return TimeSpan.FromMinutes(coordinatedTime);
		}

		public TimeSpan GetUtcEndTimeSpan()
		{
			Int32 coordinatedTime = this.GetEndTimeInMinutes();

			if (coordinatedTime < 0)
				return TimeSpan.FromMinutes(coordinatedTime + OneDayInMinutes);

			if (coordinatedTime > OneDayInMinutes)
				return TimeSpan.FromMinutes(coordinatedTime - OneDayInMinutes);

			return TimeSpan.FromMinutes(coordinatedTime);
		}
	}

	public class DayTimeModel : TimeModel
	{

		[Required]
		public DayOfWeek StartDay { get; set; }

		[Required]
		public DayOfWeek EndDay { get; set; }

		private DayOfWeek GetPreviousDay(DayOfWeek day)
		{
			if (day == (DayOfWeek)0)
				return (DayOfWeek)6;

			return (DayOfWeek)(day - 1);
		}

		private DayOfWeek GetNextDay(DayOfWeek day)
		{
			if (day == (DayOfWeek)6)
				return (DayOfWeek)0;

			return (DayOfWeek)(day + 1);
		}

		public DayOfWeek GetUtcStartDay()
		{
			Int32 coordinatedTime = this.GetStartTimeInMinutes();

			if (coordinatedTime < 0)
				return GetPreviousDay(this.StartDay);

			if (coordinatedTime > OneDayInMinutes)
				return GetNextDay(this.StartDay);

			return this.StartDay;
		}

		public DayOfWeek GetUtcEndtDay()
		{
			Int32 coordinatedTime = this.GetEndTimeInMinutes();

			if (coordinatedTime < 0)
				return GetPreviousDay(this.EndDay);

			if (coordinatedTime > OneDayInMinutes)
				return GetNextDay(this.EndDay);

			return this.EndDay;
		}
	}

	public class ExtendedDayTimeModel : DayTimeModel {
		
		[Required]
		public Int64 RestaurantId { get; set; }
	}

	public class DateTimeModel : TimeModel
	{		
		[Required]
		[Range(minimum: 1, maximum: 9999)]
		public Int32 StartYear { get; set; }

		[Required]
		[Range(minimum: 1, maximum: 9999)]
		public Int32 EndYear { get; set; }

		[Required]
		[Range(minimum: 1, maximum: 12)]
		public Int32 StartMonth { get; set; }

		[Required]
		[Range(minimum: 1, maximum: 12)]
		public Int32 EndMonth { get; set; }

		[Required]
		[Range(minimum: 1, maximum: 31)]
		public Int32 StartDay { get; set; }

		[Required]
		[Range(minimum: 1, maximum: 31)]
		public Int32 EndDay { get; set; }

		private bool IsLapYear(Int32 year)
		{
			if (year % 400 == 0
				|| year % 4 == 0 && year % 100 != 0)
				return true;
			return false;
		}

		private Int32 GetDaysInMonth(Int32 month, Int32 year)
		{
			Int32 days = 0;

			switch (month)
			{
				case 1:
				case 3:
				case 5:
				case 7:
				case 8:
				case 10:
				case 12:
					days = 31;
					break;

				case 4:
				case 6:
				case 9:
				case 11:
					days = 30;
					break;

				case 2:
					days = IsLapYear(year) ? 29 : 28;
					break;
			}

			return days;
		}

		public DateTime GetStartDateTime()
		{
			TimeSpan startTime = this.GetUtcStartTimeSpan();

			Int32 coordinatedTime = this.GetStartTimeInMinutes();
			Int32 changeDay = coordinatedTime < 0 ? -1 : coordinatedTime > OneDayInMinutes ? 1 : 0;
			Int32 changeMonth = this.StartDay + changeDay <= 0 ? -1 :
				this.StartDay + changeDay > this.GetDaysInMonth(StartMonth, StartYear) ? 1 : 0;
			Int32 changeYear = this.StartMonth + changeMonth <= 0 ? -1 :
				this.StartMonth + changeMonth > 12 ? 1 : 0;

			Int32 newYear = this.StartYear + changeYear;
			Int32 newMonth = (this.StartMonth - 1 + changeMonth + 12) % 12 + 1;
			Int32 daysInNewMonth = this.GetDaysInMonth(newMonth, newYear);
			Int32 newDay = (this.StartDay - 1 + changeDay + daysInNewMonth) % daysInNewMonth + 1;

			return new DateTime(
				newYear,
				newMonth,
				newDay,
				startTime.Hours,
				startTime.Minutes,
				startTime.Seconds);
		}

		public DateTime GetEndDateTime()
		{
			TimeSpan endTime = this.GetUtcEndTimeSpan();

			Int32 coordinatedTime = this.GetEndTimeInMinutes();
			Int32 changeDay = coordinatedTime < 0 ? -1 : coordinatedTime > OneDayInMinutes ? 1 : 0;
			Int32 changeMonth = this.EndDay + changeDay <= 0 ? -1 :
				this.EndDay + changeDay > this.GetDaysInMonth(EndMonth, EndYear) ? 1 : 0;
			Int32 changeYear = this.EndMonth + changeMonth <= 0 ? -1 :
				this.EndMonth + changeMonth > 12 ? 1 : 0;

			Int32 newYear = this.EndYear + changeYear;
			Int32 newMonth = (this.EndMonth - 1 + changeMonth + 12) % 12 + 1;
			Int32 daysInNewMonth = this.GetDaysInMonth(newMonth, newYear);
			Int32 newDay = (this.EndDay - 1 + changeDay + daysInNewMonth) % daysInNewMonth + 1;

			return new DateTime(
				newYear,
				newMonth,
				newDay,
				endTime.Hours,
				endTime.Minutes,
				endTime.Seconds);
		}
	}

	public class ExtendedDateTimeModel : DateTimeModel
	{

		[Required]
		public Int64 RestaurantId { get; set; }
	}
}
