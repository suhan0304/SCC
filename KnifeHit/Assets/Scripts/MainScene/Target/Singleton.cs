class Singleton
{
    // 싱글톤 객체를 담을 인스턴스 변수
    private static readonly Singleton INSTANCE = new Singleton(); 

    // 생성자는 private로 선언
    private Singleton() {}

    // 외부에서는 public static으로 선언되 getInstance로 접근
    public static Singleton getInstance() {
        return INSTANCE;
    }
}
