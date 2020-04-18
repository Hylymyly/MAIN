import java.sql.SQLOutput;
import java.util.Scanner;

public class KTP20 {
    public static void main(String[]args) {
        int[] arr = new int[]{1, 2, 3};
        int[] ints = cumulativeSum(arr);
        for (int j = 0; j < ints.length; j++) {
            System.out.print(ints[j]+" ");
        }
       System.out.println(getDecimalPlaces("43.02"));
        System.out.println(isPrefix("subratiophobia","suk-"));
    }

public static String repeat(String a, int b)
{
    String str="";
    for(int i =0;i<a.length();i++){
        for (int g=0;g<b;g++){
            str += a.charAt(i);
        }
    }
return str;
}

public static int defferenceMaxMin(int[] arr)
{
    int min =arr[0];
    int max=0;
    for (int i =0;i<arr.length;i++)
    {
        if (arr[i]>max) max = arr[i];
        if (arr[i]<min) min = arr[i];
    }
    int res = 0;
    res = max - min;
    return res;
}

public static boolean isAvgWhole(int[] arr)
{
    double sum = 0;
    double res =0;
    for (int i =0;i<arr.length;i++)
    {
        sum = sum+arr[i];
    }
    res = sum / (arr.length);
   if (res%1==0) return true;
   else return false;
}

public static int[] cumulativeSum(int[] arr)
{
    int res = arr.length;
    int[] arr2 = new int[res];
    arr2[0]=arr[0];
    for (int i=1;i<arr.length;i++)
    {
        arr2[i]=arr[i]+arr2[i-1];
    }
    return arr2;
}

public static int getDecimalPlaces(String a)
{
    if(!(a.contains("."))) return 0;
    String[] splitter = String.valueOf(a).split("\\.");
    int i = splitter[1].length();
    return i;
}

public static int Fibonacci(int a)
{
    int[] arr2 = new int[a];
    arr2[0]=1;
    arr2[1]=2;
    for (int i = 2; i<arr2.length;i++)
    {
        arr2[i]=arr2[i-2]+arr2[i-1];
    }
    return arr2[a-1];
}

public static boolean isStrangePair(String a)
{
    Scanner scanner = new Scanner(a);
    if ((scanner.hasNextInt())&&(a.length()<6)) return true;
    else return false;
}

public static boolean isStrangePair(String a, String b)
{
    if ((a.charAt(0)==b.charAt(b.length()-1))&&(a.charAt(a.length()-1)==b.charAt(0))) return true;
    else return false;
}

public static boolean isSuffix(String a, String b)
{
    return a.endsWith(b.substring(1));
}

public static boolean isPrefix(String a, String b)
{
    return a.startsWith(b.substring(0,b.length()-1));
}

public static int boxSeq(int a)
{
    if (a%2==0) return a; else return a+2;
}

}
