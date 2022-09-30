
using Challenges.AirTech;

namespace Test.AirTech;

public class FlightSchedulerOptionsTest {

  [Theory()]
  [InlineData(0, 1, 1)]
  [InlineData(1, 0, 1)]
  [InlineData(1, 1, 0)]
  public void FlightSchedulerOptions_values_cannot_be_zero(
    int maxNumOfDaysToSchedule,
    int maxNumOfFlightPerDay,
    int maxNumOfBoxPerAirplain) {

    var actualException = Assert.Throws<ArgumentException>(() => 
      new FlightSchedulerOptions(
        maxNumOfDaysToSchedule,
        maxNumOfFlightPerDay,
        maxNumOfBoxPerAirplain)
      );
   
    Assert.Equal("Input should be positive number.", actualException.Message);
  }


  [Theory()]
  [InlineData(10)]
  [InlineData(100)]
  [InlineData(1_000)]
  [InlineData(10_000)]
  public void FlightSchedulerOptions_order_cannot_be_sheduled(int orderNumber) {
    var options = new FlightSchedulerOptions(1, 1, 1);

    var actual = options.CanBeScheduled(orderNumber);

    Assert.False(actual);
  }


  [Theory()]
  [InlineData(0, 1)]
  [InlineData(1, 1)]
  [InlineData(10, 1)]
  [InlineData(100, 2)]
  [InlineData(1_000, 11)]
  [InlineData(10_000, 101)]
  public void FlightSchedulerOptions_order_is_sheduled(int orderNumber, int expectedDay) {
    var options = new FlightSchedulerOptions(int.MaxValue, 10, 10);

    var actual = options.GetDayNumber(orderNumber);

    Assert.Equal(expectedDay, actual);
  }

  [Theory()]
  [InlineData(0, 1)]
  [InlineData(1, 1)]
  [InlineData(10, 1)]
  [InlineData(100, 1)]
  [InlineData(1_000, 1)]
  [InlineData(10_000, 1)]
  public void FlightSchedulerOptions_order_can_be_sheduled_when_unlimited_airplane_is_available(int orderNumber, int expectedDay) {
    var options = new FlightSchedulerOptions(1, int.MaxValue, 1);

    var actual = options.GetDayNumber(orderNumber);

    Assert.Equal(expectedDay, actual);
  }

  [Theory()]
  [InlineData(0, 1)]
  [InlineData(1, 1)]
  [InlineData(10, 10)]
  [InlineData(100, 100)]
  [InlineData(1_000, 1_000)]
  [InlineData(10_000, 10_000)]
  public void FlightSchedulerOptions_order_can_be_sheduled_when_airplane_can_take_unlimited_boxs(int orderNumber, int expectedDay) {
    var options = new FlightSchedulerOptions(1, 1, int.MaxValue);

    var actual = options.GetDayNumber(orderNumber);

    Assert.Equal(expectedDay, actual);
  }
}
