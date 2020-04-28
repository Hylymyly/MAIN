import java.util.Random;
import java.util.Arrays;

public class MAIN {
    static int rows = 10;//строчки
    static int cols = 10;//столбцы

    public static void main(String[] args) {
        int[][] array = new int[rows][cols];
        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < cols; j++) {
                array[i][j] = (int) (Math.random() * 100);
            }
        }
        long time = System.nanoTime();
        System.out.println("Shell");
        shSort(copyMatr(array));
        System.out.println((System.nanoTime() - time) / 1000);
        time = 0;

        long time2 = System.nanoTime();
        System.out.println("Quick");
        quickNor(copyMatr(array));
        System.out.println((System.nanoTime() - time2) / 1000);
        time2 = 0;

        long time1 = System.nanoTime();
        System.out.println("Standart");
        standartSort(array);
        System.out.println((System.nanoTime() - time1) / 1000);
        time1 = 0;

        printMatr(array);
    }

    public static void standartSort(int[][] array) {
        for (int i = 0; i < rows; i++) {
            Arrays.sort(array[i]);
        }
    }

    public static void printMatr(int[][] array) {
        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < cols; j++) {
                if (j == cols - 1)
                    System.out.println(array[i][j]);
                else
                    System.out.print(array[i][j] + "\t");
            }
        }
    }


    public static int[][] quickNor(int[][] array) {
        for (int i = 0; i < rows; i++) {
            quickSort(array[i], 0, cols - 1);
        }
        return array;
    }

    public static void quickSort(int[] matr, int low, int high) {
        if (low >= high)
            return;//завершить выполнение если уже нечего делить

        // выбрать опорный элемент
        int middle = low + (high - low) / 2;
        int opora = matr[middle];

        // разделить на подмассивы, который больше и меньше опорного элемента
        int i = low, j = high;
        while (i <= j) {
            while (matr[i] < opora) {
                i++;
            }

            while (matr[j] > opora) {
                j--;
            }

            if (i <= j) {//меняем местами
                int temp = matr[i];
                matr[i] = matr[j];
                matr[j] = temp;
                i++;
                j--;
            }
        }

        // вызов рекурсии для сортировки левой и правой части
        if (low < j)
            quickSort(matr, low, j);

        if (high > i)
            quickSort(matr, i, high);
    }


    public static void shSort(int[][] array) {
        int h = 1;
        while (h * 3 < array.length)
            h = h * 3 + 1;
        while (h >= 1) {
            shellSort(array, h);
            h = h / 3;
        }
    }

    public static void shellSort(int[][] array, int h) {
        int length = cols;
        for (int k = 0; k < rows; k++) {
            for (int i = h; i < length; i++) {
                for (int j = i; j >= h; j = j - h) {
                    if (array[k][j] < array[k][j - h]) {
                        int temp = array[k][j];
                        array[k][j] = array[k][j - h];
                        array[k][j - h] = temp;
                    } else
                        break;
                }
            }
        }
    }

    public static void swap(int[][] array, int i, int j) {
        for (int k = 0; k < rows; k++) {
            int temp = array[k][i];
            array[k][i] = array[k][j];
            array[k][j] = temp;
        }
    }

    public static int[][] copyMatr(int array[][]) {
        int[][] newMatr = new int[rows][cols];
        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < cols; j++) {
                newMatr[i][j] = array[i][j];
            }
        }
        return newMatr;
    }


}
