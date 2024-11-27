using System.Collections.Generic;

public class QuickSort
{
    public int Partition(List<leaderboardManager.PlayersTime> arr, int left, int right)
    {
        float pivot = arr[(left + right) / 2].Time;  

        while (true)
        {
            // Mover left hacia la derecha hasta que encontremos un tiempo mayor o igual que el pivote
            while (arr[left].Time < pivot) left++;

            // Mover right hacia la izquierda hasta que encontremos un tiempo menor o igual que el pivote
            while (arr[right].Time > pivot) right--;

            if (left < right)
            {
                // Intercambiamos los elementos
                leaderboardManager.PlayersTime temp = arr[left];
                arr[left] = arr[right];
                arr[right] = temp;

                // Avanzamos hacia la siguiente posición
                left++;
                right--;
            }
            else
            {
                return right;
            }
        }
    }

    public void Sort(List<leaderboardManager.PlayersTime> arr, int left, int right)
    {
        if (left < right)
        {
            int pivot = Partition(arr, left, right);
            Sort(arr, left, pivot);
            Sort(arr, pivot + 1, right);
        }
    }
}
