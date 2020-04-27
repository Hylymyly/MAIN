import java.lang.reflect.Array;

public class KTP10 {
    public static void main(String[]args)
    {
        int[] arr = new int[]{1,5,9};

        System.out.println(ctoa("A"));
    }


public static int reminder(int a, int b)
{
    int c =0;
    if (a < b) return a;
    else
        {c = a%b;
        return c;}
}

public static double triArea(int a, int b)
{
    double p = 0.5*a*b;
    return p;
}

public static int animals(int a, int b, int c)
{
    int numleg = 2 * a + 4 * b + 4 * c;
    return numleg;
}

public static boolean profitableGamble(double a, double b, double c)
{
    if (a*b>c) return true;
    else return false;
}

public static String operation(int N, int b, int c)
{
    if (b-c==N) return "subtracted";
   else if (b+c==N) return "added";
   else if (b*c==N) return "multiply";
   else if (b/c==N) return "divide";
   else return "none";
}

public static int ctoa(String a)
{
    return a.charAt(0);
}

public static int addUpTo(int a)
{
    int res = 0;
    for (int i =1; i <= a; i++)
    {
        res = res + i;
    }
    return res;
}

public static int nextEdge(int a,int b)
{
    int c = (a+b)-1;
    return c;
}

public static int sumOfCubes(int[] arr)
{
    int res = 0;
    for (int i=0; i < arr.length;i++)
    {
      res = res + arr[i]*arr[i]*arr[i];
    }
    return res;
}

public static boolean abcmath(int a, int b, int c)
{
    int res = 0;
    for(int i =1;i<b;i++)
    {
        res = res + a + a;
    }
    if (res%c==0) return true;
    else return false;
}




}