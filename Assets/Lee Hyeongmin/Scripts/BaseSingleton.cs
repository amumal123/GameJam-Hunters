using UnityEngine;

public class BaseSingleton<T> : MonoBehaviour where T : MonoBehaviour // T는 MonoBehaviour를 상속받고 있어야 함
{
    private static T instance; // 실제로 쓰일 싱글톤 변수

    public static T Instance // 실제로 쓰일 싱글톤 변수를 외적으로 사용할때 씀(외부 접근 처리, 실제로 쓰일 싱글톤 인스턴스 생성,,,)
    {
        get
        {
            if (instance == null) // 만약 실제로 쓰일 싱글톤 변수에 값이 할당되지 않았다면 (처음 시작할땐 로직상 변수가 비어있기 때문에) 
            {
                // FindObjectOfType<T>()은 유니티6부턴 쓰이지 않음
                // 대체재: FindFirstObjectByType<T>()-가장 처음에 생성된 T를 찾아 반환 / FindAnyObjectByType<T>()-반환 대상을 특정할 수 없지만 더 빠름
                instance = FindAnyObjectByType<T>(); // 현재 씬에서 T 컴포넌트를 가진 게임 오브젝트를 찾고, 찾은 그걸 할당
                if (instance == null) // 만약 현재 씬에 T 컴포넌트를 가진 게임 오브젝트가 없다면
                {
                    // 게임 오브젝트를 새로 생성하고, 그것에 T 컴포넌트를 할당
                    GameObject obj = new(typeof(T).Name);
                    obj.AddComponent<T>();
                    //DontDestroyOnLoad(obj);
                }
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            // (T)this는 안되는 이유: 이 클래스를 상속받은 자식 클래스(예: GameManager)는 'private static GameManager instance'로 작성되기 때문에
            // 자식 클래스에선 GameManager 타입인 instance에다가 GameManager타입인 자신을 넣을 수 있지만,
            // 이 Singleton 클래스 자체에서는 그럴 의도가 없더라도 'private static T instance'에 Singleton<T> 타입을 넣으려는 걸로 처리하기 때문에
            // 명시적으로는 '(T)'가 아닌 'as T'를 사용하여 타입이 T인 instance에 Singleton<T>가 들어갈 경우를 고려해줘야 함
            instance = this as T;
            //DontDestroyOnLoad(gameObject);

            print(typeof(T).Name + " 생성 완료");
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}