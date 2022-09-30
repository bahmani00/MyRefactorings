namespace Challenges.AirTech;

public static class Gaurd {
  public static bool IsPositive(int input) {
    if (input <= 0) throw new ArgumentException("Input should be positive number.");

    return true;
  }
}
