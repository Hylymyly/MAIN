import javax.swing.*;
import java.awt.*;
import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.io.IOException;
import java.sql.SQLOutput;
import java.util.*;

import static javafx.scene.input.KeyCode.X;

class Levit extends JComponent{

    public void paint (Graphics g)
    {

    }

    public static int[][] schet(int[][] arr,int vNum) {
        int INF = Integer.MAX_VALUE / 2;
        int[][] dist = new int [vNum][vNum];
        int[] prev = new int [vNum];
        for (int i=0;i<vNum;i++) System.arraycopy(arr[i],0,dist[i],0,vNum);
        for (int k=0;k<vNum;k++)
            for (int i2 =0; i2<vNum;i2++)
                for (int j =0;j<vNum;j++)
                    dist[i2][j]=Integer.min(dist[i2][j],dist[i2][k]+dist[k][j]);
        return dist;
    }
}
class GFG{
    public static void main(String[] args) throws IOException {
        JFrame window = new JFrame();
        window.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        window.setBounds(30,30,300,300);
        window.getContentPane().add(new Levit());
        window.setVisible(true);
        Scanner scanner = new Scanner(new File("C:\\Users\\yaros\\SIAOD6\\src\\text.txt"));
        int start = scanner.nextInt();
        int end = scanner.nextInt();
        int n = scanner.nextInt();
        int[][] x = new int[n][n];
        for (int i = 0; i < n; i++) {
            for (int j = 0; j < n; j++) {
                x[i][j] = scanner.nextInt();
            }
        }
        int[][] inits = Levit.schet(x, n);
        for (int i = 0; i < n; i++) {
            for (int j = 0; j < n; j++) {
                System.out.print(inits[i][j] + " ");
            }
            System.out.println();
        }
        System.out.println("Short way: "+inits[start][end]);
        int answer = inits[start][end];
        double time = System.nanoTime();
        paintgraf(window.getGraphics(),n,x,start,end,answer);
        System.out.println("Time: "+(System.nanoTime()-time)/1000000);
    }
    public static void paintgraf(Graphics g,int a,int[][] arr,int st,int en,int ans)
    {
        int[] x = new int[a];
        int[] y = new int[a];
        int angle = 360/a;
        int i =0;
        //g.drawOval(100,100,100,100);
        while(i<a)
        {
            x[i] = (int)(140+50*Math.cos(Math.toRadians(angle*i)));
            y[i] = (int)(140+50*Math.sin(Math.toRadians(angle*i)));
            g.drawOval(x[i]-10,y[i]-10,20,20);
            g.drawString(""+i,x[i]+10,y[i]-5);
            i++;
        }
        for (int j=0;j<a;j++)
            for (int j2=0;j2<a;j2++)
                g.drawLine(x[j],y[j],x[j2],y[j2]);
            g.setColor(Color.GREEN);
        for (int k=0;k<a;k++) {
                if (arr[st][en] == ans)
                    g.drawLine(x[st],y[st],x[en],y[en]);
                else if (ans==arr[st][k]+arr[k][en]) {
                    g.drawLine(x[st],y[st],x[k],y[k]);
                    g.drawLine(x[k],y[k],x[en],y[en]);
                }
        }
    }
}