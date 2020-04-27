import java.util.ArrayList;
import java.util.Enumeration;
import java.util.List;
import java.util.Scanner;

public class KTP40 {

    public static void main(String[] args) {
        double[] arr = new double[]{16, 18, 30, 1.8};
        System.out.println(trouble(1222345,12345));
        /*int[] ints = trouble(4511,234);
        for (int i = 0; i < ints.length; i++)
            System.out.print(ints[i] + " ");*/
      //  String a = "hellow my name is Bessie and this is my eassay";
       // System.out.println(text(10,7,a));
       // System.out.println(split("()()()"));
        //System.out.println(toCamelCase("hello_edabit"));
        //System.out.println(toSnakeCase("helloEdabit"));
       // System.out.println(overTime(arr));
       // System.out.println(BMI("55 kilos","1.65 meters"));
       //System.out.println(bugger(10));
        //System.out.println(toStarShorthand("abbcccccc"));
        //System.out.println(doesRhyme("Sam I AoM!","Green eggs and ham."));
    }

    public static String text(int N,int L,String a)
    {
        String[] line = a.split(" ");
        String line2 ="";
        int k=0;
        for (int i=0;i<N;i++)
        {    if(line[i].length()+k<=L){
            line2+=line[i]+" ";
            k+=line[i].length();
        } else { line2 = line2.trim();
            line2+="\n";
            line2+=line[i]+" ";
            k=line[i].length();
        }
        }
        return line2.trim();
    }

    public static String split(String a)
    {
       char[] chars = a.toCharArray();
       String line="[";
       int k=0,l=0;
       for(int i=0;i<chars.length;i++)
       {
           if(chars[i]=='('){
               if (k==0) line+="'";
               line+="(";
               k++;
           } else {
               l++;
               if(l==k){
                   line+=")'";
                   k=0;
                   l=0;
                   if(i+1!=chars.length) line+=",";
               } else {
                   line+=")";
               }
           }
       }
       return line+"]";
    }

    public static String toCamelCase(String a)
    {
        String[] line = a.split("_");
        String line2 =line[0];
        for (int i=0;i<line.length;i++)
        {
            line2+=Character.toUpperCase(line[i].charAt(0))+line[i].substring(1);
        }
        return line2;
    }

    public static String toSnakeCase(String a)
    {
        String line ="";
        char[] chars = a.toCharArray();
        char[] chars1 = a.toLowerCase().toCharArray();
        for (int i =0; i<chars1.length;i++)
        {
            if(Character.toUpperCase(chars1[i])==chars[i])
            {
                line+="_"+chars1[i];
            } else {
                line+=chars1[i];
            }
        }
        return line;
    }

    public static String overTime(double[] arr)
    {
        double f=arr[0];
        double l=arr[1];
        double st=arr[2];
        double mul=arr[3];
        double sum =0;
        String res =" ";
        if (l<18) sum = (l-f)*st; else sum = (17-f)*st+(l-17)*mul*st;
        res="'$"+sum+"'";
        return res;
    }

    public static String BMI(String a,String b)
    {
       double kilog=0,mete=0,res=0;
       String line = a.split(" ")[1];
       kilog = Double.parseDouble(a.split(" ")[0]);
       mete = Double.parseDouble(b.split(" ")[0]);
       if (line.equals("pounds")){
           kilog=kilog/2.2;
       }
       line=b.split(" ")[1];
       if (line.equals("inches")) {
           mete=mete*2.54/100;
       }
       res = kilog/(mete*mete);
       if(res<18.5)
       {
           return res+" Underweight";
       }
       else if (res<24.9)
       {
           return res+" Normal weight";
       }
       else return res+" Overweight";
    }

    public static int bugger(int a)
    {
        int k=0;
        int b=a;
        while (a>10) {
            b = 1;
            while (a!=0) {
                b = b * (a % 10);
                a = a / 10;
            }
            a=b;
            k++;
        }
        return k;
    }

    public static String toStarShorthand(String str)
    {
     /*  StringBuilder sb = new StringBuilder();
       int idx;
       for (int i = 0; i < str.length(); i++)
       {
           char c = str.charAt(i);
           idx = str.indexOf(c,i+1);
           if (idx == -1)
           {
               sb.append(c);
           }
       }*/
      String line = "";
      char chars =str.charAt(0);
      int l=1;
      for (int i=1;i<str.length();i++){
          if(str.charAt(i)==chars) l++;
          else
          {if(l>1)
              line=line+chars+"*"+l;
          else line=line+chars;
          chars=str.charAt(i);
          l=1;}
      }
      if (l>1) line=line+chars+"*"+l;
      else line=line+chars;
      if(str.isEmpty()) return "";
      else return line;
    }

    public static boolean doesRhyme(String a, String b)
    {
        a=a.substring(a.lastIndexOf(" "));
        b=b.substring(b.lastIndexOf(" "));
        for (int i = 0; i<a.length();i++)
        {
            final String s = String.valueOf(a.toLowerCase().charAt(i));
            if(s.matches("[AEIYOUaeiyou]"))
                if(b.toLowerCase().contains(s)==false) return false;
        }
        for (int j = 0; j<b.length();j++)
        {
            final String s1 = String.valueOf(b.toLowerCase().charAt(j));
            if(s1.matches("[AEIYOUaeiyou]"))
                if(b.toLowerCase().contains(s1)==false) return false;
        }
        return true;
    }

    public static boolean trouble(long a,long b)
    {
        int k=0,l=0;
        long a1=a;
        long b1=b;
        while (a1>0)
        {
            a1=a1/10;
            l++;
        }
        while (b1>0)
        {
            b1=b1/10;
            k++;
        }

        long[] arr = new long[l];
        long[] arr2 = new long[k];

        for (int i=0;i<l;i++)
        {
            arr[i]=a%10;
            a=a/10;
        }
        for (int j=0;j<k;j++)
        {
            arr2[j]=b%10;
            b=b/10;
        }
        long count=0,count1=0,max=0,max1=0,num=0;
       for (int i1 = 0; i1 < arr.length;i1++)
       {
           boolean fl = false,fl1=false;
           num=arr[i1];
          for (int ar1 = 0; ar1 < arr.length;ar1++)
          {
              if (arr[ar1]==num)
              {
                  count++;
                   if (count==3) {
                       fl = true;
                       break;
                   }
              } else count = 0;
          }

          for (int ar2 = 0; ar2 < arr2.length;ar2++)
          {
              if (arr2[ar2]==num)
              {
                  count1++;
                  if (count1==2) {
                      fl1 = true;
                      break;
                  }
              } else count1 = 0;
          }
           if (fl==true&&fl1==true) return true;
       }
       return false;
    }
}
