class Singleton {
    // 싱글톤 객체를 담을 인스턴스 변수
    private static Singleton instance;

    // 생성자는 private로 선언 
    private Singleton() {}
	
    // 외부에서 정적 메서드를 호출하면 초기화 진행
    public static Singleton getInstance() {
        if (instance == null) {
            instance = new Singleton(); // 오직 1개의 객체만 생성
        }
        return instance;
    }
}