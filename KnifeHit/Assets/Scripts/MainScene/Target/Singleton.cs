class Singleton
{
    // 싱글톤 객체를 담을 인스턴스 변수
    private static Singleton INSTANCE; 

    // 생성자는 private로 선언 
    private Singleton() {}

    // static 블록을 이용해 예외처리
    static {
        try {
            INSTANCE = new Singleton();
        } catch (Exception e) {
            throw new RuntimeException("싱글톤 객체 생성 오류");
        }
    }

    // 외부에서는 public static으로 선언되 getInstance로 접근
    public static Singleton getInstance() {
        return INSTANCE;
    }
}
