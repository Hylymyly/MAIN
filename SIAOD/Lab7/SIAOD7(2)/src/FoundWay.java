import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.MouseEvent;
import java.awt.event.MouseListener;

public class FoundWay {
    private MapCells[][] mapCells;
    /** The number of grid cells in the X direction. **/
    private int width;

    /** The number of grid cells in the Y direction. **/
    private int height;

    /** The location where the path starts from. **/
    private Location startLoc;
    private Location startLocx;
    private Location startLocy;

    /** The location where the path is supposed to finish. **/
    private Location finishLoc;
    private Location finishLocx;
    private Location finishLocy;


    private class MapCellHandler implements MouseListener
    {
        /**
         * This value will be true if a mouse button has been pressed and we are
         * currently in the midst of a modification operation.
         **/
        private boolean modifying;

        /**
         * This value records whether we are making cells passable or
         * impassable.  Which it is depends on the original state of the cell
         * that the operation was started within.
         **/
        private boolean makePassable;

        /** Initiates the modification operation. **/
        public void mousePressed(MouseEvent e)
        {
            modifying = true;

            MapCells cell = (MapCells) e.getSource();

            // If the current cell is passable then we are making them
            // impassable; if it's impassable then we are making them passable.

            makePassable = !cell.isPassable();

            cell.setPassable(makePassable);
        }

        /** Ends the modification operation. **/
        public void mouseReleased(MouseEvent e)
        {
            modifying = false;
        }

        /**
         * If the mouse has been pressed, this continues the modification
         * operation into the new cell.
         **/
        public void mouseEntered(MouseEvent e)
        {
            if (modifying)
            {
                MapCells cell = (MapCells) e.getSource();
                cell.setPassable(makePassable);
            }
        }

        /** Not needed for this handler. **/
        public void mouseExited(MouseEvent e)
        {
            // This one we ignore.
        }

        /** Not needed for this handler. **/
        public void mouseClicked(MouseEvent e)
        {
            // And this one too.
        }
    }

    public FoundWay(int w, int h) {
        if (w <= 0)
            throw new IllegalArgumentException("w must be > 0; got " + w);

        if (h <= 0)
            throw new IllegalArgumentException("h must be > 0; got " + h);

        width = w;
        height = h;

        startLoc = new Location(2, 7);
        finishLoc = new Location(w - 3, h / 2);
        initGUI();
    }

    private void initGUI()
    {
        JFrame frame = new JFrame("Pathfinder");
        frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        Container contentPane = frame.getContentPane();

        contentPane.setLayout(new BorderLayout());

        // Use GridBagLayout because it actually respects the preferred size
        // specified by the components it lays out.

        GridBagLayout gbLayout = new GridBagLayout();
        GridBagConstraints gbConstraints = new GridBagConstraints();
        gbConstraints.fill = GridBagConstraints.BOTH;
        gbConstraints.weightx = 1;
        gbConstraints.weighty = 1;
        gbConstraints.insets.set(0, 0, 1, 1);

        JPanel mapPanel = new JPanel(gbLayout);
        mapPanel.setBackground(Color.GRAY);

        mapCells = new MapCells[width][height];
        MapCellHandler cellHandler = new MapCellHandler();

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                mapCells[x][y] = new MapCells();

                gbConstraints.gridx = x;
                gbConstraints.gridy = y;

                gbLayout.setConstraints(mapCells[x][y], gbConstraints);

                mapPanel.add(mapCells[x][y]);
                mapCells[x][y].addMouseListener(cellHandler);
            }
        }

        contentPane.add(mapPanel, BorderLayout.CENTER);

        JButton findPathButton = new JButton("Find Path");
        findPathButton.addActionListener(new ActionListener() {
            public void actionPerformed(ActionEvent e) { findAndShowPath(); }
        });

        contentPane.add(findPathButton, BorderLayout.SOUTH);

        frame.pack();
        frame.setVisible(true);

        mapCells[startLoc.xCoord][startLoc.yCoord].setEndpoint(true);
        mapCells[finishLoc.xCoord][finishLoc.yCoord].setEndpoint(true);
    }

    private void findAndShowPath() {
        double time = System.nanoTime();
        /*startLoc = new Location(2, 7);
        finishLoc = new Location(w - 3, h / 2);*/
        for (int x = 0; x<finishLoc.xCoord-1;x++) {
            if (mapCells[startLoc.xCoord + x][startLoc.yCoord].isPassable()) {
                mapCells[startLoc.xCoord + x][startLoc.yCoord].setPath(true);
            }
            else
            {
                for (int x2 = 0; x2<finishLoc.xCoord-1;x2++) {
                    if (mapCells[startLoc.xCoord + x2][startLoc.yCoord].isPassable()) {
                        mapCells[startLoc.xCoord + x2][startLoc.yCoord].setPath(false);
                        mapCells[startLoc.xCoord + x2][startLoc.yCoord].setNope(true);
                    }
                    else break;
                }
                break;
            }
        }
        for (int y = 0;y<Math.abs(startLoc.yCoord-finishLoc.yCoord);y++) {
            if (mapCells[finishLoc.xCoord][finishLoc.yCoord - y].isPassable())
                mapCells[finishLoc.xCoord][finishLoc.yCoord - y].setPath(true);
            else
            {
                for (int y2 = 0;y2<Math.abs(startLoc.yCoord-finishLoc.yCoord);y2++) {
                    if (mapCells[finishLoc.xCoord][finishLoc.yCoord - y2].isPassable()){
                        mapCells[finishLoc.xCoord][finishLoc.yCoord - y2].setPath(false);
                        mapCells[finishLoc.xCoord][finishLoc.yCoord - y2].setNope(true);
                    }
                    else break;
                }
                break;
            }
        }
        for (int y1 = 0; y1<Math.abs(startLoc.yCoord-finishLoc.yCoord)+1;y1++) {
            if (mapCells[startLoc.xCoord][startLoc.yCoord + y1].isPassable())
                mapCells[startLoc.xCoord][startLoc.yCoord + y1].setPath(true);
            else
            {
                for (int y3 = 0; y3<Math.abs(startLoc.yCoord-finishLoc.yCoord)+1;y3++) {
                    if (mapCells[startLoc.xCoord][startLoc.yCoord + y3].isPassable()){
                        mapCells[startLoc.xCoord][startLoc.yCoord + y3].setPath(false);
                        mapCells[startLoc.xCoord][startLoc.yCoord + y3].setNope(true);
                    }
                    else break;
                }
                break;
            }
        }

        for (int x1 =0; x1<finishLoc.xCoord-startLoc.xCoord;x1++) {
            if (mapCells[finishLoc.xCoord - x1][finishLoc.yCoord].isPassable())
                mapCells[finishLoc.xCoord - x1][finishLoc.yCoord].setPath(true);
            else
            {
                for (int x3 =0; x3<finishLoc.xCoord-startLoc.xCoord;x3++) {
                    if (mapCells[finishLoc.xCoord - x3][finishLoc.yCoord].isPassable()){
                        mapCells[finishLoc.xCoord - x3][finishLoc.yCoord].setPath(false);
                        mapCells[finishLoc.xCoord - x3][finishLoc.yCoord].setNope(true);
                    }
                    else break;
                }
                break;
            }
        }
        for (int y4 = 0;y4<startLoc.yCoord+1;y4++)
        {
            mapCells[startLoc.xCoord][startLoc.yCoord - y4].setNope(true);
        }
        for (int x4 = 0;x4<startLoc.xCoord+1;x4++)
        {
            mapCells[startLoc.xCoord-x4][startLoc.yCoord].setNope(true);
        }
        for (int y5 = 0; y5<height-finishLoc.yCoord;y5++)
        {
            mapCells[finishLoc.xCoord][finishLoc.yCoord+y5].setNope(true);
        }
        for (int x5 = 0; x5<width-finishLoc.xCoord;x5++)
        {
            mapCells[finishLoc.xCoord+x5][finishLoc.yCoord].setNope(true);
        }
        System.out.println("Time: "+(System.nanoTime()-time)/1000000);
    }

    public static void main(String[] args) {
        FoundWay rayFind = new FoundWay(40,30);
    }


}
