public class KTP30 {
    public static void main(String[]args) {
      /*int[] arr = new int[]{1, 2, 3};
        int[] ints = cumulativeSum(arr);
        for (int j = 0; j < ints.length; j++) {
            System.out.print(ints[j]+" ");
        }
        System.out.println();
        System.out.println();*/
        /*int[] arr = new int[]{9,8,7,6};
        int[] arr2 = new int[]{4,4,3,1};
        System.out.println(same(arr,arr2));*/
        //System.out.println(isKaprekar(298));
        //System.out.println(longestZero("1111"));
        //System.out.println(nextprime(10));
        // System.out.println(rightTriangle(70,130,110));
    }

    public static int solutions(int a, int b, int c)
    {   int f =0;
        int res = 0;
        res = b*b-4*a*c;
        if (res > 0) f=2;
        else if (res==0) f=1;
        else if (res<0) f=0;
        return f;
    }

    public static int findZip(String a)
    {
        return a.replaceFirst("zip","***").indexOf("zip");
    }

    public static boolean checkPerfect(int a)
    {
        int res =0;
        for (int i =1; i<=(a/2);i++)
        {
            if (a%i==0) res = res + i;
        }
        if (res == a) return true; else return false;
    }

    public static String flipEndChars(String a)
    {
        if (a.length()<2) return "Incompatible";
        else if (a.charAt(0)==a.charAt(a.length()-1)) return "Two's a pair.";
       else return a.charAt(a.length()-1)+a.substring(1,a.length()-1)+a.charAt(0);

    }

    public static boolean isValidHexCode(String a)
    {
        if (a.length()>6) return false;
        else if (a.matches("#[a-fA-F0-9]+")) return true; else return false;
    }

    private static boolean same(int[] arr,int[] arr2)
    {
        int s = 0;
        int k =0;
        int c = 0;
        int c2 =0;
        int res1=0;
        int res2=0;
        for (int i =0; i<arr.length;i++)
        {   c++;
            for (int i2 = i+1;i2<arr.length;i2++ ){
                if (arr[i]==arr[i2]) {
                    s++;
                    break;
                }
        }
        }
        for (int j =0; j<arr2.length;j++)
        {   c2++;
            for (int j2 = j+1;j2<arr2.length;j2++ ){
                if (arr2[j]==arr2[j2]) {
                    k++;
                    break;
                }
        }
        }
        res1 = c-s;
        res2 = c2-k;
        if (res1==res2) return true; else return false;
    }

    public static boolean isKaprekar(int a)
    {
        int kvad = a*a;
        String str = String.valueOf(kvad);
        if (str.length()==1) return kvad==a;
        else return Integer.parseInt(str.substring(0,str.length()/2))+Integer.parseInt(str.substring(str.length()/2))==a;
    }

    public static String longestZero(String s)
    {
        String res = "";
        String m = "";
        for (int i = 0; i<s.length();i++){
            if (s.charAt(i)=='0') res= res + '0';
            else {
               if(m.length()<res.length()) m=res;
               res = "";
            }
        }
        if (m.length()<res.length()) m=res;
        return m;
    }

    public static int nextprime(int a)
    {
        while (true){
            boolean fl=true;
            for (int i =2;i<a;i++){
                if (a%2==0){
                    a++;
                    fl=false;
                }
            }
            if (fl==true) break;
        }
        return a;
    }

    public static boolean rightTriangle(int a, int b,int c)
    {
        if ((a*a+b*b==c*c)||(b*b+c*c==a*a)||(c*c+a*a==b*b)) return true; else return false;
    }
}
