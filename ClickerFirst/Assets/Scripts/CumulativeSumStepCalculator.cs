using UnityEngine;

public class CumulativeSumStepCalculator : MonoBehaviour
{
    /// <summary>
    /// Вычисляет кумулятивную сумму для последовательности с заданным шагом.
    /// </summary>
    /// <param name="step">Шаг последовательности (например, 5 для 0, 5, 10...).</param>
    /// <param name="index">Индекс последовательности (например, 4 для последовательности до 20).</param>
    /// <returns>Кумулятивная сумма последовательности с заданным шагом.</returns>
    public static int GetCumulativeSum(int step, int index)
    {
        return step * index * (index + 1) / 2;
    }
}
