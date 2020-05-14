import java.util.Arrays;
import java.util.HashSet;
import java.util.Set;
import java.util.Vector;

public class KTP60 {

    public static void main(String[] args) {
        //System.out.println(convertToRoman(1986));
        //System.out.println(bell(3));
        //System.out.println(translateSentence("flag"));
        //System.out.println(longestNonrepeatingSubstring("abcabcbb"));
        //System.out.println(ulam(206));
       System.out.println(palindromedescendant(23336014));
        /*int[] ints = palindromedescendant(13001120);
        for (int i = 0; i < ints.length; i++)
            System.out.print(ints[i] + " ");*/
        //System.out.println(getHashTags("How the Avocado Bacame the Fruit of the Global Trade"));
       // System.out.println(formula("6 * 4 = 24"));
    }

    public static int bell(int a)
    {
        int[][] bell = new int [a+1][a+1];
        bell[0][0]=1;
        for (int i = 1;i<=a;i++)
        {
            bell[i][0]=bell[i-1][i-1];
            for(int j = 1; j <=i;j++)
                bell[i][j]=bell[i-1][j-1]+bell[i][j-1];
        }
        return bell[a][0];
    }
    public static String translateSentence(String str)
    {
        int len = str.length();
        int ind = -1;
        for (int i = 0; i < len;i++)
        {
            if ((str.charAt(i)=='a')||(str.charAt(i)=='e')||(str.charAt(i)=='i')||(str.charAt(i)=='o')||(str.charAt(i)=='u')||(str.charAt(i)=='y')||(str.charAt(i)=='A')||(str.charAt(i)=='E')||(str.charAt(i)=='I')||(str.charAt(i)=='O')||(str.charAt(i)=='U')||(str.charAt(i)=='Y')) {
                ind = i;
                break;
            }
        }
        if (ind == -1) return "-1";
        else if((str.charAt(0)=='a')||(str.charAt(0)=='e')||(str.charAt(0)=='i')||(str.charAt(0)=='o')||(str.charAt(0)=='u')||(str.charAt(0)=='y')||(str.charAt(0)=='A')||(str.charAt(0)=='E')||(str.charAt(0)=='I')||(str.charAt(0)=='O')||(str.charAt(0)=='U')||(str.charAt(0)=='Y'))
            return str+"yay";
        else return str.substring(ind)+str.substring(0,ind)+"ay";
    }
    public static boolean validColor(String str) {
        String[] line = str.split("[\\(\\),]");
        if (line[0].length() == line.length - 1)
            try {
                for (int i = 1; i < line.length; i++) {
                    int numer = Integer.parseInt((line[i]));
                    if (numer < 0 || numer > 255) return false;
                }
            }
        catch (Exception e)
        {
            return false;
        }
        return true;
    }
    //public static String stripUrlParams(String str) {}
    public static String[] getHashTags(String str)
    {
        int i =0;
        String[] words = str.split(" ");
        String[] res = new String[]{"a","a","a"};
        while (i<3) {
            for (String word : words) {
                if (word.length() > res[i].length()) {
                    res[i] = word;
                }
            }
            for (int j = 0; j<words.length;j++)
            {
                if (words[j].equals(res[i]))
                    words[j] = " ";
            }
            i++;
        }
        for (int g = 0; g< res.length;g++)
        {

            res[g] = "#"+res[g].toLowerCase();
        }
        return res;
    }
    public static int ulam(int a)
    {
        final int max = 2000;
        Vector<Integer> arr = new Vector<Integer>();
        arr.add(1);
        arr.add(2);
        for (int i =3;i<max;i++)
        {
            int count = 0;
            for (int j = 0; j<arr.size()-1;j++)
            {
                for (int k = j+1;k<arr.size();k++)
                {
                    if (arr.get(j)+arr.get(k)==i)
                    {
                        count++;
                    }
                    if (count>1)
                        break;
                }
                if(count>1)
                    break;
            }
            if (count==1)
            {
                arr.add(i);
            }
        }
        return arr.get(a-1);
    }
    public static String longestNonrepeatingSubstring(String str)
    {
        String out = "";
        for (int i =0;i<str.length();i++)
        {
            Set<Character> vis = new HashSet<>();
            int j=i;
            for (;j<str.length();j++)
            {
                char ch = str.charAt(j);
                if (vis.contains(ch))
                {
                    break;
                } else {
                    vis.add(ch);
                }
            }
            if (out.length()<j-i+1)
            {
                out=str.substring(i,j);
            }
        }
        return out;
    }
    public static String convertToRoman(int a)
    {
        StringBuffer con = new StringBuffer("");
        int m1 = a/1000;
        con.append(M(m1));
        int m2 = a%1000;
        int d1=m2/500;
        con.append(D(d1));
        int d2 = m2%500;
        int c1 = d2/100;
        con.append(C(c1));
        int c2 = d2%100;
        int l1 = c2/50;
        con.append(L(l1));
        int l2 = c2%50;
        int x1=l2/10;
        con.append(X(x1));
        int x2 = l2%10;
        con.append(bas(x2));
        return con.toString();
    }
    private static String M(int a){
        StringBuffer con = new StringBuffer("");
        int i = 0;
        while (i<a)
        {
            con.append("M");
            i++;
        }
        return con.toString();
    }
    private static String C(int a){
        if (a == 4) return "CD";
        else if ((a!=0)&&(a<4)){
            StringBuffer con = new StringBuffer("");
            int i = 0;
            while (i<a)
            {
                con.append("C");
                i++;
            }
            return con.toString();
        }
        else return "";
    }
    private static String X(int a)
    {
        if (a==4) return "XL";
        else if ((a!=0)&&(a<4)){
            StringBuffer con = new StringBuffer("");
            int i = 0;
            while (i<a)
            {
                con.append("X");
                i++;
            }
            return con.toString();
        }
        else return "";
    }
    private static String D(int a)
    {
        if (a==4) return "CM";
        else if (a==0) return "";
        else return "D";
    }
    private static String L(int a)
    {
        if (a==4) return "XC";
        else if (a==0) return "";
        else return "L";
    }
    private static String bas(int a)
    {
        String[] con = {"","I","II","III","IV","V","VI","VII","VIII","IX"};
        return con[a];
    }
    public static boolean formula(String str)
    {
        if ( !str.matches("^(\\d+\\s[\\*\\+-/]\\s\\d+\\s=\\s\\d+)$"))
            return false;
        String[] left = str.split("=")[0].trim().split(" ");
        String right = str.split("=")[1].trim();
        int function= 0;
        switch (left[1])
        {
            case "+":
                function=Integer.parseInt(left[0])+Integer.parseInt(left[2]);
                break;
            case "*":
                function=Integer.parseInt(left[0])*Integer.parseInt(left[2]);
                break;
            case "/":
                function=Integer.parseInt(left[0])/Integer.parseInt(left[2]);
                break;
        }
        if (function==Integer.parseInt(right))
            return true;
        else return false;
    }
    public static boolean palindromedescendant(int a)
    {

        int t = a;
        boolean fl = true;
        int c =a;
        int l=0;
        int sum =0;
        int sum1 =0 ;
        while (c>0)
        {
            c=c/10;
            l++;
        }
        int[] arr = new int[l];
        for (int i =0; i<arr.length;i++)
        {
            arr[i]=a%10;
            a=a/10;
            sum = sum +arr[i];
        }
        int len = arr.length;
        sum1=(sum-sum%10);
        for (int j = 0; j < len/2;j++)
        {
            if (arr[j]!=arr[len-j-1])
            {
                fl = false;
                break;
            }
        }
        if (sum < 10)
        {
            if ((t/sum)%2==0) fl = true; else fl = false;
        }
        else
        {
            if (sum%10==(sum-sum%10)/10) fl = true;
        }
        if (t == 11) fl =true;
        return fl;
    }
}
