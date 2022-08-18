using System;
using Xunit;
using congestion.calculator;
using FluentAssertions;

namespace COngestion.Calculator.Tests
{
    public class CalculatorTests
    {
        #region Get Toll Fee
        [Fact]
        public void should_return_zero_for_toll_free_vehicle()
        {
            var Motorcycle = new Motorcycle();
            var calculator = new CongestionTaxCalculator();
            var date = DateTime.Parse("2013-01-14 21:00:00");

            var result = calculator.GetTollFee(date, Motorcycle);

            result.Should().Be(0);
        }

        [Fact]
        public void should_return_zero_for_toll_free_day()
        {
            var car = new Car();
            var calculator = new CongestionTaxCalculator();
            var date = DateTime.Parse("2013-01-13 21:00:00");

            var result = calculator.GetTollFee(date, car);

            result.Should().Be(0);
        }
        [Theory]
        [InlineData("2013-01-14 21:00:00", 0)]
        [InlineData("2013-01-15 21:00:00", 0)]
        [InlineData("2013-02-07 06:23:27", 8)]
        [InlineData("2013-02-07 15:27:00", 13)]
        [InlineData("2013-02-08 06:27:00", 8)]
        [InlineData("2013-02-08 06:20:27", 8)]
        [InlineData("2013-02-08 14:35:00", 8)]
        [InlineData("2013-02-08 15:29:00", 13)]
        [InlineData("2013-02-08 15:47:00", 18)]
        [InlineData("2013-02-08 16:01:00", 18)]
        [InlineData("2013-02-08 16:48:00", 18)]
        [InlineData("2013-02-08 17:49:00", 13)]
        [InlineData("2013-02-08 18:29:00", 8)]
        [InlineData("2013-02-08 18:35:00", 0)]
        [InlineData("2013-03-26 14:25:00", 8)]
        [InlineData("2013-03-28 14:07:27", 0)]
        public void should_return_toll_fee_for_The_date(string dateTime, int expected)
        {
            var car = new Car();
            var calculator = new CongestionTaxCalculator();
            var date = DateTime.Parse(dateTime);

            var result = calculator.GetTollFee(date, car);

            result.Should().Be(expected);
        }
        #endregion
        #region Get Tax
        [Fact]
        public void should_return_zero_for_toll_free_vehicle_with_one_date()
        {
            var Motorcycle = new Motorcycle();
            var calculator = new CongestionTaxCalculator();
            var dates = new DateTime[] { DateTime.Parse("2013-01-14 14:00:00") };

            var result = calculator.GetTax(Motorcycle, dates);

            result.Should().Be(0);
        }
        [Fact]
        public void should_return_zero_for_toll_free_vehicle_with_multiple_dates()
        {
            var Motorcycle = new Motorcycle();
            var calculator = new CongestionTaxCalculator();
            var dates = new DateTime[]{
                DateTime.Parse("2013-01-14 14:50:00"),
            DateTime.Parse("2013-01-14 06:59:00")
            };

            var result = calculator.GetTax(Motorcycle, dates);

            result.Should().Be(0);
        }
        [Fact]
        public void should_return_total_fee()
        {
            var car = new Car();
            var calculator = new CongestionTaxCalculator();
            var dates = new DateTime[]{
                DateTime.Parse("2013-01-14 14:50:00"),
            DateTime.Parse("2013-01-14 06:50:00")
            };

            var result = calculator.GetTax(car, dates);

            result.Should().Be(21);
        }
        [Theory]
        [InlineData("2013-02-08 16:50:00", "2013-02-08 17:01:00", 18)]
        [InlineData("2013-02-08 07:58:00", "2013-02-08 08:57:00", 18)]
        [InlineData("2013-02-08 07:58:00", "2013-02-08 08:59:00", 26)]
        public void should_return_price_based_on_the_single_charge_rule(string timeOne, string timeTwo, int expected)
        {
            var car = new Car();
            var calculator = new CongestionTaxCalculator();
            var dates = new DateTime[]{
                DateTime.Parse(timeOne),
            DateTime.Parse(timeTwo)
            };

            var result = calculator.GetTax(car, dates);

            result.Should().Be(expected);
        }

        [Theory]
        [InlineData("2013-02-08 16:50:00", "2013-02-08 17:31:00", "2013-02-08 18:28:00", 26)]
        [InlineData("2013-02-08 07:58:00", "2013-02-08 08:57:00", "2013-02-08 09:56:00", 26)]
        public void should_return_price_based_on_the_single_charge_rule_without_stacking(string timeOne, string timeTwo, string timeThree, int expected)
        {
            var car = new Car();
            var calculator = new CongestionTaxCalculator();
            var dates = new DateTime[]{
                DateTime.Parse(timeOne),
            DateTime.Parse(timeTwo),
            DateTime.Parse(timeThree),
            };

            var result = calculator.GetTax(car, dates);

            result.Should().Be(expected);
        }
        [Fact]
        public void should_return_60_for_maximum_tax_in_one_day()
        {
            var car = new Car();
            var calculator = new CongestionTaxCalculator();
            var dates = new DateTime[]{
                DateTime.Parse("2013-02-08 06:01:00"),
                DateTime.Parse("2013-02-08 07:02:00"),
                DateTime.Parse("2013-02-08 08:03:00"),
                DateTime.Parse("2013-02-08 09:04:00"),
                DateTime.Parse("2013-02-08 10:05:00"),
                DateTime.Parse("2013-02-08 11:06:00"),
                DateTime.Parse("2013-02-08 12:07:00"),
                DateTime.Parse("2013-02-08 13:08:00"),
                DateTime.Parse("2013-02-08 14:09:00"),
                DateTime.Parse("2013-02-08 15:10:00"),
                DateTime.Parse("2013-02-08 16:11:00"),
                DateTime.Parse("2013-02-08 17:12:00"),
                DateTime.Parse("2013-02-08 18:13:00"),
            };

            var result = calculator.GetTax(car, dates);

            result.Should().Be(60);
        }
        #endregion
    }
}
