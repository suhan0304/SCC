enum SingletonEnum {
    INSTANCE;

    private final Client dbClient;
	
    SingletonEnum() {
        dbClient = Database.getClient();
    }

    public static SingletonEnum getInstance() {
        return INSTANCE;
    }

    public Client getClient() {
        return dbClient;
    }
}

public class Main {
    public static void main(String[] args) {
        SingletonEnum singleton = SingletonEnum.getInstance();
        singleton.getClient();
    }
}