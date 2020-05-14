import org.w3c.dom.css.RGBColor;

import javax.swing.*;
import java.awt.*;

class Sierpinski extends JComponent{
   int x1=10,y1=190,x2=190,y2=190,x3=100,y3=10,depth=5;
   public void paint (Graphics g)
   {
      double time = System.nanoTime();
      g.drawLine(x1,y1,x2,y2);
      g.drawLine(x2,y2,x3,y3);
      g.drawLine(x1,y1,x3,y3);
      subTriangle(1,(x1+x2)/2,(y1+y2)/2,(x1+x3)/2,(y1+y3)/2,(x2+x3)/2,(y3+y2)/2,g);
      System.out.println("Time: "+(System.nanoTime()-time)/1000000);
   }
   public void subTriangle(int n, int x1, int y1, int x2, int y2, int x3, int y3,Graphics g)
   {
      g.drawLine(x1,y1,x2,y2);
      g.drawLine(x2,y2,x3,y3);

      g.drawLine(x1,y1,x3,y3);
      if (n<depth)
      {
         subTriangle(n+1,(x1+x2)/2+(x2-x3)/2,(y1+y2)/2+(y2-y3)/2,(x1+x2)/2+(x1-x3)/2,(y1+y2)/2+(y1-y3)/2,(x1+x2)/2,(y1+y2)/2,g);
         subTriangle(n+1,(x3+x2)/2+(x2-x1)/2,(y3+y2)/2+(y2-y1)/2,(x3+x2)/2+(x3-x1)/2,(y3+y2)/2+(y3-y1)/2,(x3+x2)/2,(y3+y2)/2,g);
         subTriangle(n+1,(x1+x3)/2+(x3-x2)/2,(y1+y3)/2+(y3-y2)/2,(x1+x3)/2+(x1-x2)/2,(y1+y3)/2+(y1-y2)/2,(x1+x3)/2,(y1+y3)/2,g);
      }
   }
}

class GFG {
   public static void main(String[] args) {
      JFrame window = new JFrame();
      window.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
      window.setBounds(30, 30, 300, 300);
      window.getContentPane().add(new Sierpinski());
      window.setVisible(true);
   }
}
