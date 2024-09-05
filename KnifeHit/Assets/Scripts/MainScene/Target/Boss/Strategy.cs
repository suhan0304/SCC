class Strategy() {
    public interface MovableStrategy {
    public void move();
}

    public class RailLoadStrategy implements MovableStrategy{
        public void move(){
            System.out.println("선로를 통해 이동");
        }
    }

    public class LoadStrategy implements MovableStrategy{
        public void move() {
            System.out.println("도로를 통해 이동");
        }
    }
}

public class Moving {
    private MovableStrategy movableStrategy;

    public void move(){
        movableStrategy.move();
    }

    public void setMovableStrategy(MovableStrategy movableStrategy){
        this.movableStrategy = movableStrategy;
    }
}

public class Bus extends Moving{

}

public class Train extends Moving{

}