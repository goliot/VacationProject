# VacationProject
- 1인 게임 개발 프로젝트
---

### 0910
- 가장 가까이 떨어져 있는 아이템 줍기
- 서버 개발을 위한 포톤 셋업
---

### 0831
- 아이템 장착에 따른 능력치 변화 기반 마련
- 아이템 장착에 따른 장비창 디스플레이 변화 기반 마련
   - 장착시 장비창으로, 해제시 인벤토리로 옮김
---

### 0830
- 장비 UI 제작
- 장비 UI 관련 코드 구현
   - EquipmentUI.cs
- 아이템 타입 세분화
   - Equipment -> 6개의 파츠
- 아이템 장착 구현
- 인벤토리 - 장비창 UI 간의 상호작용 구현
   - 장비하면 인벤토리에서 사라지고, 장비 해제하면 인벤토리에 추가되는 시스템
---

### 0829
- 인벤토리에서, 아이템별 개수까지 표시되도록 변경
- 아이템 획득, 소모 이벤트를 발생시켜 즉시 인벤토리UI에 반영되도록 수정
- UI를 클릭할때에는 공격하지 않도록 수정
---

### 0827
- UIManager.cs, UIInputHandler.cs
   - 이벤트로, 특정 버튼을 누르면 해당 UI가 팝업되도록
- DraggableUI.cs
   - 생성된 UI를 마우스로 드래그하여 옮길 수 있도록 함
   - 각 UI들은 이 클래스를 상속받음
- PlayerItems.cs
   - 캐릭터가 갖고 있는 아이템 관리
- Item.cs
   - 아이템 프리팹에 인자로 집어 넣을 최상위 컨트롤러
- ItemEffect.cs
   - 스크립터블 오브젝트를 위한 추상 클래스
   - 각 아이템들은 해당 클래스를 상속받아 효과를 적용
---

### 0824
- 에셋 추가
   - 던전, 캐릭터 모션, 스킬 이펙트
---

### 0804
- 3D 오브젝트 무기들로 썸네일을 만드는 프로그램 구현
   - ItemCapture.cs
- 플레이어 Hp바 임시 구현
---

### 0803
- 적 추적이 정상적이지 않던 문제 수정
- 적의 죽음 애니메이션 및 로직 적용
- 로비 -> 로딩 -> 전투 씬 전환 구현
   - 비동기 씬 로드 방식 적용
- 씬 전환시 어둡게 렌더링되던 버그 수정
   - https://codingmania.tistory.com/202
---

### 0802
- 스폰시 위치 초기화가 정상적으로 이루어지지 않던 버그 수정
   - NavAgent가 활성화된 상태에서 강제로 위치를 이동시키다 보니 생겨난 버그
   - 위치를 이동 시킨 후에 활성화 시키는 것으로 해결함
- 거점 점령 시스템 임시 구현
---

### 0801
- 첫 스테이지의 맵 지형 확정
- 지형이 바뀜에 따른 적 이동 루트 등 움직임 로직 수정
- 플레이어측의 미니언 추가
---

### 0730
- 플레이어 더블점프 구현
- 적의 공격 및 데미지 적용 구현
- 플레이어 피격 구현
- Behaviour Tree를 통한 적 AI 테스트
---

### 0728
- Xml 파싱을 통한 캐릭터들의 스탯 불러오기 틀 구현
---

### 0727
- 플레이어 로직 수정
   - 콤보 연속 공격 구현
- 적 피격, 움직임, 추적 개선
---

### 0726
- 오브젝트 풀링을 통한 적 생성 구현
- 적의 공격, 추적 등 움직임 구현
---

### 0725
- FSM 기반의 캐릭터 상태 전이 로직 구현
- Terrain을 통한 맵 구현 연습
---

### 0724
- 오브젝트 풀링을 위한 풀 매니저 적용
- 사용할 에셋 정리
---

### 0721
- 3인칭 팔로잉 카메라 구현
- 메인 캐릭터 점프, 대쉬 등 추가 애니메이션 구현
---

### 0720
- 메인 캐릭터 이동, 애니메이션 적용
