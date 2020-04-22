
import java.nio.charset.StandardCharsets;
import java.security.NoSuchAlgorithmException;
import java.security.MessageDigest;
import java.util.*;
import java.util.Base64;
import java.util.LinkedHashSet;

public class KTP50 {

    public static void main(String[] args) throws NoSuchAlgorithmException {
        /*int[] arr = new int[]{72, 33, -73, 84, -12, -3, 13, -13, -68};
        String[] arr2 = new String[]{"hoops", "chuff", "bot", "bottom" +
                ""};
        String[] arr3 = sameVowelGroup(arr2);
        //long[] ints = validateCard(1234567890123456L);
        for (int i = 0; i < arr3.length; i++)
            System.out.print(arr3[i] + " ");*/
        //System.out.println(encrypt("Hello"));
        //System.out.println(decrypt(arr));
        //System.out.println(sumDigProd(1,2,3,4,5,6));
        //System.out.println(validateCard(1234567890123452L));
        //System.out.println(numToEng(901));
        //System.out.println(sameVowelGroup(arr2));
        //System.out.println(canMove("Bishop","A7","G1"));
        //System.out.println(getSha256Hash("password123"));
        //System.out.println(correctTitle("jOn SnoW, kINg IN thE noRth."));
    }

    public static int[] encrypt(String s) {
        int[] arr = new int[s.length()];
        char firstsym = s.charAt(0);
        arr[0] = firstsym;
        for (int i = 1; i < s.length(); i++) {
            arr[i] = s.charAt(i - 1) - s.charAt(i);
        }
        return arr;
    }

    public static String decrypt(int[] arr) {
        int[] arr2 = new int[arr.length];
        arr2[0] = arr[0];
        String line = "";
        for (int j = 1; j < arr.length; j++) {
            arr2[j] = arr2[j - 1] + arr[j];
        }
        for (int i = 0; i < arr.length; i++) {
            line += (String.valueOf((char) arr2[i]));
        }
        return line;
    }

    public static boolean canMove(String figure, String cow, String row)
    {
        char cowstr = cow.toLowerCase().charAt(0);//letter
        char rowstr = row.toLowerCase().charAt(0);
        int cowint = Integer.parseInt(String.valueOf(cow.charAt(1)));//number
        int rowint = Integer.parseInt(String.valueOf(row.charAt(1)));
        switch (figure)
        {
            case "King":
            {
                if ((Math.abs(cowstr-rowstr)<2)&&(Math.abs(cowint-rowint)<2)) return true; else return false;
            }
            case "Rook":
            {
                if(((cowstr==rowstr)&&(cowint!=rowint))||((cowstr!=rowstr)&&(cowint==rowint))) return true;else return false;
            }
            case "Queen":
            {
                if(((cowstr!=rowstr)&&(cowint==rowint))||((cowstr==rowstr)&&(cowint!=rowint))) return true;
                if(Math.abs(cowint-rowint)==Math.abs(cowstr-rowstr)) return true;
                break;
            }
            case "Bishop":
            {
                if (Math.abs(cowstr-rowstr)==Math.abs(cowint-rowint)) return true;else return false;
            }
            case "Knight":
            {
                if(((Math.abs(cowstr-rowstr)==1)&&(Math.abs(cowint-rowint)==2))||((Math.abs(cowstr-rowstr)==2)&&(Math.abs(cowint-rowint)==1))) return true; else return false;
            }
            case "Pawn":
            {
                if ((cowstr==rowstr)&&(cowint==2)&&(rowint==4)) return true;
                if ((cowstr==rowstr)&&(cowint==(rowint-1))) return true;
                return false;
            }
            default: return false;
        }
        return false;
    }

    public static boolean canComplete(String a, String b)
    {
        char[] chars = a.toCharArray();
        int k=0;
        for (int i = 0; i<chars.length;i++)
        {
            if(b.indexOf(String.valueOf(chars[i]),k)!=-1)
                k=b.indexOf(String.valueOf(chars[i]),k)+1;
            else return false;
        }
        return true;
    }

    public static int sumDigProd(int ... a)
    {
        int sum=0;
        for (int i=0;i<a.length;i++){
            sum+=a[i];
        }
        int k=0;
        int b=sum;
        while (sum>10) {
            b = 1;
            while (sum!=0) {
                b = b * (sum % 10);
                sum = sum / 10;
            }
            sum=b;
        }
        return b;
    }

    public static String[] sameVowelGroup(String[] arr)
    {
        StringBuilder sb = new StringBuilder();
        LinkedHashSet<Character> temp = new LinkedHashSet<>();
        for (int j =0;j<arr.length;j++)
        {
            arr[j]=arr[j].toLowerCase();
        }
        String line = "";
        String linechar = "";
        String[] line2 = new String[arr.length];
        line2[0]=arr[0];
        for (char chars: arr[0].toCharArray())
        {
            if ((chars=='a')||(chars=='e')||(chars=='i')||(chars=='o')||(chars=='u')||(chars=='y'))
                linechar+=chars;
        }
        for (int r =0;r<linechar.length();r++)
        {
            if (temp.add(linechar.charAt(r))) sb.append(linechar.charAt(r));
        }
        for (int i=1;i<arr.length;i++){
            for(char chars: arr[i].toCharArray())
            {
                if ((chars=='a')||(chars=='e')||(chars=='i')||(chars=='o')||(chars=='u')||(chars=='y'))
                line+=chars;
            }
            if(line.contains(sb)) line2[i]=arr[i]; else line2[i]="";
            line="";
        }
        return line2;
    }

    public static boolean validateCard(long a)
    {
        //71497263
        long outlast = Long.valueOf(a/10);
        long kontr = a-outlast*10;
        int k=0;
        long b =a;
        while (b>0)
        {
            b=b/10;
            k++;
        }
        long[] arr = new long[k-1];
        int i=0;
        while (outlast>0) {
            arr[i] = outlast % 10;
            outlast = outlast / 10;
            i++;
        }
        long sum =0;
        for (int j=0;j<arr.length;j=j+2)
        {
            arr[j] = arr[j]*2;
            if(arr[j]>9)
            {
               arr[j]=(arr[j]/10)+(arr[j]%10);
            }
        }
        for (int kount=0;kount<arr.length;kount++)
        {
            sum+=arr[kount];
        }
        long lastfin = sum%10;
        long res = 10-lastfin;
       if (res==kontr) return true; else return false;
    }

    public static String numToRus(int a)
    {
        String s = "";
        if (a==0)  s="ноль";
        if ((a>0)&&(a<1000))
        {
            switch (a/100)
            {
                case 1: s="сто ";break;
                case 2: s="двести ";break;
                case 3: s="триста ";break;
                case 4: s="четыреста ";break;
                case 5: s="пятьсот ";break;
                case 6: s="шестьсот ";break;
                case 7: s="семьсот ";break;
                case 8: s="восемьсот ";break;
                case 9: s="девятьсот ";break;
            }
            switch ((a%100)/10)
            {
                case 2: s=s+"двадцать ";break;
                case 3: s=s+"тридцать ";break;
                case 4: s=s+"сорок ";break;
                case 5: s=s+"пятьдесят ";break;
                case 6: s=s+"шестьдесят ";break;
                case 7: s=s+"семьдесят ";break;
                case 8: s=s+"восемьдесят ";break;
                case 9: s=s+"девяносто ";break;
            }
            if (a%100>=10&a%100<20){
                switch(a%100)
                {
                    case 10: s=s+"десять ";break;
                    case 11: s=s+"одиннадцать ";break;
                    case 12: s=s+"двянадцать ";break;
                    case 13: s=s+"тринадцать ";break;
                    case 14: s=s+"четырнадцать ";break;
                    case 15: s=s+"пятнадцать ";break;
                    case 16: s=s+"шестналцать ";break;
                    case 17: s=s+"семнадцать ";break;
                    case 18: s=s+"восемнадцать ";break;
                    case 19: s=s+"девятнадцать ";break;
                }
            }else {
                switch (a%10){
                    case 1: s=s+"один ";break;
                    case 2: s=s+"два ";break;
                    case 3: s=s+"три ";break;
                    case 4: s=s+"четыре ";break;
                    case 5: s=s+"пять ";break;
                    case 6: s=s+"шесть ";break;
                    case 7: s=s+"семь ";break;
                    case 8: s=s+"восемь ";break;
                    case 9: s=s+"девять ";break;
                }
            }
        }
        return s;
    }

    public static String numToEng(int a)
    {
        String s = "";
        if (a==0)  s="zero";
        if ((a>0)&&(a<1000))
        {
            switch (a/100)
            {
                case 1: s="hundred ";break;
                case 2: s="two hundred ";break;
                case 3: s="three hundred ";break;
                case 4: s="four hundred ";break;
                case 5: s="five hundred ";break;
                case 6: s="siz hundred ";break;
                case 7: s="seven hundred ";break;
                case 8: s="eight hundred ";break;
                case 9: s="nine hundred ";break;
            }
            switch ((a%100)/10)
            {
                case 2: s=s+"twenty ";break;
                case 3: s=s+"thirty ";break;
                case 4: s=s+"forty ";break;
                case 5: s=s+"fifty ";break;
                case 6: s=s+"sixty ";break;
                case 7: s=s+"seventy ";break;
                case 8: s=s+"eighty ";break;
                case 9: s=s+"ninety ";break;
            }
            if (a%100>=10&a%100<20){
                switch(a%100)
                {
                    case 10: s=s+"ten ";break;
                    case 11: s=s+"eleven ";break;
                    case 12: s=s+"twelve ";break;
                    case 13: s=s+"thirteen ";break;
                    case 14: s=s+"fourteen ";break;
                    case 15: s=s+"fifteen ";break;
                    case 16: s=s+"sixteen ";break;
                    case 17: s=s+"seventeen ";break;
                    case 18: s=s+"eighteen ";break;
                    case 19: s=s+"nineteen ";break;
                }
            }else {
                switch (a%10){
                    case 1: s=s+"one ";break;
                    case 2: s=s+"two ";break;
                    case 3: s=s+"three ";break;
                    case 4: s=s+"four ";break;
                    case 5: s=s+"five ";break;
                    case 6: s=s+"six ";break;
                    case 7: s=s+"seven ";break;
                    case 8: s=s+"eight ";break;
                    case 9: s=s+"nine ";break;
                }
            }
        }
        return s;
    }

    public static String getSha256Hash(String str) throws NoSuchAlgorithmException {
        MessageDigest digest = MessageDigest.getInstance("SHA-256");
        byte[] encodedhash = digest.digest(str.getBytes(StandardCharsets.UTF_8));
        String res = Base64.getEncoder().encodeToString(encodedhash);
        char[] chars = "0123456789ABCDEFF".toCharArray();
        char[] chars1 = new char[encodedhash.length*2];
        for (int i = 0; i < encodedhash.length;i++)
        {
            int v = encodedhash[i] & 0xFF;
            chars1[i*2]=chars[v>>>4];
            chars1[i*2+1]=chars[v & 0x0F];
        }
        return new String(chars1).toLowerCase();
    }

    public static String correctTitle(String a)
    {
        String line ="";
        String[] input = a.toLowerCase().split(" ");
        for(String str:input)
        {
            String word = String.valueOf(str.charAt(0)).toUpperCase();
            if(str.equals("and")||str.equals("the")||str.equals("of")||str.equals("in")) line = line + str + " ";
            else line = line + word +str.substring(1)+" ";
        }
        return line;
    }
}
