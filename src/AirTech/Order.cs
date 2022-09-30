using System.Diagnostics.CodeAnalysis;

namespace Challenges.AirTech;


public record class Order(string OrderNum, string Departure, int Priority);

public class Reservation : IEqualityComparer<Reservation> {
  public int Id { get; }
  public Order Order { get; }

  public Reservation(int id, Order order) {
    Id = id;
    Order = order;
  }

  public override string ToString() => $"order: {Order.OrderNum}, flightNumber: not scheduled";

  public bool Equals(Reservation x, Reservation y) => x?.Id == y?.Id;

  public int GetHashCode([DisallowNull] Reservation obj) => obj.Id.GetHashCode();
}

public class ScheduledReservation: Reservation {
  public string Arrival { get; }
  public int Day { get; }
  
  public ScheduledReservation(int id, int day, string arrival, Order order): base(id, order) {
    Arrival = arrival;
    Day = day;
  }

  public override string ToString() => $"order: {Order.OrderNum}, flightNumber: {Id}, departure: {Order.Departure}, arrival: {Arrival}, day: {Day}";
}
