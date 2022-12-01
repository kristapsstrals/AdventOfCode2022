public class Elf
{
    public List<int> Calories { get; }

    public Elf() {
        Calories = new List<int>();
    }

    public Elf(List<int> calories)
    {
        Calories = calories;
    }

    public int CalorySum => Calories.Sum();
}