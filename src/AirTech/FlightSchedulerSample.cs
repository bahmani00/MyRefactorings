using Challenges.AirTech;

public class FlightSchedulerSample {
  public static void Start() {
    var maxNumOfDaysToSchedule = 2;
    var maxNumOfFlightPerDay = 3;
    var maxNumOfBoxPerAirplain = 20;


    var processor = new OrderFileDownloader(@"AirTech\coding-assigment-orders.json");
    var orders = processor.Fetch();

    var flightSchedulerOptions = new FlightSchedulerOptions(
      maxNumOfDaysToSchedule,
      maxNumOfFlightPerDay,
      maxNumOfBoxPerAirplain
      );
    var scheduler = new FlightScheduler(orders, flightSchedulerOptions);
    var reservations = scheduler.Schedule("YUL");

    //UserStory1(reservations);
    Console.WriteLine();

    UserStory2(reservations);

    void UserStory1(List<ScheduledReservation> reservations) {
      var day = -1;
      foreach (var resv in reservations) {
        if (day != resv.Day) {
          day = resv.Day;
          Console.WriteLine($"Day: {day}");
        }
        Console.WriteLine($"Flight: {resv.Id}, departure: {resv.Order.Departure}, arrival: {resv.Arrival}, day: {resv.Day}");
      }
    }

    void UserStory2(List<Reservation> reservations) {
      foreach (var reservation in reservations) {
        Console.WriteLine($"{reservation}");
      }
    }
  }
}