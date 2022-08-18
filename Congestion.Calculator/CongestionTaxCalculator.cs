using System;
using System.Linq;

namespace congestion.calculator
{
    public class CongestionTaxCalculator
    {
        /**
             * Calculate the total toll fee for one day
             *
             * @param vehicle - the vehicle
             * @param dates   - date and time of all passes on one day
             * @return - the total congestion tax for that day
             */

        public int GetTax(Vehicle vehicle, DateTime[] dates)
        {
            dates = dates.OrderBy(d => d.Ticks).ToArray();
            DateTime intervalStart = dates[0];
            int totalFee = 0;
            int currentDayFee = 0;
            foreach (DateTime date in dates)
            {
                if(date.Date != intervalStart.Date)
                {
                    totalFee += currentDayFee > 60 ? 60 : currentDayFee;
                    currentDayFee = 0;
                }
                int nextFee = GetTollFee(date, vehicle);
                int tempFee = GetTollFee(intervalStart, vehicle);

                long diffInTicks = Math.Abs(date.Ticks - intervalStart.Ticks);
                double minutes = TimeSpan.FromTicks(diffInTicks).TotalMinutes;
                if (minutes <= 60)
                {
                    if (currentDayFee > 0) currentDayFee -= tempFee;
                    if (nextFee >= tempFee) tempFee = nextFee;
                    currentDayFee += tempFee;
                }
                else
                {
                    currentDayFee += nextFee;
                    intervalStart = date;
                }
            }
            totalFee += currentDayFee > 60 ? 60 : currentDayFee;

            return totalFee;
        }

        private bool IsTollFreeVehicle(Vehicle vehicle)
        {
            if (vehicle == null) return false;
            String vehicleType = vehicle.GetVehicleType();
            return vehicleType.Equals(TollFreeVehicles.Motorcycle.ToString()) ||
                   vehicleType.Equals(TollFreeVehicles.Tractor.ToString()) ||
                   vehicleType.Equals(TollFreeVehicles.Emergency.ToString()) ||
                   vehicleType.Equals(TollFreeVehicles.Diplomat.ToString()) ||
                   vehicleType.Equals(TollFreeVehicles.Foreign.ToString()) ||
                   vehicleType.Equals(TollFreeVehicles.Military.ToString());
        }

        public int GetTollFee(DateTime date, Vehicle vehicle)
        {
            if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle)) return 0;

            int hour = date.Hour;
            int minute = date.Minute;

            if (hour == 6 && minute <= 29) return 8;
            else if (hour == 6) return 13;
            else if (hour == 7) return 18;
            else if (hour == 8 && minute <= 29) return 13;
            else if (hour >= 8 && hour <= 14) return 8;
            else if (hour == 15 && minute <= 29) return 13;
            else if (hour == 15 || hour == 16) return 18;
            else if (hour == 17) return 13;
            else if (hour == 18 && minute <= 29) return 8;
            else return 0;
        }

        private Boolean IsTollFreeDate(DateTime date)
        {
            int year = date.Year;
            int month = date.Month;
            int day = date.Day;

            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return true;

            if (year == 2013)
            {
                // Holidays
                if (month == 1 && day == 1 ||
                    month == 3 && (day == 28 || day == 29) ||
                    month == 4 && (day == 1 || day == 30) ||
                    month == 5 && (day == 1 || day == 8 || day == 9) ||
                    month == 6 && (day == 5 || day == 6 || day == 21) ||
                    month == 7 ||
                    month == 11 && day == 1 ||
                    month == 12 && (day == 24 || day == 25 || day == 26 || day == 31))
                {
                    return true;
                }
            }
            return false;
        }

        private enum TollFreeVehicles
        {
            Motorcycle = 0,
            Tractor = 1,
            Emergency = 2,
            Diplomat = 3,
            Foreign = 4,
            Military = 5
        }
    }
}