namespace Challenges.AirTech;

public class FlightScheduler {
  private readonly IReadOnlyList<Order> orders;
  private readonly FlightSchedulerOptions schedulerOptions;

  public FlightScheduler(
    IReadOnlyList<Order> orders,
    FlightSchedulerOptions schedulerOptions) { 
    this.orders = orders;
    this.schedulerOptions = schedulerOptions;
  }

  public List<Reservation> Schedule(string arrival) {
    var reservations = new List<Reservation>();

    var orderIndex = 0;
    while (orderIndex < orders.Count) {
      if (!schedulerOptions.CanBeScheduled(orderIndex)) break;

      var day = schedulerOptions.GetDayNumber(orderIndex);
      var order = orders[orderIndex++];
      reservations.Add(new ScheduledReservation(orderIndex, day, arrival, order));
    }

    while (orderIndex < orders.Count) {
      var order = orders[orderIndex++];
      reservations.Add(new Reservation(orderIndex, order));
    }
    return reservations;
  }
}

public class FlightSchedulerOptions {
  public FlightSchedulerOptions(int maxNumOfDaysToSchedule, int maxNumOfFlightPerDay, int maxNumOfBoxPerAirplain) {
    Gaurd.IsPositive(maxNumOfDaysToSchedule);
    Gaurd.IsPositive(maxNumOfFlightPerDay);
    Gaurd.IsPositive(maxNumOfBoxPerAirplain);

    this.maxNumOfDaysToSchedule = maxNumOfDaysToSchedule;

    try {
      checked {
        this.numOfBoxPerDay = maxNumOfFlightPerDay * maxNumOfBoxPerAirplain;
      }
    } catch (Exception) {
      this.numOfBoxPerDay = maxNumOfBoxPerAirplain;
    }
  }

  private int maxNumOfDaysToSchedule;
  private int numOfBoxPerDay;

  public int GetDayNumber(int orderNumber) =>
    Math.Min(maxNumOfDaysToSchedule, 1 + (orderNumber / numOfBoxPerDay));

  public bool CanBeScheduled(int orderNumber) =>
     maxNumOfDaysToSchedule > orderNumber / numOfBoxPerDay;
}
